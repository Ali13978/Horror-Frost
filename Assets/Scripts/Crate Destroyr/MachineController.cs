using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour
{
    Animator anim;

    [SerializeField] GameObject Lever;
    [SerializeField] GameObject Blade;
    [SerializeField] GameObject GrabbingBlade;
    [SerializeField] string BladeName;

    [SerializeField] GameObject Crate;

    bool issue = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void RunMachine()
    {
        UIController.instance.infoText.text = "Press E to run machine";
        UIController.instance.infoText.gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E))
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
            UIController.instance.infoText.text = "I need blade to place here in machine";
            UIController.instance.infoText.gameObject.SetActive(true);
        }
        else if (PlayerController.instance.GrabbedObjectName == BladeName && PlayerController.instance.grabbingObject == GrabbingBlade)
        {
            UIController.instance.infoText.text = "Press E to place blade";
            UIController.instance.infoText.gameObject.SetActive(true);
            if (Input.GetButtonDown("UseButton"))
            {
                Blade.SetActive(true);
                Destroy(GrabbingBlade);
                PlayerController.instance.grabbingObject = null;
                PlayerController.instance.GrabbedObjectName = null;
                UIController.instance.infoText.gameObject.SetActive(false);
                UIController.instance.grabbedObjectInfo.gameObject.SetActive(false);
            }
        }
        else
        {
            UIController.instance.infoText.text = "I need something else to place here";
            UIController.instance.infoText.gameObject.SetActive(true);
        }
    }

    public void PlaceCrate()
    {
        if (Crate.activeInHierarchy)
        { return; }

        if (PlayerController.instance.GrabbedObjectName == null && PlayerController.instance.grabbingObject == null)
        {
            UIController.instance.infoText.text = "I need something needed to be destroyed here";
            UIController.instance.infoText.gameObject.SetActive(true);
        }

        else if (PlayerController.instance.GrabbedObjectName != "crate")
        {
            UIController.instance.infoText.text = "I need something else to place here";
            UIController.instance.infoText.gameObject.SetActive(true);
        }
        else if (PlayerController.instance.GrabbedObjectName == "crate")
        {
            UIController.instance.infoText.text = "Press E to place crate";
            UIController.instance.infoText.gameObject.SetActive(true);
            if (Input.GetButtonDown("UseButton"))
            {
                Crate.SetActive(true);
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
}
