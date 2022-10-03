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
    [SerializeField] float WanderRange = 50;
    [SerializeField] float StartTimer;
    float Timer;

    float distanceToTarget = Mathf.Infinity;

    Vector3 newPos;
    NavMeshAgent navMeshAgent;

    bool isProvoked = false;
    [Header("Zombie")]
    [SerializeField] bool isZombie;
    [SerializeField] List<AudioClip> Audios;
    AudioSource audioSource;

    void Start()
    {
        if(isZombie)
        {
            audioSource = GetComponent<AudioSource>();
        }
        Timer = StartTimer;
        navMeshAgent = GetComponent<NavMeshAgent>();
        newPos = RandomNavSphere(transform.position, WanderRange, -1);
        navMeshAgent.SetDestination(newPos);
        Anim.SetTrigger("move");
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (!isProvoked)
        {
            if (isZombie)
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
            Timer -= Time.deltaTime;
            if (Vector3.Distance(newPos, transform.position) <= navMeshAgent.stoppingDistance || Timer <= 0)
            {
                Timer = StartTimer;
                newPos = RandomNavSphere(transform.position, WanderRange, -1);
                navMeshAgent.SetDestination(newPos);
                Anim.SetTrigger("move");
            }
        }

        if (isProvoked)
        {
            if (!PlayerController.instance.inSafeArea)
            {
                EngageTarget();
            }

            else
            {
                chaseRange = 0.1f;
                isProvoked = false;
                StartCoroutine(ChaseRangeBack());
            }
        }

        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }

    private IEnumerator ChaseRangeBack()
    {
        yield return new WaitForSeconds(7);
        chaseRange = 20;
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
            if (isZombie)
            {
                if (!Anim.GetBool("attack"))
                {
                    if (audioSource.isPlaying)
                    {
                        audioSource.Stop();
                        audioSource.PlayOneShot(Audios[1]);
                    }
                }
            }

            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        if (distanceToTarget > Range)
        {
            UIController.instance.ZombieChaseInfo.SetActive(false);

            isProvoked = false;
            newPos = RandomNavSphere(transform.position, WanderRange, -1);
            navMeshAgent.SetDestination(newPos);
            return;
        }
        Anim.SetBool("attack", false);
        Anim.SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
        if(isZombie)
        {
            UIController.instance.ZombieChaseInfo.SetActive(true);

            if(!audioSource.isPlaying)
            {
                audioSource.clip = Audios[0];
                audioSource.Play();
            }
        }
    }

    private void AttackTarget()
    {
        Anim.SetBool("attack", true);
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
        Gizmos.DrawWireSphere(transform.position, WanderRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Range );
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
