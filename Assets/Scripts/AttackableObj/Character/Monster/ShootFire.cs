using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFire : AttackDetection
{
    private BoxCollider fireRegion;

    private MonsterController chiliController;

    public int totalTime;

    private int timeCounter;

    private void Awake()
    {
        chiliController = GetComponent<MonsterController>();
    }

    public override void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            
            timer += Time.deltaTime;
            
            if (timer >= attackInterval)
            {
                if(timeCounter >= 3)
                {
                    Skill skill = new Skill();
                    skill.damageType = DamageType.KnockDownDamage;
                    skill.minDamage = thisAttackableObj.skillList[index].effects[index].targetSkill.minDamage;
                    skill.maxDamage = thisAttackableObj.skillList[index].effects[index].targetSkill.maxDamage;
                    thisAttackableObj.skillList[index] = skill;
                    if (thisAttackableObj.gameObject.CompareTag("Player")) (thisAttackableObj as PlayerModel).attack(other.GetComponent<Character>(), index);
                    else
                    {
                        thisAttackableObj.attack(other.GetComponent<Character>(), index);
                        timeCounter++;
                    }
                }
                else
                {
                    Skill skill = new Skill();
                    skill.damageType = DamageType.SimpleDamage;
                    skill.minDamage = thisAttackableObj.skillList[index].effects[index].targetSkill.minDamage;
                    skill.maxDamage = thisAttackableObj.skillList[index].effects[index].targetSkill.maxDamage;
                    thisAttackableObj.skillList[index] = skill;
                    if (thisAttackableObj.gameObject.CompareTag("Player")) (thisAttackableObj as PlayerModel).attack(other.GetComponent<Character>(), index);
                    else
                    {
                        thisAttackableObj.attack(other.GetComponent<Character>(), index);
                        timeCounter++;
                    }
                }
                other.GetComponent<Character>().isAlive();
                timer = 0.0f;
            }
        }
        ChangeState();
        Invoke("ChangeState", attackRecovery);
    }

    public override void OnTriggerExit(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            timeCounter = 0;
        }
       
    }


    public override void ChangeState()
    {
        gameObject.transform.parent.GetComponent<ChiliController>().enabled = !gameObject.transform.parent.GetComponent<ChiliController>().enabled;
        print(gameObject.transform.parent.name + gameObject.transform.parent.GetComponent<ChiliController>().enabled);
    }
    /*IEnumerator FireTime()
    {

    }*/
}
