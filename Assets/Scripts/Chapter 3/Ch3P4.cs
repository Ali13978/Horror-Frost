using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.CrossPlatformInput;

public class Ch3P4 : MonoBehaviour
{
    [Header("Statue")]
    [SerializeField] bool isStatue;
    [SerializeField] Animator statueAnim;
    [SerializeField] TMP_InputField PasscodeField;
    [SerializeField] TMP_Text PasscodeText;
    [SerializeField] GameObject PasscodeScreen;
    private int PassCode;
    private int PassCodeEntered;


    [Header("Rock")]
    [SerializeField] bool isRock;
    // Start is called before the first frame update
    void Start()
    {
        if (isStatue)
        {
            PassCode = UnityEngine.Random.Range(1000, 9999);
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
            if (isStatue)
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

            else if(isRock)
            {
                if (PlayerController.instance.GrabbedObjectName != "PickAxe")
                {
                    UIController.instance.infoText.text = "I need pickaxe to break this rock";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to break stone";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        Destroy(gameObject);
                    }
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
                statueAnim.SetTrigger("Move");
                PasscodeScreen.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Destroy(gameObject.GetComponent<Ch3P4>());
            }
        }
    }

    public void BackButton()
    {
        PasscodeScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
