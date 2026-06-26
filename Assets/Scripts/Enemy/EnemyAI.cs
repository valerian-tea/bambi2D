using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // State definitions
    public enum AIState
    {
        Patrol,
        Chase,
        Attack,
    }

    [Header("Current State")]
    public AIState currentState = AIState.Patrol;

    [Header("References")]
    public NavMeshAgent agent;
    public Transform player;

    [Header("AI Settings")]
    public float sightRange = 10f;
    public float attackRange = 2f;

    [Header("Patrol Settings")]
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    void Start()
    {
        // Automatically fetch NavMeshAgent if not assigned
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        // Find player by tag if not explicitly linked
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        GoToNextPatrolPoint();
    }

    void Update()
    {
        // Calculate distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // State Machine Switch Logic
        switch (currentState)
        {
            case AIState.Patrol:
                HandlePatrolState(distanceToPlayer);
                break;
            case AIState.Chase:
                HandleChaseState(distanceToPlayer);
                break;
            case AIState.Attack:
                HandleAttackState(distanceToPlayer);
                break;
        }
    }

    void HandlePatrolState(float distanceToPlayer)
    {
        // Behavior: Move from waypoint to waypoint
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }

        // Transition: Spot player
        if (distanceToPlayer <= sightRange)
        {
            currentState = AIState.Chase;
        }
    }

    void HandleChaseState(float distanceToPlayer)
    {
        // Behavior: Continuously track player position
        agent.SetDestination(player.position);

        // Transition: Player gets too close (Attack Range)
        if (distanceToPlayer <= attackRange)
        {
            currentState = AIState.Attack;
        }
        // Transition: Player escapes (Sight Range)
        else if (distanceToPlayer > sightRange)
        {
            currentState = AIState.Patrol;
            GoToNextPatrolPoint();
        }
    }

    void HandleAttackState(float distanceToPlayer)
    {
        // Behavior: Stop moving and face the target
        agent.SetDestination(transform.position);
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

        // Insert your combat/damage logic or trigger animation here
        Debug.Log("Attacking the Player!");

        // Transition: Player runs away out of attack range
        if (distanceToPlayer > attackRange)
        {
            currentState = AIState.Chase;
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    // Visualizes sight and attack fields in the Unity Editor viewport
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
