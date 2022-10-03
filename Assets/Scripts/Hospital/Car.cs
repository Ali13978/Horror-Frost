using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Car : MonoBehaviour
{
    [SerializeField] Animator Anim;
    [SerializeField] Animator garageAnim;
    [SerializeField] bool Issue = true;
    bool Started= false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Started)
        { return; };
        if (Vector3.Distance(PlayerController.instance.gameObject.transform.position, transform.position) >= PlayerController.instance.Range)
        { return; }

        if(!Issue)
        {
            if (PlayerController.instance.OnTargetGameObject == gameObject)
            {
                UIController.instance.infoText.text = "Press E to start car";
                UIController.instance.infoText.gameObject.SetActive(true);
                if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                {
                    Anim.SetBool("Start", true);
                    Started = true;
                }
            }
        }
        else
        {

            if (PlayerController.instance.GrabbedObjectName == null && PlayerController.instance.grabbingObject == null)
            {
                UIController.instance.ObjectiveText.text = "Find and fill engine oil in the car";
                UIController.instance.ObjectiveText.gameObject.SetActive(true);
                UIController.instance.infoText.text = "I need something to make this car working";
                UIController.instance.infoText.gameObject.SetActive(true);
            }

            else if (PlayerController.instance.GrabbedObjectName != "Engine Oil")
            {
                UIController.instance.ObjectiveText.text = "Find and fill engine oil in the car";
                UIController.instance.ObjectiveText.gameObject.SetActive(true);
                UIController.instance.infoText.text = "I need something else to use here";
                UIController.instance.infoText.gameObject.SetActive(true);
            }
            else if (PlayerController.instance.GrabbedObjectName == "Engine Oil")
            {
                UIController.instance.infoText.text = "Press E to fill engine oil";
                UIController.instance.infoText.gameObject.SetActive(true);
                if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                {
                    UIController.instance.ObjectiveText.gameObject.SetActive(false);
                    Issue = false;
                    Destroy(PlayerController.instance.grabbingObject.gameObject);
                    PlayerController.instance.grabbingObject = null;
                    PlayerController.instance.GrabbedObjectName = null;
                    UIController.instance.infoText.gameObject.SetActive(false);
                    UIController.instance.grabbedObjectInfo.gameObject.SetActive(false);
                }
            }
        }

    }

    private void OpenGarage()
    {
        garageAnim.SetBool("Start", true);
    }
}
