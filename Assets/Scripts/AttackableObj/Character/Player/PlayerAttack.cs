using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerAttack : AttackDetection
{
    bool isAttack;
    Animator animator;

    Coroutine beforeAtkCoroutine;
    Coroutine afterAtkCoroutine;
    bool isCanAtk = false;
    [SerializeField]
    Collider trigger;


    private void Awake()
    {
        animator = transform.parent.GetComponent<Animator>();
        base.Start();
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            trigger.enabled = true;
            StartCoroutine(BeforeAtk());
        }
    }

    



    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            
            if (beforeAtkCoroutine != null && isCanAtk)
            {
                thisAttackableObj.attack(other.GetComponent<Character>(), index);
                thisAttackableObj.Effect(index);
                other.GetComponent<Character>().isAlive();
                Invoke("ChangeState", attackRecovery);
            }
        }

    }


    IEnumerator BeforeAtk()
    {
        animator.SetBool("Attack", true);
        
        while (timer < attackInterval)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        animator.SetBool("Attack", false);
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
        isCanAtk = false;
        beforeAtkCoroutine = null;
        afterAtkCoroutine = null;
        trigger.enabled = false;
    }

    IEnumerator Atk()
    {
        
        while (timer < 0.2)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0.0f;
        isCanAtk = false;
        
        StartCoroutine(AfterAtk());
    }



}
