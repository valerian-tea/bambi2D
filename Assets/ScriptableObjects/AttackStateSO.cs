using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "AI/States/Attack")]
public class AttackStateSO : EnemyStateSO
{
    public override void EnterState(EnemyAI enemy) => Debug.Log("Entered Attack State");

    public override void UpdateState(EnemyAI enemy)
    {
        enemy.AttackTarget();

        if (!enemy.IsTargetInAttackRange())
        {
            // Debug.Log("Target out of attack range.");
            // if (enemy.chaseState != null)
            // {
            //     enemy.TransitionToState(enemy.chaseState);
            // }
            // else if (enemy.patrolState != null)
            // {
            //     enemy.TransitionToState(enemy.patrolState);
            // }
            // else
            // {
            Debug.Log("transitioning to idle because not in range");
            enemy.TransitionToState(enemy.idleState);
            // }
        }
    }

    public override void ExitState(EnemyAI enemy) { }
}
