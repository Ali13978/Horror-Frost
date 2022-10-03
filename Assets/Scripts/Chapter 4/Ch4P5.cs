using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Ch4P5 : MonoBehaviour
{
    [Header("BreakAbleTree")]
    [SerializeField] bool isTree;
    [SerializeField] GameObject Logs;

    [Header("Poison")]
    [SerializeField] bool isPoison;
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
            if (isTree)
            {
                if (PlayerController.instance.GrabbedObjectName != "Axe")
                {
                    UIController.instance.ObjectiveText.text = "Find and use axe to cut trees";
                    UIController.instance.ObjectiveText.gameObject.SetActive(true);
                    UIController.instance.infoText.text = "I need axe to cut the logs";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to get logs";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        UIController.instance.ObjectiveText.gameObject.SetActive(false);
                        Logs.SetActive(true);
                        Destroy(gameObject);
                    }
                }
            }

            else if(isPoison)
            {
                if (PlayerController.instance.GrabbedObjectName != "Logs")
                {
                    UIController.instance.ObjectiveText.text = "Find logs and use poison to make it poison";
                    UIController.instance.ObjectiveText.gameObject.SetActive(true);
                    UIController.instance.infoText.text = "Poison can be placed on the log";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to poison the logs";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        UIController.instance.ObjectiveText.gameObject.SetActive(true);
                        PlayerController.instance.GrabbedObjectName = "PoisonedLog";
                        PlayerController.instance.grabbingObject.GetComponent<GrabableObject>().ObjectName = "PoisonedLog";
                        UIController.instance.grabbedObjectInfo.text = "You are grabbing poisoned log";
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
