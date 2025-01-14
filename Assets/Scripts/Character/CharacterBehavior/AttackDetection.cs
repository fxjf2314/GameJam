using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{
    [Header("攻击方式序号")]
    public int index;
    public float attackInterval = 1.0f; // 攻击间隔时间（秒）
    private float timer = 0.0f; 
    private Character thisCharacter;

    private void Start()
    {
        thisCharacter = transform.parent.GetComponent<Character>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")|| other.CompareTag("Monster"))
        {
            timer += Time.deltaTime;
            if (timer >= attackInterval)
            {
                thisCharacter.attack(other.GetComponent<Character>(), index);
                other.GetComponent<Character>().isAlive();
                timer = 0.0f;
            }
        }
    }
}
