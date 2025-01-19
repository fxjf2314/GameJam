using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.Windows;
//using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    static PlayerController mInstance;
    public static PlayerController Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<PlayerController>();
                // 如果场景中也没有，报错
                if (mInstance == null)
                {
                    Debug.LogError("未找到玩家实例");
                }
            }
            return mInstance;
        }
    }
    #region 角色移动相关
    //角色能否移动
    public bool playerCanMove;
    //控制移动、跳跃速度大小
    public float moveSpeed, jumpSpeed;
    public float finalMoveSpeed;
    float horizon;
    //判断角色是沿z轴移动还是沿x轴移动
    public bool isMoveOnZ;
    //判断钩锁协程是否完成
    public bool isRopeFinished;
    //用于地面判定
    Transform groundCheck;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public bool isGround;
    //胶囊碰撞箱、碰撞箱中心
    CapsuleCollider collider;
    Vector3 originCenter;
    //头部碰撞判断
    Transform headCheck;
    //下蹲高度，时间，高度差，站立高度
    [Range(0, 2)]
    public float crouchHeight;
    public float crouchTime;
    float standHeight;
    float heightDifference;
    float standRadius;
    float crouchRadius;
    //是否蹲下、能否起立、下一帧能否起立
    public bool isCrouch;
    bool isCanStand;
    bool nextFrameStand;
    //刚体
    public Rigidbody rb;
    #endregion
    //角色视角
    public CinemachineVirtualCamera virtualCamera;
    //动画
    Animator animator;

    
    private void Start()
    {
        //刚体
        rb = GetComponent<Rigidbody>();
        //胶囊碰撞箱
        collider = GetComponent<CapsuleCollider>();
        //动画播放器
        animator = GetComponent<Animator>();
        //移动速度
        finalMoveSpeed = moveSpeed;
        //下蹲、站立高度、半径
        standHeight = collider.height;
        standRadius = collider.radius;
        heightDifference = standHeight - crouchHeight;
        //头部、地面判定
        headCheck = transform.Find("HeadCheck");
        groundCheck = transform.Find("GroundCheck");
        //默认可以移动
        playerCanMove = true;
        isGround = true;
        //胶囊collider.height最小是radius的2倍,太小需要调整radius
        if (crouchHeight <= collider.radius * 2)
        {
            //半径是蹲下高度的一半
            crouchRadius = crouchHeight / 2;
        }
        else
        {
            //半径不变
            crouchRadius = standRadius;
        }
        heightDifference = standHeight - crouchHeight;
        originCenter = collider.center;
        isGround = true;
        //钩锁
        isRopeFinished = false;
        //移动方向
        ChangeMoveDir();
    }

    private void FixedUpdate()
    {
        MoveAndJump();
    }

    private void Update()
    {
        //更新下蹲状态
        IsCrouch();
        
    }

    void MoveAndJump()
    {
        isGround = Physics.CheckSphere(groundCheck.position, checkGroundRadius, groundLayer);

        if (playerCanMove)
        {
            OnMove();
            OnJump();
        }
    }
    void OnMove()
    {
        //获取移动键的输入使玩家移动
        horizon = Input.GetAxisRaw("Horizontal");
        //vercital = Input.GetAxis("Vertical") * finalMoveSpeed * Time.deltaTime;
        if (horizon == 0)
        {
            //没有按键输入时把z轴方向速度置0
            Vector3 newVelocity = rb.velocity;
            newVelocity.z = 0;
            newVelocity.x = 0;
            rb.velocity = newVelocity;
        }
        else
        {
            Move();
        }

        void Move()
        {
            Vector3 move;
            //计算z轴速度
            move = horizon * transform.forward;
            move.Normalize();
            move = move * finalMoveSpeed * Time.fixedDeltaTime * 100;
            if (isTurnBack())
            {
                //面向左边
                if (horizon > 0)
                {
                    //向右走转身
                    transform.Rotate(Vector3.up, 180);
                }
                else if (horizon < 0)
                {
                    move = -move;
                }
            }
            else
            {
                //面朝右边向左走
                if (horizon < 0)
                {
                    //转身
                    transform.Rotate(Vector3.up, 180);
                    move = -move;
                }
            }
            //继承y轴速度
            move.y = rb.velocity.y;
            rb.velocity = move;

            bool isTurnBack()
            {
                if (isMoveOnZ)
                {
                    return transform.forward.z < 0;
                }
                else
                {
                    return transform.forward.x > 0;
                }
            }
        }
    }
    public void ChangeMoveDir()//改变移动轴
    {
        if (isMoveOnZ)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            rb.constraints = RigidbodyConstraints.FreezePositionX;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
        }
        rb.freezeRotation = true;
    }
    void OnJump()
    {
        Vector3 velocity = new Vector3();
        if (isGround)
        {
            //重置跳跃高度
            velocity.y = 0;
            //PlayerItemCheck.Instance.jumpCount = 0;
        }
        if (Input.GetButton("Jump") && !isCrouch && isGround)
        {
            velocity.y += jumpSpeed * 10;
            rb.AddForce(velocity);
            //PlayerItemCheck.Instance.jumpCount++;
            //velocity.y = 0;
            //SelectAni(JumpAni.JumpStart);
        }
        //SwitchAni();
    }

    void IsCrouch()
    {
        //处于蹲下状态时才更新isCanStand
        if (isCrouch) isCanStand = IsCanStand();
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isCrouch)//不处于下蹲状态时按蹲键
        {
            isCrouch = true;
            //速度减半
            finalMoveSpeed = moveSpeed / 2;
            //存储碰撞箱最终位置
            Vector3 ccFinalCenter = collider.center - new Vector3(0, heightDifference / 2, 0);
            //开启协程
            //传入mono类，碰撞箱，最终center位置，最终height高度，最终半径
            CrouchAndStand.MyStartCoroutine(this, ref collider, ccFinalCenter, crouchHeight, crouchRadius, crouchTime);
            //播放下蹲动画
            MushRoomAnimationChange.SwitchCrouchAni(animator);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))//松开蹲键时判断能否起立
        {
            if (isCanStand)
            {
                isCrouch = false;
                //速度恢复
                finalMoveSpeed = moveSpeed;
                //摄像机视角恢复,碰撞箱高度恢复（平滑进行）
                //传入mono类，碰撞箱，最终center位置，最终height高度，最终半径
                CrouchAndStand.MyStartCoroutine(this, ref collider, originCenter, standHeight, standRadius, crouchTime);
                //播放起立动画
                MushRoomAnimationChange.SwitchCrouchAni(animator);
            }
            else
            {
                nextFrameStand = true;//下一帧起立
            }
        }
        else if (isCrouch && isCanStand && nextFrameStand)
        {
            isCrouch = false;
            nextFrameStand = false;
            finalMoveSpeed = moveSpeed;
        }
    }

    bool IsCanStand()
    {
        Collider[] colliders = Physics.OverlapBox(headCheck.position, headCheck.localScale / 2);
        foreach (Collider collider in colliders)
        {
            //忽略角色自身和所有子集碰撞体,忽略SceneTrigger
            if (!collider.transform.IsChildOf(transform) && collider.gameObject.layer != LayerMask.NameToLayer("SceneTrigger"))
            {
                return false;
            }
        }
        return true;
    }


    
}
    

