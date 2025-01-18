using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Bossattackdetection : MonoBehaviour
{
    [Header("攻击方式序号")]
    Animator animator;
    Vector3 aimrotate = new Vector3(8.892f, -90, 0);
    public int index;
    public float attackInterval; // 攻击间隔时间（秒）
    public float attackRecovery; // 攻击后摇时间（秒）
    public int now = 0;//现在状态
    private float timer = 0.0f;
    private AttackableObj thisAttackableObj;
    GameObject[] breakone = new GameObject[1];
    private float realtime;
    public float nowtime;
    private bool ifbrakecamera = false;
    private bool ifhavebrake = false;
    bool ifreturncamera = false;
    Transform camera;
    public float skill1time = 0;
    public float skill2time = 0;
    bool hasattack = false;
    float writetime = 0;

    static Bossattackdetection mInstance;
    public static Bossattackdetection Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<Bossattackdetection>();
            }
            if (mInstance == null)
            {

            }
            return mInstance;
        }
    }

    private void Start()
    {
        writetime = 0;
        hasattack = false;
        Instance.skill1time = 6;
        Instance.skill2time = 20;
        ifreturncamera = false;
        ifbrakecamera = true;
        ifhavebrake=false;
        animator=GameObject.Find("boss").GetComponent<Animator>();
        Instance.now = 0;
        realtime = 1f;
        Instance.nowtime = 1;
        thisAttackableObj = transform.parent.GetComponent<AttackableObj>();
        camera=GameObject.Find("Virtual Camera").GetComponent<Transform>();
    }

    private void Update()
    {
        if (Triggerenterbosslevel.Instance.ifbossmove)
        {
            realtime =realtime+Time.deltaTime;
            Instance.nowtime = Mathf.Round(realtime);
            if (Instance.nowtime > writetime)
            {
                hasattack = false;
            }
            if (Instance.nowtime > writetime)
            {
                writetime = nowtime;
            }
        }
        if (GameObject.Find("boss") != null)
        {
            if (Instance.nowtime % Instance.skill1time == 0 && (Boss.Instance.a == 18 || Boss.Instance.a == 19) && Instance.now==0 && !hasattack&& (!((Instance.nowtime % Instance.skill1time==0)&&(Instance.nowtime % Instance.skill2time==0))) )//技能1
            {
                Invoke("now1", 0.1f);
                animator.SetInteger("tomatoway",2);
                animator.SetInteger("leaf way", 1);
                Invoke("Returnnow", 2.1f);
                
                Vector3 force = PlayerController.Instance.transform.position - GameObject.Find("boss").transform.position;
                GameObject.Find("boss").GetComponent<Rigidbody>().AddForce(new Vector3(force.x,force.y,force.z), ForceMode.Acceleration);
            }
            if(Instance.now == 1&& (Boss.Instance.a == 18 || Boss.Instance.a == 19))
            {
                Followplayer(1.2f);
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "pro gaff"&& Instance.now == 2&&index==1)
        {
            GameObject.Find("boss").GetComponent<MonsterController>().hp -= 8;
            //GameObject.Find("boss").GetComponent<KnockDown>().enabled = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Instance.now == 1)
        {
            if (other.CompareTag("Fragile") || other.CompareTag("Morden"))
            {
                for (int i = 0; i < breakone.Length; i++)
                {
                    if (breakone[i] == null)
                    {
                        breakone[i] = other.gameObject;
                        break;
                    }
                }
                Invoke("destroy", 0.2f);
            }
        }
        if (other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            if (Instance.now == 1 && index == 0)
            {
                timer += Time.deltaTime;
                if (timer >= attackInterval)
                {
                    if (thisAttackableObj.gameObject.CompareTag("Player"))
                    {
                        (thisAttackableObj as PlayerModel).attack(other.GetComponent<Character>(), 0);
                        Instance.now = 0;
                        hasattack = true;
                    }
                    else
                    {
                        thisAttackableObj.attack(other.GetComponent<Character>(), 0);
                        Instance.now = 0;
                        hasattack = true;
                    }
                    //thisAttackableObj.Effect(0);
                    other.GetComponent<Character>().isAlive();
                    ChangeState();
                    Invoke("ChangeState", attackRecovery);
                    timer = 0.0f;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {

    }
    public IEnumerator EffectCooldown(Effect effect)
    {
        effect.isCoolingDown = true;
        yield return new WaitForSeconds(effect.frequency);
        effect.isCoolingDown = false;
    }
    public void ChangeState()
    {
        if (gameObject.transform.parent.CompareTag("Player"))
        {
            gameObject.transform.parent.GetComponent<PlayerController>().enabled = !gameObject.transform.parent.GetComponent<PlayerController>().enabled;
            print(gameObject.transform.parent.name + gameObject.transform.parent.GetComponent<PlayerController>().enabled);
        }
        if (gameObject.transform.parent.CompareTag("Monster"))
        {
            gameObject.transform.parent.GetComponent<MonsterController>().enabled = !gameObject.transform.parent.GetComponent<MonsterController>().enabled;
            print(gameObject.transform.parent.name + gameObject.transform.parent.GetComponent<MonsterController>().enabled);
        }
    }

    void destroy()
    {
        for (int i = 0; i < breakone.Length; i++)
        {
            if (breakone[i] != null)
            {
                breakone[i].SetActive(false);
                breakone[i] = null;
            }
        }
        CancelInvoke("destroy");
    }

    void Returnnow()
    {
        animator.SetInteger("tomatoway", 0);
        animator.SetInteger("leaf way", 0);
        Instance.now = 0;
        timer = 0;
    }

    void Followplayer()
    {
        Vector3 force = PlayerController.Instance.transform.position - GameObject.Find("boss").transform.position;
        GameObject.Find("boss").GetComponent<Rigidbody>().AddForce(force*10, ForceMode.Acceleration);
    }

    void Followplayer(float speed)
    {
        Vector3 force = PlayerController.Instance.transform.position - GameObject.Find("boss").transform.position;
        GameObject.Find("boss").GetComponent<Rigidbody>().AddForce(force *speed, ForceMode.Acceleration);
    }

    void now1()
    {
        CancelInvoke("now1");
        Instance.now = 1;
    }

    void now2()
    {
        Instance.now = 2;
    }
}
