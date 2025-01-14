using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType 
{
    SimpleDamage,
    KnockDownDamage,
    EffectBasedDamage,
    StackableDamage
}
public enum Destructive
{
   None,
   low,
   monderate,
   high
}
[System.Serializable]
public struct Effect
{
    [Header("改变(值增加,枚举类型直接变化)")]
    public Skill targetSkill;
    [Header("Player击倒率")]
    public int KnockDownRate;
    [Header("秒/每次")]
    public float frequency;
    public bool isCoolingDown;
}
[System.Serializable]
public struct Skill
{
    public DamageType damageType;
    public int damageAmount;
    public int minDamage;
    public int maxDamage;
    public Destructive destructive;
    public List<Effect> effects;
}