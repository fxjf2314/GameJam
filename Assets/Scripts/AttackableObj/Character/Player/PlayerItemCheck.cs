using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemCheck : MonoBehaviour,IDataPersistence
{
    static PlayerItemCheck mInstance;
    public static PlayerItemCheck Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<PlayerItemCheck>();
                // 如果场景中也没有，报错
                if (mInstance == null)
                {
                    Debug.LogError("未找到玩家实例");
                }
            }
            return mInstance;
        }
    }

    //鸡蘑菇
    //滑翔相关
    public bool isGetGlideItem;
    public float glideSpeed;
    public float glideFallingSpeed;
    public bool isCanGlide;
    Rigidbody rb;
    //土豆蘑菇相关
    public bool canDefense;
    public bool potatoMashroom;
    public Image cooldownBar;
    public GameObject defense;
    public GameObject defenseParticle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cooldownBar = GameObject.Find("DefenseCoolingDown").GetComponent<Image>();
        defense = GameObject.Find("DefenseIcon");
        defenseParticle = GameObject.Find("DefenseParticle");
        isCanGlide = false;
        isGetGlideItem = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        OnGlide();
    }

    private void Update()
    {
        NewMethod();
    }
    //拿到滑翔装备时，检测滑翔输入
    private void NewMethod()
    {
        if(isGetGlideItem)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isCanGlide = true;
            }
            else if (PlayerController.Instance.isGround)
            {
                isCanGlide = false;
            }
        }
    }

    void OnGlide()
    {
        if (isCanGlide && Input.GetKey(KeyCode.Space) && !PlayerController.Instance.isGround && !PlayerController.Instance.isCrouch && gameObject.transform.position.y >= 3)
        {
            //解除移动输入限制，确保用钩锁后可以滑翔
            PlayerController.Instance.playerCanMove = true;
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
            Vector3 glideDirection = Vector3.forward;
            glideDirection = transform.TransformDirection(glideDirection);
            glideDirection *= glideSpeed;
            glideDirection.y = rb.velocity.y;
            rb.velocity = glideDirection;
            rb.velocity += Vector3.down * 30 * Time.deltaTime;
        }
    }

    public void startPotatoCooling()
    {
        defenseParticle.gameObject.SetActive(false);
        StartCoroutine(DefenseCooling());
        StartCoroutine(CooldownRoutine());
        defenseParticle.transform.position = transform.position + Vector3.up * 1;
        defenseParticle.gameObject.SetActive(true);
    }

    //血量增减
    public void MaxHpUp(int increasementHp)
    {
        HealthBarController.Instance.AddMaxHp(increasementHp);
        PlayerModel.Instance.hp += increasementHp;
    }
    public void MaxHpDown(int decreasementHp)
    {
        PlayerModel.Instance.maxHp -= decreasementHp;
        PlayerModel.Instance.hp -= decreasementHp;
    }
    public void SpeedUp(int increaseSpeed)
    {
        PlayerController.Instance.finalMoveSpeed += increaseSpeed;
    }
    public void JumpSpeedUp(int increaseJumpSpeed)
    {
        PlayerController.Instance.jumpSpeed += increaseJumpSpeed;
    }


    IEnumerator DefenseCooling()
    {
        canDefense = false;

        yield return new WaitForSeconds(60);

        if(potatoMashroom)
        {
            canDefense = true;
        }
        
    }

    IEnumerator CooldownRoutine()
    {

        defense.GetComponentInParent<CanvasGroup>().alpha = 1.0f;
        cooldownBar.enabled = true;
        cooldownBar.fillAmount = 1.0f;  // 初始化为1

        float elapsed = 0.0f;
        while (elapsed < 60.0f)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / 60.0f;
            cooldownBar.fillAmount = 1 - progress;  // 从1逐渐减少到0
            yield return null;
        }

        cooldownBar.enabled = false;
        defense.GetComponentInParent<CanvasGroup>().alpha = 0.0f;
   }


    public void SaveData(ref GameData gameData)
    {
        gameData.isGetGlideItem = isGetGlideItem;
    }

    public void LoadData(GameData gameData)
    {
        isGetGlideItem = gameData.isGetGlideItem;
    }
}
