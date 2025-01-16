using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    //滑翔相关
    public bool isGetGlideItem;
    public float glideSpeed;
    public float glideFallingSpeed;
    public bool isCanGlide;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isCanGlide = false;
        isGetGlideItem = true;

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

    //血量增减
    void MaxHpUp(int increasementHp)
    {
        PlayerModel.Instance.maxHp += increasementHp;
        PlayerModel.Instance.hp += increasementHp;
    }
    void MaxHpDown(int decreasementHp)
    {
        PlayerModel.Instance.maxHp -= decreasementHp;
        PlayerModel.Instance.hp -= decreasementHp;
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
