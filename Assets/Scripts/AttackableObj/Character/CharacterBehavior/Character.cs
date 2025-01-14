
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character: AttackableObj
{
    public int hp;

    public void isAlive()
    {
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void ApplyDamage(int amount, DamageType type)
    {
        hp -= amount;
        switch (type)
        {
            case DamageType.SimpleDamage:
                break;
            case DamageType.EffectBasedDamage:
                break;
            case DamageType.StackableDamage:
                break;
            case DamageType.KnockDownDamage:
                KnockDown();
                break;
        }
    }
    private void KnockDown()
    {
        if (!gameObject.GetComponent<KnockDown>().cooldownDuration.isCoolingDown)
        {
            gameObject.GetComponent<KnockDown>().enabled = true;
        }
    }
}
