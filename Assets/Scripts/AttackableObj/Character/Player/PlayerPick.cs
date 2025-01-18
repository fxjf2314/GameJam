using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPick : MonoBehaviour
{
    public Transform inventoryParent; // 玩家背包的 Transform，用于放置拾取的物品
    public float pickupSpeed = 5f; // 拾取速度

    private void OnTriggerEnter(Collider other)
    {
        // 检测进入触发器的是否是可拾取物品
        if (other.CompareTag("LootItem"))
        {
            LootItem lootItem = other.GetComponent<LootItem>();
            lootItem.StopRotation();
            StartCoroutine(PickupCoroutine(other.transform));
        }
    }

    private IEnumerator PickupCoroutine(Transform itemTransform)
    {
        // 计算物品到玩家背包的位置差
        Vector3 startPosition = itemTransform.position;
        Vector3 endPosition = inventoryParent.position;

        float timePassed = 0f;

        // 使用 Lerp 逐渐移动物品到玩家背包位置
        while (timePassed < 1f)
        {
            timePassed += Time.deltaTime * pickupSpeed;
            timePassed = Mathf.Clamp01(timePassed);
            itemTransform.position = Vector3.Lerp(startPosition, endPosition, timePassed);
            
            yield return null;
        }

        
    }
}
