using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class MonsterController : Character
{
    Transform player;
    Rigidbody monsterRb;
    Collider monsterCollider;
    //寻路导航
    //NavMeshAgent agent;
    private void Start()
    {
        //transform.AddComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        monsterRb = GetComponent<Rigidbody>();
        monsterCollider = GetComponent<Collider>();
    }

    private void Update()
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
            //玩家和slime距离过近就会追击玩家
            if (Vector3.Distance(player.position, transform.position) < 3)
            {
                Vector3 force = player.position - transform.position;
                // force.x = 0;
                //force.y = 0;
                monsterRb.AddForce(force, ForceMode.Acceleration);
                //agent.SetDestination(player.position);
                //agent.enabled = true;
            }
            else
            {
                //agent.SetDestination(slime.position);
                //agent.enabled= false;
            }
        }
    }
}
