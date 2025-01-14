using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class KnockDown : MonoBehaviour
{
    public float duration;
    public CooldownDuration cooldownDuration;

    private void OnEnable()
    {
        if (gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<PlayerController>().enabled = false;
        }
        if (gameObject.CompareTag("Monster"))
        {
            gameObject.GetComponent<MonsterController>().enabled = false;
        }
        if (gameObject.transform.Find("Attack") != null)
        {
            gameObject.transform.Find("Attack").gameObject.SetActive(false);
        }
        Invoke("DisableSelf", duration);
        cooldownDuration.StartCoroutine(cooldownDuration.Cooldown(cooldownDuration.cooldownDuration));
    }
    private void OnDisable()
    {
        if (gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<PlayerController>().enabled =true;
        }
        if (gameObject.CompareTag("Monster"))
        {
            gameObject.GetComponent<MonsterController>().enabled = true;
        }
        if (gameObject.transform.Find("Attack") != null)
        {
            gameObject.transform.Find("Attack").gameObject.SetActive(true);
        }
    }
    private void DisableSelf()
    {
        this.enabled = false;
    }
}
