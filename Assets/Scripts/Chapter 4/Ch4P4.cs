using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Ch4P4 : MonoBehaviour
{
    [Header("Grave")]
    [SerializeField] bool isGrave;

    [Header("Statue")]
    [SerializeField] bool isStatue;
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
            if (isGrave)
            {
                if (PlayerController.instance.GrabbedObjectName != "Shovel")
                {
                    UIController.instance.infoText.text = "I need something to dig it";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to dig grave";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        Destroy(gameObject);
                    }
                }
            }

            else if (isStatue)
            {
                if (PlayerController.instance.GrabbedObjectName != "PickAxe")
                {
                    UIController.instance.infoText.text = "I need pickaxe to destroy it";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to destroy statue";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        Spirit.SetActive(true);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
