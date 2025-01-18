using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAttack : MonoBehaviour
{
    [Header("¹¥»÷·½Ê½ÐòºÅ")]
    public int index;
    public float attackInterval;
    private float timer = 0.0f;
    private AttackableObj thisAttackableObj;

    private void Start()
    {
        timer = attackInterval;
        thisAttackableObj = GetComponent<AttackableObj>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (timer >= attackInterval)
            {
                if (thisAttackableObj.gameObject.CompareTag("Player")) (thisAttackableObj as PlayerModel).attack(collision.gameObject.GetComponent<Character>(), index);
                else thisAttackableObj.attack(collision.gameObject.GetComponent<Character>(), index);
                thisAttackableObj.Effect(index);
                collision.gameObject.GetComponent<Character>().isAlive();
                timer = 0.0f;
            }
        }
    }
}
