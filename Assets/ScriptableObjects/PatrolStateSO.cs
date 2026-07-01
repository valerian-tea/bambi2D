using UnityEngine;

[CreateAssetMenu(fileName = "PatrolState", menuName = "AI/States/Patrol")]
public class PatrolStateSO : EnemyStateSO
{
    public float patrolSpeed = 2f;

    public override void EnterState(EnemyAI enemy)
    {
        Debug.Log("Entered Patrol State");
    }

    public override void UpdateState(EnemyAI enemy)
    {
        // Patrol movement logic using enemy.transform
        enemy.Move(patrolSpeed);

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
