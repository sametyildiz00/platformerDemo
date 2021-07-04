using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealth : Health, IDamageable<int>
{

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);


        if (CheckIfDead())
        {
            OnDeath();
        }

    }

    protected override void OnDeath()
    {
        base.OnDeath();

        Destroy(gameObject);
    }
}
