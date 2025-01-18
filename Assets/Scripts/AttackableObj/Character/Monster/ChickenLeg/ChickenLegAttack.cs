using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChickenLegAttack : AttackDetection
{
    Animator animator;
    Coroutine beforeAtkCoroutine;
    Coroutine afterAtkCoroutine;
    bool isCanAtk = false;
    [SerializeField]
    Collider trigger;

    protected override void Start()
    {
        base.Start();
        animator = transform.GetComponent<Animator>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (beforeAtkCoroutine == null && !isCanAtk)
            {
                beforeAtkCoroutine = StartCoroutine(BeforeAtk());
            }
            if(beforeAtkCoroutine != null && isCanAtk)
            {
                thisAttackableObj.attack(other.GetComponent<Character>(), index);
                thisAttackableObj.Effect(index);
                other.GetComponent<Character>().isAlive();
                Invoke("ChangeState", attackRecovery);
            }
        }
        
    }

    protected override void OnTriggerStay(Collider other)
    {
        
    }

    protected override void OnTriggerExit(Collider other)
    {
        
    }

    public void Reset()
    {
        StopAllCoroutines();
        beforeAtkCoroutine = null;
        afterAtkCoroutine = null;
        isCanAtk = false;
        animator.SetBool("isAttack", false);
    }

    IEnumerator BeforeAtk()
    {
        animator.SetBool("isAttack", true);
        trigger.enabled = false;
        while (timer < attackInterval)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        animator.SetBool("isAttack", false);
        isCanAtk = true;
        //Debug.Log("Attacking");
        timer = 0.0f;
        afterAtkCoroutine = StartCoroutine(Atk());
    }

    IEnumerator AfterAtk()
    {
        while (timer < attackRecovery)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0.0f;
        isCanAtk=false;
        beforeAtkCoroutine = null;
        afterAtkCoroutine= null;
        trigger.enabled = true;
    }

    IEnumerator Atk()
    {
        trigger.enabled = true;
        while (timer < 0.2)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0.0f;
        isCanAtk = false;
        trigger.enabled = false;
        StartCoroutine(AfterAtk());
    }

    

}
