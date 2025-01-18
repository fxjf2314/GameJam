
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character: AttackableObj
{
    [SerializeField]
    private Animator characterAnimator;

    ChiliController chiliController;

    [SerializeField]
    private int deathTime;

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
        hp -= amount;
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
        if (!gameObject.GetComponent<KnockDown>().cooldownDuration.isCoolingDown)
        {
            characterAnimator.SetLayerWeight(1,1);
            characterAnimator.SetBool("KnockDown",true);
            characterAnimator.SetBool("KnockdownFinish", true);
            gameObject.GetComponent<KnockDown>().enabled = true;
        }
    }

    IEnumerator Death()
    {
        characterAnimator.SetBool("Died", true);

        yield return new WaitForSeconds(deathTime);

        gameObject.SetActive(false);
    }
}
