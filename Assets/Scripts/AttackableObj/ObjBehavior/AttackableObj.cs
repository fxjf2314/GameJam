using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class AttackableObj: MonoBehaviour
{
    public List<Skill> skillList;
    public virtual void attack(Character target, int index)
    {
        Skill damage = skillList[index];
        damage.damageAmount = UnityEngine.Random.Range(skillList[index].minDamage, skillList[index].maxDamage + 1);
        skillList[index] = damage;
        int damageAmount = CalculateDamage(target, index);
        target.ApplyDamage(damageAmount, skillList[index].damageType);
    }
    private int CalculateDamage(Character target, int index)
    {
        return skillList[index].damageAmount;
    }
    public void Effect(int index)
    {
        for (int i = 0; i < skillList[index].effects.Count; i++) 
        {
            if(!skillList[index].effects[i].isCoolingDown)
            {
                Skill skill = new Skill();
                skill.damageType = skillList[index].effects[i].targetSkill.damageType;
                skill.minDamage = skillList[index].effects[i].targetSkill.minDamage + skillList[index].minDamage;
                skill.maxDamage = skillList[index].effects[i].targetSkill.maxDamage + skillList[index].maxDamage;
                skill.destructive = skillList[index].effects[i].targetSkill.destructive;
                skill.effects = new List<Effect>(skillList[index].effects);
                skillList[index] = skill;
                if (gameObject.CompareTag("Player"))
                {

                    gameObject.GetComponent<PlayerModel>().KnockDownRate += skillList[index].effects[i].KnockDownRate;
                }
            }
        }
    }
}
