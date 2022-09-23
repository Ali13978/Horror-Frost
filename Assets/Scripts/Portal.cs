using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Portal : MonoBehaviour
{
    
    void Update()
    {
        if (Vector3.Distance(PlayerController.instance.gameObject.transform.position, transform.position) >= PlayerController.instance.Range)
        { return; }

        if(PlayerController.instance.OnTargetGameObject != gameObject)
        { return; }
        
        if(LevelManager.instance.CollectedSpirits == LevelManager.instance.ReqSpirits)
        {
            UIController.instance.infoText.text = "Press E to enter portal";
            UIController.instance.infoText.gameObject.SetActive(true);
            if(CrossPlatformInputManager.GetButtonDown("UseButton"))
            {
                UIController.instance.LoadingPannel.SetActive(true);
            }
        }

        else
        {
            UIController.instance.infoText.text = "I need more spirits to enter portal";
            UIController.instance.infoText.gameObject.SetActive(true);
        }
    }
}