using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;



    private void Awake()
    {
        instance = this;
    }
    
    [HideInInspector] public int CollectedSpirits = 0;
    [SerializeField] public int ReqSpirits;
    [SerializeField] public int Lives = 3;

    [SerializeField] int DefaultChapter;

    [SerializeField] List<GameObject> ChapterSpawnPoints;

    private void Start()
    {
        UIController.instance.LivesText.text = Lives.ToString();
        if(!PlayerPrefs.HasKey("CurrentChapter"))
        {
            PlayerPrefs.SetInt("CurrentChapter", DefaultChapter);
        }

        for(int i = 0; i<= ChapterSpawnPoints.Count; i++)
        {
            if(i == (PlayerPrefs.GetInt("CurrentChapter") - 1))
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
            UIController.instance.LivesText.text = Lives.ToString();
            Die();
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void Die()
    {
        PlayerController.instance.transform.position = new Vector3(50, -50, 50);
        PlayerController.instance.gameObject.SetActive(false);
        UIController.instance.LoadingPannel.SetActive(true);
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i <= ChapterSpawnPoints.Count; i++)
        {
            if (i == (PlayerPrefs.GetInt("CurrentChapter") - 1))
            {
                PlayerController.instance.gameObject.transform.position = ChapterSpawnPoints[i].transform.position;
            }
        }

        UIController.instance.LoadingPannel.SetActive(false);
        UIController.instance.LoadingPannelText.text = "Loading...";
        PlayerController.instance.gameObject.SetActive(true);
        PlayerController.instance.TakingDamage = false;
    }

    public void ResetCollectedSpirits()
    {
        CollectedSpirits = 0;
        UIController.instance.NoOfSpirits.text = CollectedSpirits.ToString();
    }
}
