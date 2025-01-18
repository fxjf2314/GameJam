using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFire : AttackDetection
{
    
    //public int totalTime;

    private int timeCounter;

    public int MyTimeCounter { get => timeCounter; set => timeCounter = value; }

    public override void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            
            timer += Time.deltaTime;
            
            if (timer >= 0.5)
            {
                if(timeCounter >= 3)
                {
                    Skill skill = new Skill();
                    skill.damageType = DamageType.KnockDownDamage;
                    skill.minDamage = thisAttackableObj.skillList[index].minDamage;
                    skill.maxDamage = thisAttackableObj.skillList[index].maxDamage;
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
                    //Debug.Log("1111");
                    Skill skill = new Skill();
                    skill.damageType = DamageType.SimpleDamage;
                    skill.minDamage = thisAttackableObj.skillList[index].minDamage;
                    skill.maxDamage = thisAttackableObj.skillList[index].maxDamage;
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
    
    
}
