using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "AI/States/Idle")]
public class IdleStateSO : EnemyStateSO
{
    public float lookRotationSpeed = 5f;

    public override void EnterState(EnemyAI enemy)
    {
        Debug.Log("Entered Idle State.");
    }

    public override void UpdateState(EnemyAI enemy)
    {
        if (enemy.IsTargetInAttackRange())
        {
            enemy.TransitionToState(enemy.attackState);
        }
    }

    public override void ExitState(EnemyAI enemy) { }
}
