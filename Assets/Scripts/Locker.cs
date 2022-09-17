using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Locker : MonoBehaviour
{
    private int PassCode;
    private int PassCodeEntered;
    private bool Locked = true;
    [SerializeField] TMP_InputField PasscodeField;
    [SerializeField] TMP_Text PasscodeText;
    [SerializeField] Doors Issue;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        PassCode = UnityEngine.Random.Range(1000, 9999);
        anim = GetComponent<Animator>();
        PasscodeText.text = PassCode.ToString();
    }

    public void PassCodeCheck()
    {
        if (PasscodeField.text != null)
        {
            PassCodeEntered = Convert.ToInt32(PasscodeField.text);
            if(PassCodeEntered == PassCode)
            {
                Issue.anyIssue = false;
                Locked = false;
                UIController.instance.PasscodeScreen.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void BackButton()
    {
        UIController.instance.PasscodeScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if(!Locked)
        { return; }
        if (Vector3.Distance(PlayerController.instance.gameObject.transform.position, transform.position) >= PlayerController.instance.Range)
        { return; }

        if (PlayerController.instance.OnTargetGameObject == gameObject)
        {
            UIController.instance.infoText.text = "Press E to enter passcode";
            UIController.instance.infoText.gameObject.SetActive(true);
            if(Input.GetButtonDown("UseButton"))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                UIController.instance.PasscodeScreen.SetActive(true);
            }
        }
    }
}
