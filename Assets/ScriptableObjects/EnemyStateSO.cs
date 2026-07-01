using UnityEngine;

public abstract class EnemyStateSO : ScriptableObject
{
    public abstract void EnterState(EnemyAI enemy);
    public abstract void UpdateState(EnemyAI enemy);
    public abstract void ExitState(EnemyAI enemy);
}
