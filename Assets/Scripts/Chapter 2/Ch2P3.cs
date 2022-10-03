using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Ch2P3 : MonoBehaviour
{
    [Header("PlaceLadder")]
    [SerializeField] bool PlaceLadderObj;
    [SerializeField] GameObject Ladder;

    [Header("Pumpkin")]
    [SerializeField] bool isPumpkin;
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
            if(PlaceLadderObj)
            {
                if (PlayerController.instance.GrabbedObjectName != "Ladder")
                {
                    UIController.instance.ObjectiveText.text = "Find and use ladder at the side of bridge to go down";
                    UIController.instance.ObjectiveText.gameObject.SetActive(true);
                    UIController.instance.infoText.text = "I need something to go down";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to place ladder";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        UIController.instance.ObjectiveText.gameObject.SetActive(false);
                        Ladder.SetActive(true);
                        Destroy(PlayerController.instance.grabbingObject.gameObject);
                        PlayerController.instance.grabbingObject = null;
                        PlayerController.instance.GrabbedObjectName = null;
                        UIController.instance.infoText.gameObject.SetActive(false);
                        UIController.instance.grabbedObjectInfo.gameObject.SetActive(false);
                        Destroy(gameObject);
                    }
                }
            }

            else if(isPumpkin)
            {
                if (PlayerController.instance.GrabbedObjectName != "Knife")
                {
                    UIController.instance.ObjectiveText.text = "Find and use knife to cut the pumpkin";
                    UIController.instance.ObjectiveText.gameObject.SetActive(true);
                    UIController.instance.infoText.text = "I need something to cut the pumpkin";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to cut the pumpkin";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        UIController.instance.ObjectiveText.gameObject.SetActive(false);
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
