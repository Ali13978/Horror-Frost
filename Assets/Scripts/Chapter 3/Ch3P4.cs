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
    [SerializeField] Ch3P4 Rock;
    private int PassCode;
    private int PassCodeEntered;


    [Header("Rock")]
    [SerializeField] bool isRock;
    [SerializeField] Ch3P4 Statue;
    [SerializeField] GameObject Spirit;
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
                UIController.instance.ObjectiveText.text = "Find and enter passcode in the statue";
                UIController.instance.ObjectiveText.gameObject.SetActive(true);
                UIController.instance.infoText.text = "Press E to enter passcode";
                UIController.instance.infoText.gameObject.SetActive(true);
                if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                {
                    PasscodeScreen.SetActive(true);
                }
            }

            else if(isRock && Statue == null)
            {
                if (PlayerController.instance.GrabbedObjectName != "PickAxe")
                {
                    UIController.instance.ObjectiveText.text = "Find and use pickaxe to break rock of statue";
                    UIController.instance.ObjectiveText.gameObject.SetActive(true);
                    UIController.instance.infoText.text = "I need pickaxe to break this rock";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to break stone";
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

    public void PassCodeCheck()
    {
        if (PasscodeField.text != null)
        {
            PassCodeEntered = System.Convert.ToInt32(PasscodeField.text);
            if (PassCodeEntered == PassCode)
            {
                UIController.instance.ObjectiveText.gameObject.SetActive(false);
                statueAnim.SetTrigger("Move");
                Rock.Statue = null;
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
