using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] public GameObject LoadingScreen;

    [SerializeField] public TMP_Text infoText;
    [SerializeField] public TMP_Text grabbedObjectInfo;
    [SerializeField] public TMP_Text errorText;

    [SerializeField] public GameObject PasscodeScreen;
    
    //[SerializeField] public GameObject LoadingPannel;
    //[SerializeField] public TMP_Text LoadingPannelText;

    [SerializeField] public TMP_Text LivesText;

    [SerializeField] public TMP_Text timerText;

    [SerializeField] List<GameObject> cardsInUI;
    [SerializeField] List<Sprite> cardsSprites;
    
    [SerializeField] public GameObject FrogsOutScreen;
    [SerializeField] public GameObject SpidersOutScreen;
    [SerializeField] public GameObject LifeLostScreen;

    [SerializeField] public GameObject ExitWarningScreen;

    [SerializeField] public List<GameObject> ChapterScreens;

    [SerializeField] public GameObject DiedScreen;

    [SerializeField] public TMP_Text ObjectiveText;

    [SerializeField] public GameObject ZombieChaseInfo;

    public void CollectSpirit(int x)
    {
        for(int i = 0; i <= cardsInUI.Count; i++)
        {
            if(x == i)
            {
                cardsInUI[i].GetComponent<Image>().sprite = cardsSprites[(i + 1)];
            }
        }
    }

    public void ResetSpirits()
    {
        foreach(GameObject i in cardsInUI)
        {
            i.GetComponent<Image>().sprite = cardsSprites[0];
        }
    }

    public void ExitLevel()
    {
        Time.timeScale = 0;
        ExitWarningScreen.SetActive(true);
    }

    public void OutContinueButton()
    {
        LoadingScreen.SetActive(true);
        LoadingScreen.GetComponent<LoadingScreen>().LoadingOn(1);
        LevelManager.instance.Respawn();
    }

    public void OutExitButton()
    {
        UIController.instance.LoadingScreen.SetActive(false);
        ExitWarningScreen.SetActive(true);
    }

    public void ExitWarningNo()
    {
        ExitWarningScreen.SetActive(false);
        Time.timeScale = 1;
        LevelManager.instance.Respawn();
    }

    public void ExitWarningYes()
    {
        Time.timeScale = 1;
        LoadingScreen.SetActive(true);
        LoadingScreen.GetComponent<LoadingScreen>().LoadingOn(0);
    }

    public void PortalContinue()
    {
        LoadingScreen.SetActive(true);
        LoadingScreen.GetComponent<LoadingScreen>().LoadingOn(1);
        LevelManager.instance.Respawn();
    }

}
