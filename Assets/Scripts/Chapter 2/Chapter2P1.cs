using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Chapter2P1 : MonoBehaviour
{
    [Header("For Car Door")]
    [SerializeField] bool isCarDoor;
    [Space(10)]
    [Header("For Car Button")]
    [SerializeField] bool isButton;
    [SerializeField] GameObject BackOfCar;
    [Header("For Bear Trap")]
    [SerializeField] bool isBrearTrap;
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
            if(isCarDoor)
            {
                if (PlayerController.instance.GrabbedObjectName != "Stone")
                {
                    UIController.instance.infoText.text = "I need stone to destroy this door";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to remove door";
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

            else if(isButton)
            {
                UIController.instance.infoText.text = "Press E to open back of car";
                UIController.instance.infoText.gameObject.SetActive(true);

                if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                {
                    BackOfCar.GetComponent<Animator>().SetTrigger("Open");
                    Destroy(gameObject.GetComponent<Chapter2P1>());
                }
            }

            else if (isBrearTrap)
            {
                if (PlayerController.instance.GrabbedObjectName != "Container")
                {
                    UIController.instance.infoText.text = "I need barrel to block this bear trap";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to remove trap";
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
        }
    }
}
