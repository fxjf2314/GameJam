using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiliController : MonsterController
{
    
    public GameObject fire;

    private Animator chiliAnimator;

    [SerializeField]
    private BoxCollider fireCollider;
    
    bool isAttack;

    [SerializeField]
    private ShootFire shootFire;

    private float attackDuring = 2.5f;

    protected override void Start()
    {
        base.Start();
        chiliAnimator = GetComponent<Animator>();
        
        
    }

    public override void Update()
    {
        base.Update();
        if (Vector3.Distance(player.position, transform.position) <= 5 && !isAttack)
        {
            
            StartCoroutine(AttackToPlayer());
            
            StartCoroutine(WaitForAttack());
        }
    }

    public IEnumerator AttackToPlayer()
    {
        chiliAnimator.SetBool("Finish", false);
        chiliAnimator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.5f);
        fire.SetActive(true);
        yield return new WaitForSeconds(attackDuring);
        chiliAnimator.SetBool("Attack", false);
        chiliAnimator.SetBool("Finish", true);

        fire.SetActive(false);
    }

    IEnumerator WaitForAttack()
    {
        isAttack = true;
        shootFire.MyTimeCounter = 0;

        yield return new WaitForSeconds(8.0f);

        isAttack = false;
    }

    
}
