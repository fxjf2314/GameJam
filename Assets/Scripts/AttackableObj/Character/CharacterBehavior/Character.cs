
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character: AttackableObj
{
    [SerializeField]
    protected Animator characterAnimator;

    [SerializeField]
    GameObject EXP;

    [SerializeField]
    GameObject deathEffect;

    [SerializeField]
    private float deathTime;

    public GameObject deathUI;

    public int hp;

    public void isAlive()
    {
        if (hp <= 0)
        {
            
            //StopCoroutine();
            StartCoroutine(Death());

        }
    }
    public void ApplyDamage(int amount, DamageType type)
    {
        print(gameObject.name+"受到了"+ amount+"点伤害");
        hp -= amount;
        if(hp <= 0 && PlayerItemCheck.Instance.canDefense)
        {
            Debug.Log("Defense!");
            hp += amount;
            PlayerItemCheck.Instance.startPotatoCooling();
        }
        switch (type)
        {
            case DamageType.SimpleDamage:
                break;
            case DamageType.EffectBasedDamage:
                break;
            case DamageType.StackableDamage:
                break;
            case DamageType.KnockDownDamage:
                KnockDown();
                break;
        }
    }
    protected void KnockDown()
    {
        if (gameObject.GetComponent<KnockDown>()&&!gameObject.GetComponent<KnockDown>().cooldownDuration.isCoolingDown)
        {
            ChiliKnockDown();
            characterAnimator.SetLayerWeight(1,1);
            characterAnimator.SetBool("KnockDown",true);
            characterAnimator.SetBool("KnockdownFinish", false);
            gameObject.GetComponent<KnockDown>().enabled = true;
        }
    }

    IEnumerator Death()
    {
        KnockDown();
        characterAnimator.SetBool("Died", true);

        yield return new WaitForSeconds(deathTime);
        if (gameObject.transform.CompareTag("Monster"))
        {
            if(deathEffect != null)
            {
                Instantiate(deathEffect,gameObject.transform.position,Quaternion.identity);
            }
            Instantiate(EXP,gameObject.transform.position,Quaternion.identity);
        }
        gameObject.SetActive(false);
        if(gameObject.transform.CompareTag("Player"))
            deathUI.SetActive(true);
    }

    private void ChiliKnockDown()
    {
        if(gameObject.GetComponent<ChiliController>() != null)
        {
            ChiliController chiliController = gameObject.GetComponent<ChiliController>();
            chiliController.fire.SetActive(false);
        }
    }
}
