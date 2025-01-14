using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class gaff : MonoBehaviour
{
    Character others;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            others = other.gameObject.GetComponent<Character>();
            hpdecrease();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            others = other.gameObject.GetComponent<Character>();
            Invoke("hpdecrease", 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            others = other.gameObject.GetComponent<Character>();
            CancelInvoke("hpdecrease");
        }
    }
    void hpdecrease()
    {
        if (others != null)
        {
            others.hp -= 5;
            CancelInvoke("hpdecrease");
        }
    }
}
