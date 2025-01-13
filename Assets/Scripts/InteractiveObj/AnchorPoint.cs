using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnchorPoint : MonoBehaviour
{
    public GameObject playerPos; // 玩家的位置
    //public Button ropeButton; // 绳索按钮
    public Rigidbody playerRigidbody; // 玩家的Rigidbody组件
    public float duration = 1.0f; // 绳索移动的持续时间
    public float initialForce = 5.0f; // 初始力的大小
    public float forceIncreaseRate = 10.0f; // 力增加的速率
    

    private bool isFinished;//判定动作是否完成
    private bool isActive = false;
    private void Update()
    {
        //Debug.Log(playerPos.position);
        // 检查玩家是否在锚点的有效范围内
        if (Mathf.Abs(transform.position.z - playerPos.transform.position.z) <= 5 && playerPos.transform.position.z < transform.position.z)
        {
            isActive = true;
            transform.GetChild(0).gameObject.SetActive(true);
            //ropeButton.interactable = true;
        }
        else
        {
            isActive = false;
            transform.GetChild(0).gameObject.SetActive(false);
            //ropeButton.interactable = false;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            isCanUse();
        }
        if (isFinished == true && PlayerController.Instance.isGround)
        {
            PlayerController.Instance.playerCanMove = true;
        }

    }

    private void Start()
    {
        
        // 注册绳索按钮的点击事件
        //ropeButton.onClick.AddListener(() => isCanUse());
        isFinished = true;
        playerRigidbody = playerPos.GetComponent<Rigidbody>();
    }

    private void isCanUse()
    {
        if(isActive == true)
        {
            StartCoroutine(RopeMove());
            isFinished = false;
            PlayerController.Instance.playerCanMove = false;
        }
        

    }

    private IEnumerator RopeMove()
    {
        playerRigidbody.useGravity = false;
        playerRigidbody.velocity = Vector3.zero;
        float elapsedTime = 0.0f; // 已用时间
        Vector3 moveDir = (transform.position - playerPos.transform.position).normalized; // 移动方向

        while (elapsedTime < duration)
        {
            // 计算当前的力，随着时间逐渐增加
            float currentForce = initialForce + (elapsedTime / duration) * forceIncreaseRate;
            // 朝向锚点方向施加力
            playerRigidbody.AddForce(moveDir * currentForce, ForceMode.Acceleration);

            // 更新已用时间
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保玩家最终到达锚点位置
        //playerPos.position = transform.position;
        playerRigidbody.useGravity = true;
        // 停止玩家的移动
        //playerRigidbody.velocity = Vector3.zero;
        isFinished = true;
    }

    
}