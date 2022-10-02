using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] bool isFrog;
    [SerializeField] bool isScorpian;

    private void AttackHitEvent()
    {
        if (PlayerController.instance.TakingDamage) return;
        if (target == null) return;
        PlayerController.instance.TakingDamage = true;
        if (isFrog)
        {
            if (!PlayerController.instance.VenomDrinked)
            {
                UIController.instance.FrogsOutScreen.SetActive(true);
                LevelManager.instance.TakeDamage();
            }
        }
        else if(isScorpian)
        {
            if (PlayerController.instance.GrabbedObjectName != "PoisonedLog")
            {
                UIController.instance.SpidersOutScreen.SetActive(true);
                LevelManager.instance.TakeDamage();
            }
        }
        else
        {
            UIController.instance.LifeLostScreen.SetActive(true);
            LevelManager.instance.TakeDamage();
        }
    }

    private void AttackOff()
    {
        GetComponent<Animator>().SetBool("attack", false);
    }

}