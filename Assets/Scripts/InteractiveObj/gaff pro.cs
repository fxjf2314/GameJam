using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class gaffpro : MonoBehaviour
{
    Character others;
    Vector3 firedir;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (others != null)
        {
            if (others.name == "Player" && others.hp < 0)
            {
                Invoke("Destroyplayer", 0.1f);
            }
            if (others.name == "boss" && others.hp < 0)
            {
                Invoke("Destroyboss", 0.1f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        firedir = other.transform.position - gameObject.transform.position;
        if (other.gameObject.name == "Player"||other.gameObject.name=="boss")
        {
            others = other.gameObject.GetComponent<Character>();
            hpdecrease();
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" || other.gameObject.name == "boss")
        {
           // others = other.gameObject.GetComponent<Character>();
          //  Invoke("hpdecrease", 1f);
            other.GetComponent<Rigidbody>().AddForce(firedir*200);
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player" || other.gameObject.name == "boss")
        {
            others = other.gameObject.GetComponent<Character>();
            CancelInvoke("hpdecrease");
        }
    }
    void hpdecrease()
    {
        if (others != null&&others.name=="Player")
        {
            others.hp -= 5;
            CancelInvoke("hpdecrease");
        }
        if (others != null && others.name == "boss")
        {
            others.hp -= 2;
            CancelInvoke("hpdecrease");
        }
    }

    void Destroyplayer()
    {
        Destroy(GameObject.Find("Player"));
    }

    void Destroyboss()
    {
        Destroy(GameObject.Find("boss"));
    }
}
