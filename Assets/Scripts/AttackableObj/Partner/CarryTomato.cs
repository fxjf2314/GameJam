using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CarryTomato : Partner
{
    Rigidbody body;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float duration;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        if(PlayerController.Instance.isMoveOnZ == true)
        {
            body.constraints = RigidbodyConstraints.FreezePositionX;
        }
        else
        {
            body.constraints = RigidbodyConstraints.FreezePositionZ;
        }
        StartCoroutine(RollForwardRoutine());
        //Attack();
    }

    protected override void Attack()
    {
        if(gameObject.transform.rotation.y <= 0 )
        {
            
        }
        else
        {
            body.velocity = new Vector3(0, 0, -speed);
        }
        
    }

    IEnumerator RollForwardRoutine()
    {
        float elapsedTime = 0.0f;

        
        while (elapsedTime < duration)
        {
            // 设置Rigidbody的velocity，使其向前滚动
            body.velocity = transform.forward * speed;
            
            elapsedTime += Time.deltaTime;  // 更新已滚动时间
            yield return null;  // 暂停协程，直到下一帧
        }
        // 滚动结束后，将velocity设置为0，停止滚动
        body.velocity = Vector3.zero;
        Destroy(gameObject);
        // 可以在这里添加停止滚动后的逻辑
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Monster"))
        {
            attack(collision.transform.GetComponent<Character>(), 0);
            StopCoroutine(RollForwardRoutine());
            collision.gameObject.GetComponent<Character>().isAlive();
            Destroy(gameObject);
        }
    }
}
