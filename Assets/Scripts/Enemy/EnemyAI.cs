using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Animator Anim;
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float Range = 9f;
    [SerializeField] float turnSpeed = 5f;
    float distanceToTarget = Mathf.Infinity;

    Vector3 newPos;
    NavMeshAgent navMeshAgent;

    bool isProvoked = false;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        newPos = RandomNavSphere(transform.position, 50, -1);
        navMeshAgent.SetDestination(newPos);
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if(!isProvoked)
        {
            if (Vector3.Distance(newPos , transform.position) <= navMeshAgent.stoppingDistance)
            {
                newPos = RandomNavSphere(transform.position, 50, -1);
                navMeshAgent.SetDestination(newPos);
                Anim.SetTrigger("move");
            }
        }

        if(isProvoked)
        {
            EngageTarget();
        }

        else if(distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }

    }

    private void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget > navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        else if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        if (distanceToTarget > Range)
        {
            isProvoked = false;
            newPos = RandomNavSphere(transform.position, 50, -1);
            navMeshAgent.SetDestination(newPos);
            return;
        }
        Anim.SetBool("attack", false);
        Anim.SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        Anim.SetBool("attack", true);
        Debug.Log(name + " has seeked player and destroying it");
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion LookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, LookRotation, Time.deltaTime * turnSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
