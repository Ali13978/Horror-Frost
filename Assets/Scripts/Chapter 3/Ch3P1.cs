using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Ch3P1 : MonoBehaviour
{
    [Header("FactoryDoorStopper")]
    [SerializeField] bool isFactoryDoor;

    [Header("Locker")]
    [SerializeField] bool isLockerDoor;
    [SerializeField] GameObject Spirit;
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
            if (isFactoryDoor)
            {
                if (PlayerController.instance.GrabbedObjectName != "Spray")
                {
                    UIController.instance.infoText.text = "I need spray to remove gas smell";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to use spray";
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

            else if(isLockerDoor)
            {
                if (PlayerController.instance.GrabbedObjectName != "Key")
                {
                    UIController.instance.infoText.text = "I need key to open locker";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to use key";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        Destroy(PlayerController.instance.grabbingObject.gameObject);
                        PlayerController.instance.grabbingObject = null;
                        PlayerController.instance.GrabbedObjectName = null;
                        UIController.instance.infoText.gameObject.SetActive(false);
                        UIController.instance.grabbedObjectInfo.gameObject.SetActive(false);
                        Spirit.SetActive(true);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
