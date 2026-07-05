using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "AI/States/Attack")]
public class AttackStateSO : EnemyStateSO
{
    public override void EnterState(EnemyAI enemy) { }

    public override void UpdateState(EnemyAI enemy)
    {
        if (enemy.IsTargetInAttackRange())
        {
            enemy.TriggerAttack();
            return;
        }
        enemy.TransitionToState(enemy.idleState);
    }

    public override void ExitState(EnemyAI enemy) { }
}
