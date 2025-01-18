using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    bool isAttack;

    [SerializeField]
    GameObject attacker;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isAttack)
        {
            StartCoroutine(Attack());
        }
    }

    

    IEnumerator Attack()
    {
        isAttack = true;
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.6f);
        attacker.SetActive(true);

        yield return new WaitForSeconds(0.65f);

        
        animator.SetBool("Attack", false);
        attacker.SetActive(false);
        isAttack = false;
    }
}
