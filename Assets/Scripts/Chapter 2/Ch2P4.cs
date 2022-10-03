using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Ch2P4 : MonoBehaviour
{
    [Header("Large Stone")]
    [SerializeField] bool isLargeStone;

    [Header("Small Stone")]
    [SerializeField] bool isSmallStone;
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
            if(isLargeStone)
            {
                if (PlayerController.instance.GrabbedObjectName != "Rod")
                {
                    UIController.instance.ObjectiveText.text = "Find and use rod to move the stone";
                    UIController.instance.ObjectiveText.gameObject.SetActive(true);
                    UIController.instance.infoText.text = "I need rod to to move this stone";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to move stone";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        UIController.instance.ObjectiveText.gameObject.SetActive(false);
                        Destroy(PlayerController.instance.grabbingObject.gameObject);
                        PlayerController.instance.grabbingObject = null;
                        PlayerController.instance.GrabbedObjectName = null;
                        UIController.instance.infoText.gameObject.SetActive(false);
                        UIController.instance.grabbedObjectInfo.gameObject.SetActive(false);
                        GetComponent<Animator>().SetTrigger("Move");
                    }
                }
            }

            else if(isSmallStone)
            {
                if (PlayerController.instance.GrabbedObjectName != "PickAxe")
                {
                    UIController.instance.ObjectiveText.text = "Find and use pickaxe to break small rock";
                    UIController.instance.ObjectiveText.gameObject.SetActive(true);
                    UIController.instance.infoText.text = "I need something to break this rock";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to break stone";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        UIController.instance.ObjectiveText.gameObject.SetActive(false);
                        Spirit.SetActive(true);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
