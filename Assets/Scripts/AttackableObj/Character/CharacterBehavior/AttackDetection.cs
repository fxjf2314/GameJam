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

    protected virtual void Start()
    {
        thisAttackableObj = transform.parent.GetComponent<AttackableObj>();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            {
                //print(thisAttackableObj.gameObject.name);
                for (int i = 0; i < thisAttackableObj.skillList[index].effects.Count; i++)
                    StartCoroutine(EffectCooldown(thisAttackableObj.skillList[index].effects[i]));
            }
        }
    }
    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")|| other.CompareTag("Monster"))
        {
            timer += Time.deltaTime;
            if (timer >= attackInterval)
            {
                if (thisAttackableObj.gameObject.CompareTag("Player")) (thisAttackableObj as PlayerModel).attack(other.GetComponent<Character>(), index);
                else thisAttackableObj.attack(other.GetComponent<Character>(), index);
                thisAttackableObj.Effect(index);
                other.GetComponent<Character>().isAlive();
                ChangeState();
                Invoke("ChangeState", attackRecovery);
                timer = 0.0f;
            }
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            for (int i = 0; i < thisAttackableObj.skillList[index].effects.Count; i++)
                StopCoroutine(EffectCooldown(thisAttackableObj.skillList[index].effects[i]));
        }
    }
    public IEnumerator EffectCooldown(Effect effect)
    {
        effect.isCoolingDown = true;
        yield return new WaitForSeconds(effect.frequency);
        effect.isCoolingDown = false;
    }
    protected virtual void ChangeState()
    {
        if (gameObject.transform.parent.CompareTag("Player"))
        {
            gameObject.transform.parent.GetComponent<PlayerController>().enabled = !gameObject.transform.parent.GetComponent<PlayerController>().enabled;
            print(gameObject.transform.parent.name+ gameObject.transform.parent.GetComponent<PlayerController>().enabled);
        }
        if (gameObject.transform.parent.CompareTag("Monster"))
        {
            gameObject.transform.parent.GetComponent<MonsterController>().enabled = !gameObject.transform.parent.GetComponent<MonsterController>().enabled;
            print(gameObject.transform.parent.name+ gameObject.transform.parent.GetComponent<MonsterController>().enabled);
        }
    }
}
