using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CallPartner : MonoBehaviour
{
    [SerializeField]
    Armor[] partners;

    public Image cooldownBar; // 进度条

    int index;

    int coolingDownTime;

    [SerializeField]
    bool isCanUse;

    [SerializeField]
    Transform releasePos;

    [SerializeField]
    GameObject[] partnersPrefab;

    public float distance = 2.0f; // 生成预制件与玩家的距离
    public Vector3 direction = new Vector3(0, 0, 1);

    void Start()
    {
        // 初始化进度条
        cooldownBar.fillAmount = 0.0f;
        //StartCoroutine(CooldownRoutine());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            BeginAttack();
            
        }
    }

    private void BeginAttack()
    {
        if (PartnerButton.MyInstance.partner != null && isCanUse)
        {
            foreach (Armor partner in partners)
            {
                if (partner.GetType() == PartnerButton.MyInstance.partner.GetType())
                {
                    index = partner.id;
                    coolingDownTime = partner.coolingDown;
                    StartCoroutine(CoolingDown());
                    StartCoroutine(CooldownRoutine());
                    break;
                }
            }
            
            Vector3 spawnPosition = releasePos.position + direction.normalized * distance;
            Quaternion spawnRotation = releasePos.rotation;
            Instantiate(partnersPrefab[index], spawnPosition, spawnRotation);
        }
    }

    IEnumerator CoolingDown()
    {
        isCanUse = false;
        
        yield return new WaitForSeconds(coolingDownTime);

        isCanUse = true;
    }



    IEnumerator CooldownRoutine()
    {
        

        cooldownBar.enabled = true;
        cooldownBar.fillAmount = 1.0f;  // 初始化为1

        float elapsed = 0.0f;
        while (elapsed < coolingDownTime)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / coolingDownTime;
            cooldownBar.fillAmount = 1 - progress;  // 从1逐渐减少到0
            yield return null;
        }

        cooldownBar.enabled = false;
    }

    
}
