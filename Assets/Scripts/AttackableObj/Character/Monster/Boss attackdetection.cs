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
    private float nowtime;
    private bool ifbrakecamera = false;
    private bool ifhavebrake = false;
    bool ifreturncamera = false;
    Transform camera;
    public float skill1time = 0;
    public float skill2time = 0;

    private void Start()
    {
        skill1time = 7;
        skill2time = 20;
        ifreturncamera = false;
        ifbrakecamera = true;
        ifhavebrake=false;
        animator=GameObject.Find("boss").GetComponent<Animator>();
        now = 0;
        realtime = 1f;
        nowtime = 1;
        thisAttackableObj = transform.parent.GetComponent<AttackableObj>();
        camera=GameObject.Find("Virtual Camera").GetComponent<Transform>();
    }

    private void Update()
    {
        /*
        if (ifbrakecamera)
        {
            Vector3 rotatedir = new Vector3();
            float brakespeed = 30f;
            if (!ifreturncamera)
            {
                if (camera.eulerAngles.x < 30)
                {
                    rotatedir = camera.eulerAngles;
                    rotatedir.x += Time.deltaTime * brakespeed;
                    //rotatedir.y += Time.deltaTime * brakespeed;
                    rotatedir.z += Time.deltaTime * brakespeed;
                    camera.eulerAngles = rotatedir;
                }
                else
                {
                    ifreturncamera = true;
                }
            }
            if (ifreturncamera)
            {
                if(camera.eulerAngles.x > 8.892f&& camera.eulerAngles.z>0)
                {
                    rotatedir = camera.eulerAngles;
                    rotatedir.x -= Time.deltaTime * brakespeed;
                    //rotatedir.y -= Time.deltaTime * brakespeed;
                    rotatedir.z -= Time.deltaTime * brakespeed;
                    camera.eulerAngles = rotatedir;
                }
                else
                {
                    ifreturncamera=false;
                    camera.eulerAngles = aimrotate;
                    ifhavebrake = true;
                    ifbrakecamera = false;
                }
            }
           
        }
        */
        if (Triggerenterbosslevel.Instance.ifbossmove)
        {
            realtime =realtime+Time.deltaTime;
            nowtime=Mathf.Round(realtime);
        }
        if (GameObject.Find("boss") != null)
        {
            if (nowtime % skill1time == 0 && (Boss.Instance.a == 18 || Boss.Instance.a == 19) && now==0)//技能1
            {
                Invoke("now1", 0.1f);
                animator.SetInteger("tomatoway",2);
                animator.SetInteger("leaf way", 1);
                Invoke("Returnnow", 2.1f);
                
                Vector3 force = PlayerController.Instance.transform.position - GameObject.Find("boss").transform.position;
                GameObject.Find("boss").GetComponent<Rigidbody>().AddForce(force, ForceMode.Acceleration);
            }
            if (nowtime % skill2time == 0 && (Boss.Instance.a == 18 || Boss.Instance.a == 19) && now == 0)//技能2
            {
                Invoke("now2",0.9f);
                Vector3 force = PlayerController.Instance.transform.position - GameObject.Find("boss").transform.position;
                force.y = 10f;
                animator.SetInteger("tomatoway", 4);
                animator.SetInteger("leaf way", 2);
                GameObject.Find("boss").GetComponent<Rigidbody>().AddForce(force * 0.1f, ForceMode.Acceleration);
                Invoke("Returnnow", 2.1f);
            }
            if(now==1&& (Boss.Instance.a == 18 || Boss.Instance.a == 19))
            {
                Followplayer(0.1f);
            }
            if(now == 2 && (Boss.Instance.a == 18 || Boss.Instance.a == 19))
            {
                Vector3 force = PlayerController.Instance.transform.position - GameObject.Find("boss").transform.position;
                force.y = -50f;
                GameObject.Find("boss").GetComponent<Rigidbody>().AddForce(force * 0.1f, ForceMode.Acceleration);
                if (GameObject.Find("boss").GetComponent<Transform>().position.y < 1&&!ifhavebrake)
                {
                    ifbrakecamera = true;
                    ifhavebrake = true;
                }
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            {
                for (int i = 0; i < thisAttackableObj.skillList[index].effects.Count; i++)
                    StartCoroutine(EffectCooldown(thisAttackableObj.skillList[index].effects[i]));
            }
        }
        if (other.tag == "pro gaff"&&now==2&&index==1)
        {
            GameObject.Find("boss").GetComponent<MonsterController>().hp -= 8;
            //GameObject.Find("boss").GetComponent<KnockDown>().enabled = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (now == 1)
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
        if (now == 2)
        {
            if (other.CompareTag("Fragile") || other.CompareTag("Morden")||other.CompareTag("Hard"))
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
            if (now == 1 && index == 0)
            {
                timer += Time.deltaTime;
                if (timer >= attackInterval)
                {
                    if (thisAttackableObj.gameObject.CompareTag("Player"))
                    {
                        (thisAttackableObj as PlayerModel).attack(other.GetComponent<Character>(), 0);
                        now = 0;
                    }
                    else
                    {
                        thisAttackableObj.attack(other.GetComponent<Character>(), 0);
                        now = 0;
                    }
                    //thisAttackableObj.Effect(0);
                    other.GetComponent<Character>().isAlive();
                    ChangeState();
                    Invoke("ChangeState", attackRecovery);
                    timer = 0.0f;
                }
            }
            if (now == 2 && index == 1) 
            {
                timer += Time.deltaTime;
                if (timer >= attackInterval)
                {
                    if (thisAttackableObj.gameObject.CompareTag("Player"))
                    {
                        (thisAttackableObj as PlayerModel).attack(other.GetComponent<Character>(), 1);
                    }
                    else
                    {
                        thisAttackableObj.attack(other.GetComponent<Character>(), 1);
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
        if (other.CompareTag("Player") || other.CompareTag("Monster"))
        {
            for (int i = 0; i < thisAttackableObj.skillList[index].effects.Count; i++)
                StopCoroutine(EffectCooldown(thisAttackableObj.skillList[index].effects[i]));
        }
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
        now = 0;
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
        now = 1;
    }

    void now2()
    {
        now = 2;
    }
}
