using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompleted : MonoBehaviour
{
    public void ContinueButtton()
    {
        PlayerPrefs.DeleteKey("CurrentChapter");
        SceneManager.LoadScene(0);
    }
}
