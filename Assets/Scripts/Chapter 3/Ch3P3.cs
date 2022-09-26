using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Ch3P3 : MonoBehaviour
{
    [Header("Box")]
    [SerializeField] bool isBox;
    [SerializeField] GameObject Lock;
    bool Locked = true;

    [Header("Anti-Venom")]
    [SerializeField] bool isAntiVenom;

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
            if (isBox)
            {
                if (Locked)
                {
                    if (PlayerController.instance.GrabbedObjectName != "Hatchet")
                    {
                        UIController.instance.infoText.text = "It's lock is rusted need hatchet";
                        UIController.instance.infoText.gameObject.SetActive(true);
                    }
                    else
                    {
                        UIController.instance.infoText.text = "Press E to break lock";
                        UIController.instance.infoText.gameObject.SetActive(true);
                        if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                        {
                            Destroy(Lock);
                            Locked = false;
                        }
                    }
                }

                else
                {
                    UIController.instance.infoText.text = "Press E to open locker";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        Destroy(gameObject);
                        //Place the locker open system here
                    }
                }
            }

            else if(isAntiVenom)
            {
                UIController.instance.infoText.text = "Press E to use venom";
                UIController.instance.infoText.gameObject.SetActive(true);
                if(CrossPlatformInputManager.GetButtonDown("UseButton"))
                {
                    PlayerController.instance.VenomDrinked = true;
                }
            }
        }
    }
}
