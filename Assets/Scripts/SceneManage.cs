using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    [SerializeField] GameObject LoadingScreen;

    [Header("NewGameButton")]
    [SerializeField] GameObject newGameBg;
    [SerializeField] GameObject newGameEffect;
    [SerializeField] GameObject newGameContinue;

    [Header("NewgameContinueButton")]
    [SerializeField] GameObject NextScreen;

    [Header("AudioOn/Off")]
    [SerializeField] Image image;
    [SerializeField] Sprite SoundOnIcon;
    [SerializeField] Sprite SoundOffIcon;
    [SerializeField] AudioMixer mixer;
    bool AudioOn;

    public void continueGame()
    {
        LoadingScreen.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void newGame()
    {
        StartCoroutine(newGameEffectCoroutine());
    }

    public void newGameContinueButton()
    {
        NextScreen.SetActive(true);
    }

    public void ContinueScreenContinueButton()
    {
        PlayerPrefs.DeleteKey("CurrentChapter");
        LoadingScreen.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    private IEnumerator newGameEffectCoroutine()
    {
        newGameBg.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        newGameEffect.SetActive(true);
        StartCoroutine(newGameContinueCoroutine());
    }

    private IEnumerator newGameContinueCoroutine()
    {
        yield return new WaitForSeconds(1);
        newGameContinue.SetActive(true);
    }
    
    public void AudioOnOff()
    {
        if (AudioOn)
        {
            mixer.SetFloat("Master", -80);
            image.sprite = SoundOffIcon;
            AudioOn = false;
        }
        else
        {
            mixer.SetFloat("Master", 0);
            image.sprite = SoundOnIcon;
            AudioOn = true;
        }
    }

}
