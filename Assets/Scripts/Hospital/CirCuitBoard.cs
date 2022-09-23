using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CirCuitBoard : MonoBehaviour
{
    [SerializeField] Animator myAnimator;
    [SerializeField] Doors IssueFile;
    [SerializeField] GameObject MainLever;

    private bool Issue = true;

    private void Start()
    {
        MainLever.SetActive(false);
    }

    private void Update()
    {
        if(myAnimator.GetBool("TurnOnLever") == true)
        { return; }
        if (Vector3.Distance(PlayerController.instance.gameObject.transform.position, transform.position) >= PlayerController.instance.Range)
        { return; }

        if (PlayerController.instance.OnTargetGameObject == gameObject)
        {
            if (Issue == false)
            {
                UIController.instance.infoText.text = "Press E to use lever";
                UIController.instance.infoText.gameObject.SetActive(true);
                if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                {
                    IssueFile.anyIssue = false;
                    myAnimator.SetBool("TurnOnLever", true);
                }
            }
            else
            {
                if (PlayerController.instance.GrabbedObjectName != "lever")
                {
                    UIController.instance.infoText.text = "I need lever to place here";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }

                else
                {
                    UIController.instance.infoText.text = "Press E to place lever";
                    UIController.instance.infoText.gameObject.SetActive(true);

                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        Issue = false;
                        Destroy(PlayerController.instance.grabbingObject.gameObject);
                        MainLever.SetActive(true);
                        PlayerController.instance.grabbingObject = null;
                        PlayerController.instance.GrabbedObjectName = null;
                        UIController.instance.grabbedObjectInfo.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
