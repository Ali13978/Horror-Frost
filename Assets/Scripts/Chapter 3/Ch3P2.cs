using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Ch3P2 : MonoBehaviour
{
    [Header("Gear Holder")]
    [SerializeField] bool isGearHolder;
    [SerializeField] GameObject Gear;

    [Header("Gear")]
    [SerializeField] bool isGear;
    [SerializeField] GameObject Handle;

    [Header("Handle")]
    [SerializeField] bool isHandle;
    [SerializeField] Animator Anim;
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
            if (isGearHolder)
            {
                if (PlayerController.instance.GrabbedObjectName != "Gear Box")
                {
                    UIController.instance.ObjectiveText.text = "Find and use gear box in the generator";
                    UIController.instance.ObjectiveText.gameObject.SetActive(true);
                    UIController.instance.infoText.text = "I need gear box to place here";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to place gear";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        UIController.instance.ObjectiveText.gameObject.SetActive(false);
                        Destroy(PlayerController.instance.grabbingObject.gameObject);
                        PlayerController.instance.grabbingObject = null;
                        PlayerController.instance.GrabbedObjectName = null;
                        UIController.instance.infoText.gameObject.SetActive(false);
                        UIController.instance.grabbedObjectInfo.gameObject.SetActive(false);
                        Gear.SetActive(true);
                        Destroy(gameObject.GetComponent<Ch3P2>());
                    }
                }
            }

            else if (isGear)
            {
                if (PlayerController.instance.GrabbedObjectName != "button")
                {
                    UIController.instance.ObjectiveText.text = "Find and place button to use generator";
                    UIController.instance.ObjectiveText.gameObject.SetActive(true);
                    UIController.instance.infoText.text = "I need button to start generator";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to place button";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        UIController.instance.ObjectiveText.gameObject.SetActive(false);
                        Destroy(PlayerController.instance.grabbingObject.gameObject);
                        PlayerController.instance.grabbingObject = null;
                        PlayerController.instance.GrabbedObjectName = null;
                        UIController.instance.infoText.gameObject.SetActive(false);
                        UIController.instance.grabbedObjectInfo.gameObject.SetActive(false);
                        Handle.SetActive(true);
                        Destroy(gameObject.GetComponent<Ch3P2>());
                    }
                }
            }

            else if (isHandle)
            {
                UIController.instance.infoText.text = "Press E to use the generator";
                UIController.instance.infoText.gameObject.SetActive(true);
                if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                {
                    Anim.SetTrigger("StartRotation");
                    Destroy(gameObject.GetComponent<Ch3P2>());
                }
            }
        }
    }
}
