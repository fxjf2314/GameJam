using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;
    private void OnDisable()
    {
        GameObject instantiatedObject = Instantiate(prefab,gameObject.transform.position,prefab.transform.rotation,parent);
    }
}
