using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum JumpAni
{
    JumpStart,
    JumpToFall,
    FallToFinal
}

public static class AnimationChange
{
    static void SelectAni(JumpAni jumpAni, Animator animator)
    {
        switch (jumpAni)
        {
            case JumpAni.JumpStart:
                {
                    animator.SetBool("isJump", true);
                }
                break;
            case JumpAni.JumpToFall:
                {
                    animator.SetBool("isFall", true);
                }
                break;
            case JumpAni.FallToFinal:
                {
                    animator.SetBool("isJump", false);
                    animator.SetBool("isFall", false);
                }
                break;
            default: Debug.Log("未能找到对应枚举"); break;
        }
    }


    static void SwitchAni(Animator animator)
    {

        if (!PlayerController.Instance.isGround)
        {
            //如果玩家正在下落且isFall == false就播放下落动画
            if (PlayerController.Instance.rb.velocity.y < 0 && !animator.GetBool("isFall"))
            {
                SelectAni(JumpAni.JumpToFall, animator);
            }
        }
        if (animator.GetBool("isFall"))
        {
            //落到地面就播放结束动画
            if (PlayerController.Instance.isGround)
            {
                SelectAni(JumpAni.FallToFinal, animator);
            }
        }
    }
}
