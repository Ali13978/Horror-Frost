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
                    UIController.instance.infoText.text = "I need rod to to move this stone";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to move stone";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        GetComponent<Animator>().SetTrigger("Move");
                    }
                }
            }

            else if(isSmallStone)
            {
                if (PlayerController.instance.GrabbedObjectName != "PickAxe")
                {
                    UIController.instance.infoText.text = "I need something to break this rod";
                    UIController.instance.infoText.gameObject.SetActive(true);
                }
                else
                {
                    UIController.instance.infoText.text = "Press E to break stone";
                    UIController.instance.infoText.gameObject.SetActive(true);
                    if (CrossPlatformInputManager.GetButtonDown("UseButton"))
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
