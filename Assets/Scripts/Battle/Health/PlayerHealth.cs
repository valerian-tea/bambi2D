using System;
using UnityEngine;

public class PlayerHealth : Health
{
    public static event Action OnHealthChanged;

    protected override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(int damage, bool isFrontHit)
    {
        base.TakeDamage(damage, isFrontHit);
        OnHealthChanged?.Invoke();
    }
}
