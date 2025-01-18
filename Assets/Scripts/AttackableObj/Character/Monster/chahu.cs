using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chahu : MonoBehaviour
{
    bool ifdown = false;
    bool hasdamage = false;
    // Start is called before the first frame update
    void Start()
    {
        hasdamage = false;
        ifdown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 10.5&&!hasdamage)
        {
            ifdown = true;
            
        }
        if(transform.position.y <= 1)
        {
            returnback();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ifdown)
        {
            if (collision.gameObject.name == "boss")
            {
                GameObject.Find("boss").GetComponent<MonsterController>().hp -= 10;
                ifdown = false;
                hasdamage = true;
            }
        }
    }

    void returnback()
    {
        ifdown=false;
        hasdamage=true;
    }
}
