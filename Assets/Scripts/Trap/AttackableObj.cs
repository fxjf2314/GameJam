using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackableObj: MonoBehaviour
{
    public List<Skill> damageList;
    public void attack(Character target, int index)
    {
        Skill damage = damageList[index];
        damage.damageAmount = Random.Range(damageList[index].minDamage, damageList[index].maxDamage + 1);
        damageList[index] = damage;
        int damageAmount = CalculateDamage(target, index);
        target.ApplyDamage(damageAmount, damageList[index].damageType);
    }
    private int CalculateDamage(Character target, int index)
    {
        return damageList[index].damageAmount;
    }
}
