using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public GameObject prefab;
    //public Transform parent;
    public GameObject particleSystem;
    private void OnDisable()
    {
        /*if (prefab != null)
        {
                Instantiate(prefab, gameObject.transform.position, prefab.transform.rotation);
        }*/
        if(particleSystem != null)
        {
            particleSystem.transform.position = gameObject.transform.position;
            particleSystem.SetActive(true);
        }
        
    }
}
