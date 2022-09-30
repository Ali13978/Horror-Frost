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
                PlayerPrefs.SetInt("CurrentChapter", (PlayerPrefs.GetInt("CurrentChapter") + 1));
                LevelManager.instance.ResetCollectedSpirits();
                if (PlayerController.instance.GrabbedObjectName != null)
                {
                    Destroy(PlayerController.instance.grabbingObject.gameObject);
                    PlayerController.instance.grabbingObject = null;
                    PlayerController.instance.GrabbedObjectName = null;
                    UIController.instance.infoText.gameObject.SetActive(false);
                    UIController.instance.grabbedObjectInfo.gameObject.SetActive(false);
                }
                LevelManager.instance.Die();
            }
        }

        else
        {
            UIController.instance.infoText.text = "I need more spirits to enter portal";
            UIController.instance.infoText.gameObject.SetActive(true);
        }
    }
}