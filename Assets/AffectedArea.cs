using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectedArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == PlayerController.instance.gameObject)
        {
            LevelManager.instance.TakeDamage();
        }
    }
}
