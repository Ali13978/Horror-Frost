using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] Animator myAnimator;
    [SerializeField] public bool anyIssue;
    [SerializeField] string IssueError;
    [SerializeField] string Name;

    private void Update()
    {
        if(Vector3.Distance(PlayerController.instance.gameObject.transform.position, transform.position) >= 6)
        { return; }

        if(anyIssue)
        {
            if (PlayerController.instance.OnTargetGameObject == gameObject)
            {
                if (Input.GetButtonDown("UseButton"))
                {
                    UIController.instance.infoText.text = IssueError;
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
            }
            return;
        }

        if(PlayerController.instance.OnTargetGameObject == gameObject)
        {
            UIController.instance.infoText.text = "Press E to Open/Close the " + Name;
            UIController.instance.infoText.gameObject.SetActive(true);

            if(Input.GetButtonDown("UseButton"))
            {
                if (!myAnimator.GetBool("Open"))
                {   myAnimator.SetBool("Open", true);   }
                else
                {   myAnimator.SetBool("Open", false);  }
            }
        }
    }
}