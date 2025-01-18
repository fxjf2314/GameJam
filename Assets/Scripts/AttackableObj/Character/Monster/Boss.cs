using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    GameObject bosshpbar;
    GameObject bossdeath;
    GameObject bossdeathparticle;

   // Rigidbody bossrb;
    public int a = 0;//×´Ì¬Á¿
    bool ifcanmove = false;
    bool ifhpstart = false;
    bool ifreturnani = false;
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
        bossdeathparticle = GameObject.Find("bossdeathparticle");
        bossdeathparticle.SetActive(false);
        bossdeath = GameObject.Find("bossdeath");
        bossdeath.SetActive(false);
        ifreturnani = false;
        ifhpstart = false;
        bosshpbar = GameObject.Find("Bosshpbar");
        //bosshpbar.SetActive(false);
        bosshpbar.GetComponent<Slider>().maxValue = GameObject.Find("boss").GetComponent<MonsterController>().hp;
        bosshpbar.GetComponent<Slider>().value = 0;
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
        if (GameObject.Find("boss").GetComponent<MonsterController>().hp <= 0)
        {
            if (!ifreturnani)
            {
                animat.SetInteger("tomatoway", 0);
                animat.SetInteger("leaf way", 0);
                Invoke("Deathani", 2);
                ifreturnani=true;
            }
            if (particle != null)
            {
                particle.SetActive(false);
            }
            Instance.a = 20;
            Bossattackdetection.Instance.now = 0;
            Bossattackdetection.Instance.nowtime = 1;
            //Invoke("Deathani", 2);
            //Deathani();
            //GameObject.Find("boss").GetComponent<MonsterController>().enabled = false;
            GameObject.Find("skill1").GetComponent<Bossattackdetection>().enabled = false;
            GameObject.Find("skill2").GetComponent<Bossskill2>().enabled = false;
            Invoke("destroyboss", 4f);
        }

        if (GameObject.Find("boss").GetComponent<MonsterController>().hp < 125&& Instance.a ==18)
        {
            GameObject.Find("Player").GetComponent<Rigidbody>().AddForce((GameObject.Find("Player").GetComponent<Transform>().position - transform.position)*1000);
            Bossattackdetection.Instance.skill2time -= 4;
            Bossattackdetection.Instance.skill1time -= 1;
            GameObject.Find("skill1").GetComponent<Bossattackdetection>().attackInterval -= 0.2f;
            GameObject.Find("boss").GetComponent<MonsterController>().initDamage = 1;
            Instance.a = 0;
            animat.SetInteger("tomatoway", 100);
            animat.SetInteger("leaf way", 2);
            Invoke("Returnleafway", 1);
            Invoke("Bossdisappera", 2.1f);
            ifspeattack = true;
        }

        if (!ifcanmove)
        {
            transform.position=aimpos;
        }

        if (bosshpbar.GetComponent<Slider>().value == 250)
        {
            ifhpstart = true;
        }

        if (ifhpstart)
        {
            bosshpbar.GetComponent<Slider>().value = GameObject.Find("boss").GetComponent<MonsterController>().hp;
        }
    }
    void Bossdisappera()
    {
        CancelInvoke("Bossdisappera");
        boss.SetActive(false);
        boss.transform.position = aimpos;
        Invoke("Bossappera", 0.3f);
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

    void Deathani()
    {
        animat.SetInteger("tomatoway", 7);
        animat.SetInteger("leaf way", 3);
    }

    void Returnleafway()
    {
        animat.SetInteger("leaf way", 0);
    }

    private void destroyboss()
    {
        bossdeath.SetActive(true);
        gameObject.SetActive(false);
        bossdeathparticle.SetActive(true);
        Invoke("deathparticle", 1);

    }

    void deathparticle()
    {
        
        bossdeathparticle.SetActive(false);
    }

    void bosshpdecrease()
    {
        Instance.a++;       
        //GameObject.Find("boss").GetComponent<MonsterController>().hp -= 1;
        
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
            if(Instance.a == 3)
            {
                gaff3.SetActive(true);
            }
            if (Instance.a == 5)
            {
                gaff2.SetActive(true);
                GameObject.Find("Player").GetComponent<Rigidbody>().AddForce((transform.position-GameObject.Find("Player").GetComponent<Transform>().position ) * 800);
            }
            if( Instance.a == 7)
            {
                gaff.SetActive(true);
            }
            //CancelInvoke("bosshpdecrease");
            Invoke("bosshpdecrease", 1f);
        }
    }
}
