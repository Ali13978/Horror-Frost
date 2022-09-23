using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Spirit : MonoBehaviour
{
    [SerializeField] GameObject GFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(PlayerController.instance.gameObject.transform.position, transform.position) >= PlayerController.instance.Range)
        { return; }

        if(PlayerController.instance.OnTargetGameObject != GFX)
        { return; }

        UIController.instance.infoText.text = "Press E to collect the spirit";
        UIController.instance.infoText.gameObject.SetActive(true);
        if(CrossPlatformInputManager.GetButtonDown("UseButton"))
        {
            CollectSpirit();
        }
    }

    private void CollectSpirit()
    {
        LevelManager.instance.CollectedSpirits++;
        UIController.instance.NoOfSpirits.text = LevelManager.instance.CollectedSpirits.ToString();
        Destroy(gameObject);
    }
}
