using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class MonsterController : Character
{
    protected Transform player;
    protected Rigidbody monsterRb;
    [Header("索敌半径")][SerializeField]
    float maxDis;
    [Header("索敌半径")]
    [SerializeField]
    float minDis;
    [Header("跳跃速度")]
    [SerializeField]
    float jumpSpeed;
    [Header("跳跃间隔时间")]
    [SerializeField]
    float jumpInterval = 5;
    [Header("移动速度")]
    [SerializeField]
    float moveSpeed = 10;

    //存储跳跃协程，只开一个
    Coroutine monsterJump;

    protected virtual void Start()
    {
        player = GameObject.Find("Player").transform;
        monsterRb = GetComponent<Rigidbody>();
    }
    
    public virtual void Update()
    {
        //发送移动指令
        if (gameObject.name != "boss")
        {
            MoveToPlayer();
        }
    }
    void MoveToPlayer()
    {
        if (player != null)
        {
            //玩家和monster距离过近就会追击玩家
            if (Vector3.Distance(player.position, transform.position) < maxDis && Vector3.Distance(player.position, transform.position) > minDis)
            {
                HorizonMove();
                if(monsterJump == null)
                {
                    monsterJump = StartCoroutine(VerticalMove(jumpInterval));
                }
            }
            else
            {
                monsterRb.velocity = Vector3.zero;
            }

        }
    }

    void HorizonMove()
    {
        Vector3 subPos = player.position - transform.position;
        if (PlayerController.Instance.isMoveOnZ)
        {
            if (transform.forward.z * subPos.z < 0 && Vector3.Distance(transform.position,player.position) > 10 )
            {
                //转身面向玩家
                transform.RotateAround(transform.position, Vector3.up, 180);
            }
            if (transform.position.z > player.position.z)
            {
                monsterRb.velocity = new Vector3(0, monsterRb.velocity.y, -moveSpeed);
            }
            else
            {
                monsterRb.velocity = new Vector3(0, monsterRb.velocity.y, moveSpeed);
            }
        }
        else
        {
            if (transform.forward.x * subPos.x < 0 && Vector3.Distance(transform.position, player.position) > 10)
            {
                //转身面向玩家
                transform.Rotate(Vector3.up, 180);
            }
            if (transform.position.x > player.position.x)
            {
                monsterRb.velocity = new Vector3(-moveSpeed, monsterRb.velocity.y, 0);
            }
            else
            {
                monsterRb.velocity = new Vector3(moveSpeed, monsterRb.velocity.y, 0);
            }
        }
    }

    IEnumerator VerticalMove(float intreval)
    {
        yield return new WaitForSeconds(intreval);
        //Debug.Log("11");
        monsterRb.AddForce(Vector3.up * jumpSpeed *10);
        monsterJump = null;
    }
}
