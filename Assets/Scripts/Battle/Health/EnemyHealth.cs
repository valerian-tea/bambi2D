using System;
using UnityEngine;

public class EnemyHealth : Health
{
    private EnemyAI enemyAI;

    protected override void Start()
    {
        base.Start();
        enemyAI = GetComponent<EnemyAI>();
    }

    public override void PlayHitAnimation(bool isFrontHit)
    {
        if (enemyAI != null && enemyAI.isAttacking)
        {
            return;
        }

        base.PlayHitAnimation(isFrontHit);
    }
}
