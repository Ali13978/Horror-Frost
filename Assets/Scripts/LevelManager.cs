using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    [SerializeField] bool ResetLevel;
    [SerializeField] GameObject MiniMapCam;
    [SerializeField] GameObject MainCam;
    
    public int CollectedSpirits = 0;
    [SerializeField] public int ReqSpirits;
    [SerializeField] public int Lives = 3;

    [SerializeField] int DefaultChapter;

    [SerializeField] List<GameObject> ChapterSpawnPoints;

    [SerializeField] GameObject BgAudioObj;

    private void Awake()
    {
        MiniMapCam.GetComponent<AudioListener>().enabled = false;
        instance = this;

        if (ResetLevel)
        {
            PlayerPrefs.DeleteKey("CurrentChapter");
        }

        if (!PlayerPrefs.HasKey("CurrentChapter"))
        {
            PlayerPrefs.SetInt("CurrentChapter", DefaultChapter);
        }

    }
    
    
    private void Start()
    {
        UIController.instance.LivesText.text = "X " + Lives.ToString();

        for (int i = 0; i <= ChapterSpawnPoints.Count; i++)
        {
            if (i == (PlayerPrefs.GetInt("CurrentChapter") - 1))
            {
                PlayerController.instance.gameObject.transform.position = ChapterSpawnPoints[i].transform.position;
            }
        }
    }

    public void TakeDamage()
    {
        Lives--; 

        if(Lives <= 0)
        {
            RestartLevel();
        }

        else
        {
            UIController.instance.LivesText.text = "X " + Lives.ToString();
            Die();
        }
    }

    private void RestartLevel()
    {
        UIController.instance.DiedScreen.SetActive(true);
    }

    public void Die()
    {
        PlayerController.instance.transform.position = new Vector3(50, -50, 50);
        PlayerController.instance.gameObject.SetActive(false);
        MiniMapCam.GetComponent<AudioListener>().enabled = true;
    }

    public void Respawn()
    {

        for (int i = 0; i <= ChapterSpawnPoints.Count; i++)
        {
            if (i == (PlayerPrefs.GetInt("CurrentChapter") - 1))
            {
                BgAudioObj.GetComponent<AudiosManager>().PlayBgSound();
                PlayerController.instance.gameObject.transform.position = ChapterSpawnPoints[i].transform.position;
            }
        }

        foreach(GameObject i in UIController.instance.ChapterScreens)
        {
            i.SetActive(false);
        }

        UIController.instance.FrogsOutScreen.SetActive(false);
        UIController.instance.SpidersOutScreen.SetActive(false);
        UIController.instance.LifeLostScreen.SetActive(false);
        UIController.instance.LoadingScreen.SetActive(false);

        MiniMapCam.GetComponent<AudioListener>().enabled = false;
        PlayerController.instance.gameObject.SetActive(true);
        PlayerController.instance.TakingDamage = false;
    }

    public void ResetCollectedSpirits()
    {
        CollectedSpirits = 0;
        UIController.instance.ResetSpirits();
    }
}
