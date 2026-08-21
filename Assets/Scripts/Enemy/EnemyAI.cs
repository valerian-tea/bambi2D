using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Settings")]
    private SpriteRenderer spriteRenderer;
    private GameObject player;

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
    public bool isAttacking = false;
    public GameObject attackHitbox;
    public Vector2 hitboxOffset = new Vector2(1f, 0f);

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (initialState != null)
        {
            TransitionToState(initialState);
        }
    }

    private void FixedUpdate()
    {
        Debug.Log("isattacking: " + isAttacking);
        if (!isAttacking)
        {
            FaceTarget(player?.transform);
        }
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
        isAttacking = true;
        AlignHitbox();
        Debug.Log("Enemy is attacking!");
        animator.SetTrigger("attack");
        animator.SetInteger("attackID", 0);
    }

    public void EndAttack()
    //animation event
    {
        Debug.Log("Attack animation ended.");
        isAttacking = false;
    }

    private void AlignHitbox()
    {
        if (attackHitbox != null)
        {
            Vector2 currentOffset = hitboxOffset;
            if (spriteRenderer.flipX)
                currentOffset.x *= -1f;
            attackHitbox.transform.localPosition = currentOffset;
        }
    }

    public void Idle()
    {
        Debug.Log("Enemy is idling.");
    }

    public bool CanAttack()
    {
        return attackTimer <= 0f;
    }

    public void TriggerAttack()
    {
        if (!CanAttack())
        {
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
            return true;
        }
        return false;
    }

    private void FaceTarget(Transform target)
    {
        if (target == null || spriteRenderer == null)
            return;

        bool targetIsLeft = target.position.x < transform.position.x;
        spriteRenderer.flipX = targetIsLeft;
    }
}
