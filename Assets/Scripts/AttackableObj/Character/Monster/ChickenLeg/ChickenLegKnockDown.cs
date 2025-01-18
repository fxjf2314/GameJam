using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenLegKnockDown : KnockDown
{
    private void Start()
    {
        characterAnimator = transform.Find("jituiA").GetComponent<Animator>();
    }

    public override void SetState(bool state)
    {        
        gameObject.GetComponent<MonsterController>().enabled = state;
        if (!state)
        {
            gameObject.transform.Find("jituiA").GetComponent<AttackDetection>().StopAllCoroutines();
            gameObject.transform.Find("jituiA").GetComponent<AttackDetection>().enabled = state;
        }
        else
        {
            gameObject.transform.Find("jituiA").GetComponent<AttackDetection>().enabled = state;
            (gameObject.transform.Find("jituiA").GetComponent<AttackDetection>() as ChickenLegAttack).Reset();
            StartCoroutine(standup());
        }
    }

    IEnumerator standup()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.transform.Find("jituiA").GetComponent<Collider>().enabled = true;
    }
}
