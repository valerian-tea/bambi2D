using UnityEngine;

[CreateAssetMenu(fileName = "ChaseState", menuName = "AI/States/Chase")]
public class ChaseStateSO : EnemyStateSO
{
    public float chaseSpeed = 3f;

    public override void EnterState(EnemyAI enemy)
    {
        Debug.Log("Entered Chase State");
    }

    public override void UpdateState(EnemyAI enemy)
    {
        // Chase movement logic using enemy.transform
        enemy.Move(chaseSpeed);

        // If the player is close enough, attack immediately.
        if (enemy.IsTargetInAttackRange())
        {
            if (enemy.attackState != null)
            {
                enemy.TransitionToState(enemy.attackState);
                return;
            }
        }

        // State Transition Check for chasing if the player is visible.
        if (enemy.IsTargetInSight())
        {
            // If the enemy archetype supports chasing, transition
            if (enemy.chaseState != null)
            {
                enemy.TransitionToState(enemy.chaseState);
            }
        }
    }

    public override void ExitState(EnemyAI enemy) { }
}
