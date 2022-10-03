using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Ch2P5 : MonoBehaviour
{
    [Header("man")]
    [SerializeField] bool isMan;
    [SerializeField] Doors lockedDoor;

    [Header("UnTiedPlanks")]
    [SerializeField] bool isUntiedPlank;
    [SerializeField] GameObject TiedPlanks;

    [Header("Crate")]
    [SerializeField] bool isCrate;
    [SerializeField] GameObject Spirit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(PlayerController.instance.gameObject.transform.position, transform.position) >= PlayerController.instance.Range)
        { return; }

        if (PlayerController.instance.OnTargetGameObject == gameObject)
        {
            if (isMan)
            {
                if (PlayerController.instance.GrabbedObjectName != "tiedPlanks")
                {
                    UIController.instance.ObjectiveText.text = "Find and give tied planks to stranger at side of the wooden house";
                    UIController.instance.ObjectiveText.gameObject.SetActive(true);
                    UIController.instance.infoText.text = "Stranger: First take the tied planks for me to enter the house";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to give planks";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        UIController.instance.ObjectiveText.gameObject.SetActive(false);
                        Destroy(PlayerController.instance.grabbingObject.gameObject);
                        PlayerController.instance.grabbingObject = null;
                        PlayerController.instance.GrabbedObjectName = null;
                        UIController.instance.infoText.gameObject.SetActive(false);
                        UIController.instance.grabbedObjectInfo.gameObject.SetActive(false);
                        lockedDoor.anyIssue = false;
                        Destroy(gameObject.GetComponent<Ch2P5>());
                    }
                }
            }

            else if (isUntiedPlank)
            {
                if (PlayerController.instance.GrabbedObjectName != "rope")
                {
                    UIController.instance.ObjectiveText.text = "Find and use rope to tie these planks";
                    UIController.instance.ObjectiveText.gameObject.SetActive(true);
                    UIController.instance.infoText.text = "I need something to tie these planks";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to tie planks";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        UIController.instance.ObjectiveText.gameObject.SetActive(false);
                        Destroy(PlayerController.instance.grabbingObject.gameObject);
                        PlayerController.instance.grabbingObject = null;
                        PlayerController.instance.GrabbedObjectName = null;
                        UIController.instance.infoText.gameObject.SetActive(false);
                        UIController.instance.grabbedObjectInfo.gameObject.SetActive(false);
                        TiedPlanks.SetActive(true);
                        Destroy(gameObject);
                    }
                }
            }

            else if(isCrate)
            {
                if (PlayerController.instance.GrabbedObjectName != "PickAxe")
                {
                    UIController.instance.ObjectiveText.text = "Find and use pickaxe to destroy the crate";
                    UIController.instance.ObjectiveText.gameObject.SetActive(true);
                    UIController.instance.infoText.text = "I need pickaxe to break this crate";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to break crate";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        UIController.instance.ObjectiveText.gameObject.SetActive(false);
                        Spirit.SetActive(true);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
