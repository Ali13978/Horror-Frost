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
                LevelManager.instance.TakeDamage();
            }
            UIController.instance.LoadingPannelText.text = "Anti venom may help against these frogs";
        }
        else if(isScorpian)
        {
            if (PlayerController.instance.GrabbedObjectName != "PoisonedLog")
            {
                LevelManager.instance.TakeDamage();
            }
            UIController.instance.LoadingPannelText.text = "I should make poisoned logs against the spiders";
        }
        else
        {
            LevelManager.instance.TakeDamage();
        }
    }

    private void AttackOff()
    {
        GetComponent<Animator>().SetBool("attack", false);
    }

}