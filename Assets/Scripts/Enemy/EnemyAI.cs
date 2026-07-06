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

    [Header("Combat")]
    public float attackCooldown = 3.0f;
    public float attackRange = 1.5f;
    private float attackTimer = 0f;

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
        if (attackTimer > 0f)
            attackTimer -= Time.fixedDeltaTime;
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
        animator.SetBool("isAttacking", true);
    }

    public void Idle()
    {
        Debug.Log("Enemy is idling.");
        animator.SetBool("isAttacking", false);
    }

    public bool CanAttack()
    {
        return attackTimer <= 0f;
    }

    public void TriggerAttack()
    {
        if (!CanAttack())
        {
            animator.SetBool("isAttacking", false);
            return;
        }

        AttackTarget();
        attackTimer = attackCooldown;
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
}
