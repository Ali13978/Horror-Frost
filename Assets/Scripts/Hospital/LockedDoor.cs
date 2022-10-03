using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] GameObject planks;
    [SerializeField] GameObject Collider;

    [SerializeField] bool isLockedDoor;
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(PlayerController.instance.gameObject.transform.position, transform.position) >= 6)
        { return; }
        if (planks == null)
        { return; }

        if(PlayerController.instance.OnTargetGameObject == Collider)
        {
            if(PlayerController.instance.GrabbedObjectName != "crowbar")
            {
                if (isLockedDoor)
                {
                    UIController.instance.ObjectiveText.text = "Find and use crowbar to remove these planks";
                    UIController.instance.ObjectiveText.gameObject.SetActive(true);
                }
                UIController.instance.infoText.text = "I need something to remove these planks";
                UIController.instance.infoText.gameObject.SetActive(true);
            }
            else
            {
                UIController.instance.infoText.text = "Press E to remove planks";
                UIController.instance.infoText.gameObject.SetActive(true);
                if(CrossPlatformInputManager.GetButtonDown("UseButton"))
                {
                    if (isLockedDoor)
                    {
                        UIController.instance.ObjectiveText.gameObject.SetActive(false);
                    }
                    Destroy(planks.gameObject);
                    Collider.GetComponent<Doors>().anyIssue = false;
                    Destroy(gameObject.GetComponent<LockedDoor>());
                }
            }
        }
    }
}
