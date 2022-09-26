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
                if (PlayerController.instance.GrabbedObjectName != "Gear")
                {
                    UIController.instance.infoText.text = "I need gear to place here";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to place gear";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
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
                if (PlayerController.instance.GrabbedObjectName != "Handle")
                {
                    UIController.instance.infoText.text = "I need handle to rotate the gear";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to place handle";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
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
                UIController.instance.infoText.text = "Press E to rotate the handle";
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
