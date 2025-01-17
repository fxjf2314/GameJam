using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Animator animat;
    private bool ifspeattack = false;
    private bool ifdanger=true;
    Vector3 aimpos = new Vector3(0, 0.2f, 12.53553f);
    GameObject particle;
    GameObject gaff;
    GameObject gaff2;
    GameObject gaff3;
    GameObject boss;

   // Rigidbody bossrb;
    public int a = 0;//×´Ì¬Á¿
    bool ifcanmove = false;
    static Boss mInstance;
    public static Boss Instance
    {
        get
        {
            if(mInstance == null)
            {
                mInstance=FindObjectOfType<Boss>();
                if (mInstance == null)
                {

                }
            }
            return mInstance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ifcanmove = true;
        //bossrb= GetComponent<Rigidbody>();
        boss = gameObject;
        gaff = GameObject.Find("gaff");
        gaff2 = GameObject.Find("gaff (2)");
        gaff3 = GameObject.Find("gaff (3)");
        gaff.SetActive(false);
        gaff2.SetActive(false);
        gaff3.SetActive(false);
        Instance.a = 18;
        ifdanger = true;
        ifspeattack = false;
        animat = GameObject.Find("boss").GetComponent<Animator>();
        particle = GameObject.Find("particles");
        particle.SetActive(false);
        animat.SetInteger("leaf way", 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("boss").GetComponent<MonsterController>().hp < 50&& Instance.a ==18)
        {
            GameObject.Find("skill1").GetComponent<Bossattackdetection>().skill2time -= 5;
            GameObject.Find("skill1").GetComponent<Bossattackdetection>().skill1time -= 2f;
            GameObject.Find("skill2").GetComponent<Bossattackdetection>().skill2time -= 5;
            GameObject.Find("skill2").GetComponent<Bossattackdetection>().skill1time -= 2f;
            Instance.a = 0;
            GameObject.Find("boss").GetComponent<MonsterController>().hp += 30;
            animat.SetInteger("tomatoway", 100);
            animat.SetInteger("leaf way", 2);
            Invoke("Returnleafway", 1);
            Invoke("Bossdisappera", 2.1f);
            ifspeattack = true;
        }
        /*if (ifspeattack)
        {
            Vector3 bossrot = new Vector3(0, 0, 0) - transform.eulerAngles;
            Vector3 move = aimpos - GameObject.Find("boss").GetComponent<Transform>().position;
            //GameObject.Find("boss").GetComponent<Rigidbody>().AddForce(move, ForceMode.Acceleration);
            GameObject.Find("boss").transform.position += move /1000 ;
            GameObject.Find("boss").transform.eulerAngles += bossrot / 1000;
            Invoke("bossrotate", 2f);
        }*/
        if (!ifcanmove)
        {
            transform.position=aimpos;
        }

    }
    void Bossdisappera()
    {
        CancelInvoke("Bossdisappera");
        boss.SetActive(false);
        boss.transform.position = aimpos;
        Invoke("Bossappera", 0.2f);
        //boss.transform.eulerAngles = Vector3.zero;
    }

    void Bossappera()
    {
        boss.SetActive(true);
        bossrotate();
    }

    void bossrotate()
    {
        ifspeattack = false;
        CancelInvoke("bossrotate");
        GameObject.Find("boss").transform.eulerAngles = new Vector3(0, 0, 0);
        //bossrb.freezeRotation = true;
        transform.position = aimpos;
        ifcanmove = false;
        animat.SetInteger("leaf way", 4);
        animat.SetInteger("tomatoway", 10);
        Invoke("Bossattack", 1);
    }

    private void Bossattack()
    {
        CancelInvoke("Bossattack");
        
        if (GameObject.Find("boss") != null)
        {
            Invoke("bosshpdecrease", 1f);
        }
    }

    void Returnleafway()
    {
        animat.SetInteger("leaf way", 0);
    }
    void bosshpdecrease()
    {
        Instance.a++;       
        GameObject.Find("boss").GetComponent<MonsterController>().hp -= 1;
        
        if (Instance.a == 10) 
        {
            Instance.a = 19;
            if (GameObject.Find("boss") != null)
            {
                particle.SetActive(false);
                //bossrb.freezeRotation = false;
                //gameObject.GetComponent<CapsuleCollider>().enabled = true;
               // bossrb.useGravity = true;
                ifcanmove = true;
            }
           
           // CancelInvoke("bosshpdecrease");
        }
        else if (Instance.a < 10)
        {
            if (Instance.a == 2)
            {
                particle.SetActive(true);
                animat.SetInteger("leaf way", -4);
                animat.SetInteger("tomatoway", -10);
            }
            if (Instance.a == 8)
            {
                gaff.SetActive(true);
                gaff2.SetActive(true);
                gaff3.SetActive(true);
                
            }
            //CancelInvoke("bosshpdecrease");
            Invoke("bosshpdecrease", 1f);
        }
    }
}
