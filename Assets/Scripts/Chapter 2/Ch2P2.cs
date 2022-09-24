using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Ch2P2 : MonoBehaviour
{
    [Header("Mud")]
    [SerializeField] bool isMud;

    [Header("NailsBlocker")]
    [SerializeField] bool isNailsBlocker;
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
            if (isMud)
            {
                if (PlayerController.instance.GrabbedObjectName != "Shovel")
                {
                    UIController.instance.infoText.text = "I need shovel to dig it";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to dig";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        Destroy(gameObject);
                    }
                }
            }

            else if (isNailsBlocker)
            {
                if (PlayerController.instance.GrabbedObjectName != "Shoes")
                {
                    UIController.instance.infoText.text = "I need some shoes to enter here";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    GetComponent<MeshCollider>().isTrigger = true;
                }
            }
        }
    }
}
