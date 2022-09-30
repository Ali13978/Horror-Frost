using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] public TMP_Text infoText;
    [SerializeField] public TMP_Text grabbedObjectInfo;
    [SerializeField] public TMP_Text errorText;

    [SerializeField] public GameObject PasscodeScreen;
    
    [SerializeField] public GameObject LoadingPannel;
    [SerializeField] public TMP_Text LoadingPannelText;
    [SerializeField] public TMP_Text LivesText;

    [SerializeField] public TMP_Text timerText;

    [SerializeField] List<GameObject> cardsInUI;
    [SerializeField] List<Sprite> cardsSprites;

    private void Start()
    {
        LoadingPannel.SetActive(false);
    }

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
}
