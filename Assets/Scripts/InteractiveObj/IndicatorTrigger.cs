using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IndicatorTrigger : MonoBehaviour
{
    [SerializeField]
    TextMeshPro text;

    private void Start()
    {
        text = transform.Find("Text").GetComponent<TextMeshPro>();
        text.alpha = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            text.alpha = 1.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.alpha = 0;
        }
    }
}
