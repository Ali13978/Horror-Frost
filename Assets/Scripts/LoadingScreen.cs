using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] TMP_Text tiptext;
    [SerializeField] List<string> tips;

    [SerializeField] float timerToLoadLevel;

    [SerializeField] float timerToChangeTip;
    private float CurrentTimer;

    private bool TimerStarted = false;

    int SceneNumber;

    private void Awake()
    {
        CurrentTimer = timerToChangeTip;
    }

    public void LoadingOn(int LoadSceneNumber)
    {
        SceneNumber = LoadSceneNumber;
        int index = Random.Range(0, tips.Count);
        tiptext.text = tips[index];

        TimerStarted = true;
    }

    private void Update()
    {
        if(TimerStarted)
        {
            TimerToChangeTip();
            TimerToLoadLevel();
        }
    }

    private void TimerToLoadLevel()
    {
        timerToLoadLevel -= Time.deltaTime;

        if(timerToLoadLevel <= 0)
        {
            SceneManager.LoadScene(SceneNumber);
        }
    }

    private void TimerToChangeTip()
    {
        CurrentTimer -= Time.deltaTime;

        if(CurrentTimer <= 0)
        {
            int index = Random.Range(0, tips.Count);
            tiptext.text = tips[index];
            CurrentTimer = timerToChangeTip;
        }
    }
}
