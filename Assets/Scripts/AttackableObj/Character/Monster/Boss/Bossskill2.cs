using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Bossskill2 : MonoBehaviour
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
    //public float nowtime;
    private bool ifbrakecamera = false;
    private bool ifhavebrake = false;
    bool ifreturncamera = false;
    Transform camera;
    //public float skill1time = 0;
    //public float skill2time = 0;
    bool hasattack = false;
    float writetime = 0;

    private void Start()
    {
        writetime = 0;
        hasattack = false;
        Bossattackdetection.Instance.skill1time = 6;
        Bossattackdetection.Instance.skill2time = 20;
        ifreturncamera = false;
        ifbrakecamera = false;
        ifhavebrake = false;
        animator = GameObject.Find("boss").GetComponent<Animator>();
        Bossattackdetection.Instance.now = 0;
        realtime = 1f;
        Bossattackdetection.Instance.nowtime = 1;
        thisAttackableObj = transform.parent.GetComponent<AttackableObj>();
        camera = GameObject.Find("Virtual Camera").GetComponent<Transform>();
    }

    private void Update()
    {
        
        if (ifbrakecamera)
        {
            Vector3 rotatedir = new Vector3();
            float brakespeed = 150f;
            if (!ifreturncamera)
            {
                if (camera.eulerAngles.x < 20)
                {
                    rotatedir = camera.eulerAngles;
                    rotatedir.x += Time.deltaTime * brakespeed;
                    //rotatedir.y += Time.deltaTime * brakespeed;
                    //rotatedir.z += Time.deltaTime * brakespeed;
                    camera.eulerAngles = rotatedir;
                }
                else
                {
                    ifreturncamera = true;
                }
            }
            if (ifreturncamera)
            {
                if(camera.eulerAngles.x > 8.892f)
                {
                    rotatedir = camera.eulerAngles;
                    rotatedir.x -= Time.deltaTime * brakespeed;
                    //rotatedir.y -= Time.deltaTime * brakespeed;
                    //rotatedir.z -= Time.deltaTime * brakespeed;
                    camera.eulerAngles = rotatedir;
                }
                else
                {
                    ifreturncamera=false;
                    camera.eulerAngles = aimrotate;
                    ifhavebrake = false;
                    ifbrakecamera = false;
                }
            }
           
        }
        
        if (Triggerenterbosslevel.Instance.ifbossmove)
        {
            realtime = realtime + Time.deltaTime;
            Bossattackdetection.Instance.nowtime = Mathf.Round(realtime);
            if (Bossattackdetection.Instance.nowtime > writetime)
            {
                hasattack = false;
            }
            if (Bossattackdetection.Instance.nowtime > writetime)
            {
                writetime = Bossattackdetection.Instance.nowtime;
            }
        }
        if (GameObject.Find("boss") != null)
        {
            if (Bossattackdetection.Instance.nowtime % Bossattackdetection.Instance.skill2time == 0 && (Boss.Instance.a == 18 || Boss.Instance.a == 19) && Bossattackdetection.Instance.now == 0 && !hasattack)//技能2
            {
                Invoke("now2", 1.1f);
                Vector3 force = PlayerController.Instance.transform.position - GameObject.Find("boss").transform.position;
                force.y = 90f;
                animator.SetInteger("tomatoway", 4);
                animator.SetInteger("leaf way", 2);
                GameObject.Find("boss").GetComponent<Rigidbody>().AddForce(force * 0.1f, ForceMode.Acceleration);
                Invoke("Returnnow", 2.1f);
            }
            if (Bossattackdetection.Instance.now == 2 && (Boss.Instance.a == 18 || Boss.Instance.a == 19))
            {
                Vector3 force = PlayerController.Instance.transform.position - GameObject.Find("boss").transform.position;
                force.y = -50f;
                GameObject.Find("boss").GetComponent<Rigidbody>().AddForce(force * 0.1f, ForceMode.Acceleration);
                if (GameObject.Find("boss").GetComponent<Transform>().position.y <= 1 && !ifhavebrake)
                {
                    ifbrakecamera = true;
                    ifhavebrake = true;
                }
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "pro gaff" && Bossattackdetection.Instance.now == 2 && index == 1)
        {
            GameObject.Find("boss").GetComponent<MonsterController>().hp -= 8;
            //GameObject.Find("boss").GetComponent<KnockDown>().enabled = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Bossattackdetection.Instance.now == 2)
        {
            if (other.CompareTag("Fragile") || other.CompareTag("Morden") || other.CompareTag("Hard"))
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
            if (Bossattackdetection.Instance.now == 2 && index == 1)
            {
                timer += Time.deltaTime;
                if (timer >= attackInterval)
                {
                    if (thisAttackableObj.gameObject.CompareTag("Player"))
                    {
                        (thisAttackableObj as PlayerModel).attack(other.GetComponent<Character>(), 1);
                        Bossattackdetection.Instance.now = 0;
                        hasattack = true;
                    }
                    else
                    {
                        thisAttackableObj.attack(other.GetComponent<Character>(), 1);
                        Bossattackdetection.Instance.now = 0;
                        hasattack = true;
                    }
                    //thisAttackableObj.Effect(1);
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
        Bossattackdetection.Instance.now = 0;
        timer = 0;
    }

    void Followplayer()
    {
        Vector3 force = PlayerController.Instance.transform.position - GameObject.Find("boss").transform.position;
        GameObject.Find("boss").GetComponent<Rigidbody>().AddForce(force * 10, ForceMode.Acceleration);
    }

    void Followplayer(float speed)
    {
        Vector3 force = PlayerController.Instance.transform.position - GameObject.Find("boss").transform.position;
        GameObject.Find("boss").GetComponent<Rigidbody>().AddForce(force * speed, ForceMode.Acceleration);
    }

    void now1()
    {
        Bossattackdetection.Instance.now = 1;
    }

    void now2()
    {
        CancelInvoke("now2");
        Bossattackdetection.Instance.now = 2;
    }
}
