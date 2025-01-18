using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockDown : MonoBehaviour
{
    public float duration;
    public CooldownDuration cooldownDuration;
    [SerializeField]
    protected Animator characterAnimator;

    private void Start()
    {
        characterAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        SetState(false);
        Invoke("DisableSelf", duration);
        cooldownDuration.StartCoroutine(cooldownDuration.Cooldown(cooldownDuration.cooldownDuration));
    }
    private void OnDisable()
    {
        SetState(true);
        characterAnimator.SetBool("KnockdownFinish", true);
        characterAnimator.SetBool("KnockDown", false);
        characterAnimator.SetLayerWeight(0, 1);
        characterAnimator.Play("zou",1);
    }
    public virtual void SetState(bool state)
    {
        if (gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<PlayerController>().enabled = state;
        }
        if (gameObject.CompareTag("Monster"))
        {
            gameObject.GetComponent<MonsterController>().enabled = state;
        }
        if (gameObject.transform.Find("Attack") != null)
        {
            gameObject.transform.Find("Attack").gameObject.SetActive(state);
        }
    }
    private void DisableSelf()
    {
        this.enabled = false;
    }
}
