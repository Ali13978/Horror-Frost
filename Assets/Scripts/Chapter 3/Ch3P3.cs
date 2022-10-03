using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Ch3P3 : MonoBehaviour
{
    [Header("Box")]
    [SerializeField] bool isBox;
    [SerializeField] GameObject Lock;
    [SerializeField] GameObject Spirit;

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
                if (PlayerController.instance.GrabbedObjectName != "Hatchet")
                {
                    UIController.instance.ObjectiveText.text = "Find and use hatchet to open crate";
                    UIController.instance.ObjectiveText.gameObject.SetActive(true);
                    UIController.instance.infoText.text = "I need hatchet to open it";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to open it";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        UIController.instance.ObjectiveText.gameObject.SetActive(false);
                        Spirit.SetActive(true);
                        Destroy(Lock);
                    }
                }
            }

            else if (isAntiVenom)
            {
                UIController.instance.infoText.text = "Press E to use venom";
                UIController.instance.infoText.gameObject.SetActive(true);
                if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                {
                    PlayerController.instance.VenomDrinked = true;
                }
            }
        }
    }
}
