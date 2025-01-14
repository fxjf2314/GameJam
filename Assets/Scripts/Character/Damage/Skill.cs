using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType 
{
    SimpleDamage,
    EffectBasedDamage,
    KnockDownDamage,
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
public struct Skill
{
    public DamageType damageType;
    public int damageAmount;
    public int minDamage;
    public int maxDamage;
    public Destructive destructive;
}