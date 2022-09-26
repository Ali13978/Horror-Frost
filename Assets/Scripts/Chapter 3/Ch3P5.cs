using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Ch3P5 : MonoBehaviour
{
    [Header("BridgeStopper")]
    [SerializeField] bool isBridgeStopper;
    [SerializeField] GameObject RepairedBridge;

    [Header("DestroyableTrees")]
    [SerializeField] bool isTree;
    [SerializeField] GameObject Logs;

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
            if (isBridgeStopper)
            {
                if (PlayerController.instance.GrabbedObjectName != "Logs")
                {
                    UIController.instance.infoText.text = "I need logs to repair bridge";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to repair bridge";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        Destroy(PlayerController.instance.grabbingObject.gameObject);
                        PlayerController.instance.grabbingObject = null;
                        PlayerController.instance.GrabbedObjectName = null;
                        UIController.instance.infoText.gameObject.SetActive(false);
                        UIController.instance.grabbedObjectInfo.gameObject.SetActive(false);
                        RepairedBridge.SetActive(true);
                        Destroy(gameObject);
                    }
                }
            }

            else if (isTree)
            {
                if (PlayerController.instance.GrabbedObjectName != "Axe")
                {
                    UIController.instance.infoText.text = "I need axe to cut the logs";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to get logs";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        Logs.SetActive(true);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
