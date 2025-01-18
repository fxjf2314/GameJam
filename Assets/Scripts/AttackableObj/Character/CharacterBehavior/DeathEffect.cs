using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;
    public GameObject particleSystem;
    private void OnDisable()
    {
        if (prefab != null)
        {
                GameObject instantiatedObject = Instantiate(prefab, gameObject.transform.position, prefab.transform.rotation, parent);
        }
        particleSystem.transform.position = gameObject.transform.position;
        particleSystem.SetActive(true);
    }
}
