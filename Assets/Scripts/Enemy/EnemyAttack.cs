using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Transform target;

    private void AttackHitEvent()
    {
        if (target == null) return;
        LevelManager.instance.TakeDamage();
    }
}