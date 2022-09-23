using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [SerializeField] public TMP_Text NoOfSpirits;
    [SerializeField] public GameObject LoadingPannel;
    [SerializeField] public TMP_Text LivesText;

    private void Start()
    {
        LoadingPannel.SetActive(false);
    }
}
