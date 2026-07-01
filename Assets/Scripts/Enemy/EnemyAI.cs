using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("State Configuration")]
    [SerializeField]
    private EnemyStateSO initialState;

    // Reference fields for states this specific enemy is allowed to use
    public EnemyStateSO idleState;
    public EnemyStateSO patrolState;
    public EnemyStateSO chaseState;
    public EnemyStateSO attackState;
    public LayerMask playerLayer;
    private Animator animator;

    private EnemyStateSO _currentState;

    private float attackRange = 1.5f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (initialState != null)
        {
            TransitionToState(initialState);
        }
    }

    private void FixedUpdate()
    {
        _currentState?.UpdateState(this);
        Debug.Log($"Current State: {_currentState?.GetType().Name}");
    }

    public void TransitionToState(EnemyStateSO newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState?.EnterState(this);
    }

    // Helper methods called by the ScriptableObjects
    public void Move(
        float speed
    ) { /* Navigation logic */
    }

    public void AttackTarget()
    {
        animator.SetTrigger("attack");
    }

    public bool IsTargetInSight()
    {
        return true; /* Check distance/line of sight */
    }

    public bool IsTargetInAttackRange()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(
            transform.position,
            attackRange,
            playerLayer
        );
        if (playerCollider != null)
        {
            Debug.Log("Player Detected!");
            return true;
        }
        Debug.Log("Player NOT found!");
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
