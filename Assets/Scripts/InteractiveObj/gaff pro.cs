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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        firedir = other.transform.position - gameObject.transform.position;
        if (other.gameObject.name == "Player"||other.gameObject.name=="boss")
        {
            others = other.gameObject.GetComponent<Character>();
            hpdecrease();
            firedir.x = 0;
            firedir.y = 1.5f;
            other.GetComponent<Rigidbody>().AddForce(firedir * 1000);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" || other.gameObject.name == "boss")
        {
            // others = other.gameObject.GetComponent<Character>();
            //  Invoke("hpdecrease", 1f);
            //other.GetComponent<Rigidbody>().AddForce(firedir*300);
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
            others.hp -= 1;
            PlayerModel.Instance.isAlive();
            CancelInvoke("hpdecrease");
        }
        if (others != null && others.name == "boss")
        {
            others.hp -= 1;
            CancelInvoke("hpdecrease");
        }
    }

    void Destroyplayer()
    {
        Destroy(GameObject.Find("Player"));
    }
}
