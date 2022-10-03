using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MachineController : MonoBehaviour
{
    Animator anim;

    [SerializeField] GameObject Lever;
    [SerializeField] GameObject Blade;
    [SerializeField] GameObject GrabbingBlade;
    [SerializeField] string BladeName;

    [SerializeField] GameObject Crate;
    [SerializeField] GameObject Spirit;

    bool issue = true;
    bool MachineUsed = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Spirit.SetActive(false);
    }

    public void RunMachine()
    {
        UIController.instance.infoText.text = "Press E to run machine";
        UIController.instance.infoText.gameObject.SetActive(true);
        if (CrossPlatformInputManager.GetButtonDown("UseButton"))
        {
            anim.SetBool("MachineStarted", true);
        }
    }

    public void BladeManage()
    {
        if(Blade.activeInHierarchy)
        { return; }

        if (PlayerController.instance.GrabbedObjectName == null && PlayerController.instance.grabbingObject == null)
        {
            UIController.instance.ObjectiveText.text = "Find and place blade on machine";
            UIController.instance.ObjectiveText.gameObject.SetActive(true);
            UIController.instance.infoText.text = "I need blade to place here in machine";
            UIController.instance.infoText.gameObject.SetActive(true);
        }
        else if (PlayerController.instance.GrabbedObjectName == BladeName && PlayerController.instance.grabbingObject == GrabbingBlade)
        {
            UIController.instance.infoText.text = "Press E to place blade";
            UIController.instance.infoText.gameObject.SetActive(true);
            if (CrossPlatformInputManager.GetButtonDown("UseButton"))
            {
                UIController.instance.ObjectiveText.gameObject.SetActive(false);
                Blade.SetActive(true);
                Destroy(GrabbingBlade);
                PlayerController.instance.grabbingObject = null;
                PlayerController.instance.GrabbedObjectName = null;
                UIController.instance.infoText.gameObject.SetActive(false);
                UIController.instance.grabbedObjectInfo.gameObject.SetActive(false);
            }
        }
    }

    public void PlaceCrate()
    {
        if (Crate.activeInHierarchy)
        { return; }

        if (PlayerController.instance.GrabbedObjectName == null && PlayerController.instance.grabbingObject == null)
        {
            UIController.instance.ObjectiveText.text = "Find and place crate on machine";
            UIController.instance.ObjectiveText.gameObject.SetActive(true);
            UIController.instance.infoText.text = "I need something needed to be destroyed here";
            UIController.instance.infoText.gameObject.SetActive(true);
        }

        else if (PlayerController.instance.GrabbedObjectName != "crate")
        {
            UIController.instance.infoText.text = "I need something else to place here";
            UIController.instance.ObjectiveText.text = "Find and place crate on machine";
            UIController.instance.ObjectiveText.gameObject.SetActive(true);
            UIController.instance.infoText.gameObject.SetActive(true);
        }
        else if (PlayerController.instance.GrabbedObjectName == "crate")
        {
            UIController.instance.infoText.text = "Press E to place crate";
            UIController.instance.infoText.gameObject.SetActive(true);
            if (CrossPlatformInputManager.GetButtonDown("UseButton"))
            {
                Crate.SetActive(true);
                UIController.instance.ObjectiveText.gameObject.SetActive(false);
                Destroy(PlayerController.instance.grabbingObject.gameObject);
                PlayerController.instance.grabbingObject = null;
                PlayerController.instance.GrabbedObjectName = null;
                UIController.instance.infoText.gameObject.SetActive(false);
                UIController.instance.grabbedObjectInfo.gameObject.SetActive(false);
            }
        }
    }

    private void MachineStartedFalse()
    {
        anim.SetBool("MachineStarted", false);
    }

    private void Update()
    {
        if (Vector3.Distance(PlayerController.instance.gameObject.transform.position, transform.position) >= PlayerController.instance.Range)
        { return; }
        if(MachineUsed)
        { return; }

        issue = !Blade.activeInHierarchy;

        if (!issue)
        {
            if (PlayerController.instance.OnTargetGameObject == gameObject)
            {
                UIController.instance.infoText.text = "Use the lever to run this machine";
                UIController.instance.infoText.gameObject.SetActive(true);
            }
            else if(PlayerController.instance.OnTargetGameObject == Lever)
            {
                RunMachine();
            }
        }

        if (PlayerController.instance.OnTargetGameObject == gameObject)
        {
            PlaceCrate();
            BladeManage();
        }
    }

    private void DestroyCrate()
    { 
        if(!Crate.activeInHierarchy)
        { return; }
        MachineUsed = true;
        Spirit.SetActive(true);
        Destroy(Crate);
    }
}
