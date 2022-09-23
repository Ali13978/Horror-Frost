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

    [SerializeField] GameObject PlayerRespawnPoint;
    [HideInInspector] public int CollectedSpirits = 0;
    [SerializeField] public int ReqSpirits;
    [SerializeField] public int Lives = 3;

    private void Start()
    {
        UIController.instance.LivesText.text = Lives.ToString();
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

    private void Die()
    {
        UIController.instance.LoadingPannel.SetActive(true);
        PlayerController.instance.transform.position = new Vector3(0, -50, 0);
        PlayerController.instance.gameObject.SetActive(false);
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2);
        PlayerController.instance.transform.position = PlayerRespawnPoint.transform.position;
        UIController.instance.LoadingPannel.SetActive(false);
        PlayerController.instance.gameObject.SetActive(true);
    }
}
