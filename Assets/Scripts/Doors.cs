using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Doors : MonoBehaviour
{
    [SerializeField] Animator myAnimator;
    [SerializeField] public bool anyIssue;
    [SerializeField] string IssueError;
    [SerializeField] string Name;

    private void Update()
    {
        if(Vector3.Distance(PlayerController.instance.gameObject.transform.position, transform.position) >= PlayerController.instance.Range)
        { return; }

        if(anyIssue)
        {
            if (PlayerController.instance.OnTargetGameObject == gameObject)
            {
                if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                {
                    UIController.instance.infoText.text = IssueError;
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
            }
            return;
        }

        else if(PlayerController.instance.OnTargetGameObject == gameObject && !anyIssue)
        {
            UIController.instance.infoText.text = "Press E to Open/Close the " + Name;
            UIController.instance.infoText.gameObject.SetActive(true);

            if(CrossPlatformInputManager.GetButtonDown("UseButton"))
            {
                if (!myAnimator.GetBool("Open"))
                {   myAnimator.SetBool("Open", true);   }
                else
                {   myAnimator.SetBool("Open", false);  }
            }
        }
    }
}
