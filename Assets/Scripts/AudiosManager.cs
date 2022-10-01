using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiosManager : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;

    [SerializeField] bool isBackGround;
    [SerializeField] List<AudioClip> BgClips;

    [SerializeField] bool isGrabableObj;
    [SerializeField] public List<AudioClip> clips;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        audioSource.spatialBlend = 1;
        
        if(isBackGround)
        {
            audioSource.spatialBlend = 0;
            PlayBgSound();
        }
    }

    public void PlayBgSound()
    {
        if (PlayerPrefs.GetInt("CurrentChapter") == 1)
        {
            audioSource.clip = BgClips[0];
            audioSource.Play();
        }

        else
        {
            audioSource.clip = BgClips[1];
            audioSource.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            audioSource.clip = clips[0];
            audioSource.Play();
        }
    }

}
