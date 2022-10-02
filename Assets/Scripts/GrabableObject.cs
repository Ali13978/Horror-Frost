using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObject : MonoBehaviour
{
    [SerializeField] public string ObjectName;

    [SerializeField] string GrabInfo;

    [HideInInspector] public bool isGrabbed = false;
    [HideInInspector] public bool isAttached = false;

    public void ShowGrabInfo()
    {
        if (!isGrabbed)
        {
            UIController.instance.infoText.text = GrabInfo;
            UIController.instance.infoText.gameObject.SetActive(true);
        }
    }

    public void GrabObject()
    {
        if (PlayerController.instance.grabbingObject == null && PlayerController.instance.GrabbedObjectName == null)
        {
            PlayerController.instance.gameObject.GetComponent<AudioSource>().clip = GetComponent<AudiosManager>().clips[1];
            PlayerController.instance.GetComponent<AudioSource>().Play();
            PlayerController.instance.grabbingObject = gameObject;
            PlayerController.instance.GrabbedObjectName = ObjectName;

            UIController.instance.grabbedObjectInfo.text = "You are grabbing  " + ObjectName;
            UIController.instance.grabbedObjectInfo.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            UIController.instance.errorText.gameObject.SetActive(true);
            UIController.instance.errorText.text = "First Throw the Previous item";
            StartCoroutine(TurnOffError());
        }
    }

    private IEnumerator TurnOffError()
    {
        yield return new WaitForSeconds(2);
        UIController.instance.errorText.gameObject.SetActive(false);
    }
}
