using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Ch4P1 : MonoBehaviour
{
    [Header("DigHere")]
    [SerializeField] bool isDigHere;

    [Header("Flowers")]
    [SerializeField] bool isFlower;

    [Header("Tomb")]
    [SerializeField] bool isTomb;
    [SerializeField] Doors TombDoor;
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
            if (isDigHere)
            {
                if (PlayerController.instance.GrabbedObjectName != "Shovel")
                {
                    UIController.instance.infoText.text = "I need shovel to dig it";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to dit it";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        Destroy(gameObject);
                    }
                }
            }

            else if(isFlower)
            {
                if (PlayerController.instance.GrabbedObjectName != "Potion")
                {
                    UIController.instance.infoText.text = "I can make potion magical using these flowers";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to make potion magical";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        PlayerController.instance.grabbingObject.GetComponent<GrabableObject>().ObjectName = "Magical potion";
                        PlayerController.instance.GrabbedObjectName = "Magical potion";
                        UIController.instance.grabbedObjectInfo.text = "You are grabbing the magical potion";
                        Destroy(gameObject);
                    }
                }
            }

            else if(isTomb)
            {
                if (PlayerController.instance.GrabbedObjectName != "Magical potion")
                {
                    UIController.instance.infoText.text = "I need magical potion to place here";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to use magical potion";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        Destroy(PlayerController.instance.grabbingObject.gameObject);
                        PlayerController.instance.grabbingObject = null;
                        PlayerController.instance.GrabbedObjectName = null;
                        UIController.instance.infoText.gameObject.SetActive(false);
                        UIController.instance.grabbedObjectInfo.gameObject.SetActive(false);
                        TombDoor.anyIssue = false;
                        Destroy(GetComponent<Ch4P1>());
                    }
                }
            }
        }
    }
}
