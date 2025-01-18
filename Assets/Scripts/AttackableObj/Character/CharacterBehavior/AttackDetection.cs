using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{

    [Header("攻击方式序号")]
    public int index;
    public float attackInterval; // 攻击间隔时间（秒）
    public float attackRecovery; // 攻击后摇时间（秒）
    protected float timer = 0.0f;
    protected AttackableObj thisAttackableObj;

    private void Start()
    {
        timer = attackInterval;
        thisAttackableObj = transform.parent.GetComponent<AttackableObj>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            {
                if (thisAttackableObj.skillList[index].effects != null)
                {
                    for (int i = 0; i < thisAttackableObj.skillList[index].effects.Count; i++)
                        StartCoroutine(EffectCooldown(thisAttackableObj.skillList[index].effects[i]));
                }
            }
        }
    }
    public virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            timer += Time.deltaTime;
            if (timer >= attackInterval&&!thisAttackableObj.isRecovering)
            {
                if (thisAttackableObj.gameObject.CompareTag("Player")) (thisAttackableObj as PlayerModel).attack(other.GetComponent<Character>(), index);
                else thisAttackableObj.attack(other.GetComponent<Character>(), index);
                thisAttackableObj.Effect(index);
                other.GetComponent<Character>().isAlive();
                SetStateFalse();
                StartCoroutine(IsRecovering());
                Invoke("SetStateTrue", attackRecovery);
                timer = 0.0f;
            }
        }
    }
    public virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            if (thisAttackableObj.skillList[index].effects != null)
            {
                for (int i = 0; i < thisAttackableObj.skillList[index].effects.Count; i++)
                    StopCoroutine(EffectCooldown(thisAttackableObj.skillList[index].effects[i]));
            }
        }
    }
    public IEnumerator EffectCooldown(Effect effect)
    {
        effect.isCoolingDown = true;
        yield return new WaitForSeconds(effect.frequency);
        effect.isCoolingDown = false;
    }
    public IEnumerator IsRecovering()
    {
        thisAttackableObj.isRecovering = true;
        yield return new WaitForSeconds(attackRecovery);
        thisAttackableObj.isRecovering = false;
    }
    public  virtual void SetStateTrue()
    {
        if (gameObject.transform.parent.CompareTag("Player"))
        {
            gameObject.transform.parent.GetComponent<PlayerController>().enabled = true ;
        }
        if (gameObject.transform.parent.CompareTag("Monster"))
        {
            gameObject.transform.parent.GetComponent<MonsterController>().enabled = true;
        }
    }
    public virtual void SetStateFalse()
    {
        if (gameObject.transform.parent.CompareTag("Player"))
        {
            gameObject.transform.parent.GetComponent<PlayerController>().enabled = false;
        }
        if (gameObject.transform.parent.CompareTag("Monster"))
        {
            gameObject.transform.parent.GetComponent<MonsterController>().enabled = false;
        }
    }
}