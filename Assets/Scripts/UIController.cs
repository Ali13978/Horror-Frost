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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
