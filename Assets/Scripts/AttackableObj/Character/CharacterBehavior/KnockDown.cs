using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockDown : MonoBehaviour
{
    public float duration;
    public CooldownDuration cooldownDuration;

    private void OnEnable()
    {
        SetState(false);
        Invoke("DisableSelf", duration);
        cooldownDuration.StartCoroutine(cooldownDuration.Cooldown(cooldownDuration.cooldownDuration));
    }
    private void OnDisable()
    {
        SetState(true);
    }
    public void SetState(bool state)
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
