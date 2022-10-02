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
        LoadingScreen.SetActive(true);
        SceneManager.LoadScene(0);
    }

    public void OutContinueButton()
    {
        UIController.instance.LoadingScreen.SetActive(true);
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
        LevelManager.instance.Respawn();
    }

    public void ExitWarningYes()
    {
        LoadingScreen.SetActive(true);
        SceneManager.LoadScene(0);
    }

    public void PortalContinue()
    {
        UIController.instance.LoadingScreen.SetActive(true);
        LevelManager.instance.Respawn();
    }

}
