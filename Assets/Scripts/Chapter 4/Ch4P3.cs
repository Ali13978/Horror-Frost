using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;

public class Ch4P3 : MonoBehaviour
{
    [Header("Bucket")]
    [SerializeField] bool isBucket;

    [Header("Dirty Slab")]
    [SerializeField] bool isDirtySlab;

    [Header("Passcode system")]
    [SerializeField] bool isPasscodeDevice;
    [SerializeField] Animator Anim;
    [SerializeField] TMP_InputField PasscodeField;
    [SerializeField] TMP_Text PasscodeText;
    [SerializeField] GameObject PasscodeScreen;
    private int PassCode;
    private int PassCodeEntered;

    [Header("P2Grave")]
    [SerializeField] bool isP2Grave;
    

    // Start is called before the first frame update
    void Start()
    {
        if(isPasscodeDevice)
        {
            PassCode = Random.Range(1000, 9999);
            PasscodeText.text = PassCode.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(PlayerController.instance.gameObject.transform.position, transform.position) >= PlayerController.instance.Range)
        { return; }

        if (PlayerController.instance.OnTargetGameObject == gameObject)
        {
            if (isBucket)
            {
                if (PlayerController.instance.GrabbedObjectName != "Cloth")
                {
                    UIController.instance.infoText.text = "I can wet the cloth piece here";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to wet cloth";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        PlayerController.instance.grabbingObject.GetComponent<GrabableObject>().ObjectName = "wet cloth";
                        PlayerController.instance.GrabbedObjectName = "wet cloth";
                        UIController.instance.grabbedObjectInfo.text = "You are grabbing the wet cloth";
                        Destroy(gameObject);
                    }
                }
            }

            else if(isDirtySlab)
            {
                if (PlayerController.instance.GrabbedObjectName != "wet cloth")
                {
                    UIController.instance.infoText.text = "I need something to clean it";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to clean it";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        Destroy(PlayerController.instance.grabbingObject.gameObject);
                        PlayerController.instance.grabbingObject = null;
                        PlayerController.instance.GrabbedObjectName = null;
                        UIController.instance.infoText.gameObject.SetActive(false);
                        UIController.instance.grabbedObjectInfo.gameObject.SetActive(false);
                        Destroy(gameObject);
                    }
                }
            }

            else if(isP2Grave)
            {
                if (Anim.GetBool("Move"))
                {
                    UIController.instance.infoText.text = "Press E to rotate it";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        Anim.SetTrigger("Rotate");
                    }
                }

                else
                {
                    UIController.instance.infoText.text = "First add passcode";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
            }

            else if(isPasscodeDevice)
            {
                UIController.instance.infoText.text = "Press E to enter passcode";
                UIController.instance.infoText.gameObject.SetActive(true);
                if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    PasscodeScreen.SetActive(true);
                }
            }
        }
    }


    public void PassCodeCheck()
    {
        if (PasscodeField.text != null)
        {
            PassCodeEntered = System.Convert.ToInt32(PasscodeField.text);
            if (PassCodeEntered == PassCode)
            {
                Anim.SetBool("Move", true);
                PasscodeScreen.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    public void BackButton()
    {
        PasscodeScreen.SetActive(false);
    }
}
