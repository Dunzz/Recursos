using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 10f; 
    public float attackRange = 2f; 
    public float attackCooldown = 2f; 
    private NavMeshAgent agent;
    private float lastAttackTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            agent.SetDestination(player.position);

            if (distanceToPlayer <= attackRange)
            {
                agent.isStopped = true;

                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    Attack();
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }

    void Attack()
    {
        Debug.Log("El enemigo ataca al jugador.");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
