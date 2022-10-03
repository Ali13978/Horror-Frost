using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

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

                if(PlayerPrefs.GetInt("CurrentChapter") >= 5)
                {
                    SceneManager.LoadScene(2);
                }

                if(PlayerPrefs.GetInt("CurrentChapter") == 4)
                {
                    PlayerController.instance.TimerStarted = false;
                    PlayerController.instance.TimeCounter = PlayerController.instance.StartTimer;
                    UIController.instance.timerText.gameObject.SetActive(false);
                }

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

                for (int i = 0; i <= UIController.instance.ChapterScreens.Count; i++)
                {
                    if (i == (PlayerPrefs.GetInt("CurrentChapter") - 1))
                    {
                        UIController.instance.ChapterScreens[i].SetActive(true);
                    }
                }
            }
        }

        else
        {
            UIController.instance.infoText.text = "I need more spirits to enter portal";
            UIController.instance.infoText.gameObject.SetActive(true);
        }
    }
}