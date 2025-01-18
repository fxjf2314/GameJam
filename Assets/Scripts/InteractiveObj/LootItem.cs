using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItem : MonoBehaviour
{
    [SerializeField]
    private Item item;

    private Coroutine coroutine;
    public float moveSpeed = 1.0f; // 上下移动的速度
    public float rotateSpeed = 10.0f; // 旋转的速度
    public float moveDistance = 0.1f; // 上下移动的距离

    private Vector3 initialPosition; // 初始位置
    private Vector3 rotationCenter;  // 旋转的中心点

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            if (gameObject.transform.name == "exp(Clone)")
            {
                TalentTree.MyInstance.points++;
                Destroy(gameObject);
            }
            else
            {
                InventoryScript.MyInstance.AddItem((Armor)Instantiate(item));
                Destroy(gameObject);
            }
            
        }
    }

    void Start()
    {
       
        // 记录初始位置
        initialPosition = transform.position;

        // 计算旋转中心，使用对象的初始位置
        rotationCenter = initialPosition;

        // 启动协程
        coroutine = StartCoroutine(MoveAndRotate());
    }

    public IEnumerator MoveAndRotate()
    {
        while (true)
        {
            // 计算上下移动的偏移量
            float yOffset = moveDistance * Mathf.Sin(Time.time * moveSpeed);

            // 更新位置
            transform.position = new Vector3(initialPosition.x, initialPosition.y + yOffset, initialPosition.z);

            // 旋转对象围绕固定的世界坐标点的Y轴
            transform.RotateAround(rotationCenter, Vector3.up, rotateSpeed * Time.deltaTime);

            // 暂停协程，直到下一帧
            yield return null;
        }
    }

    public void StopRotation()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}
