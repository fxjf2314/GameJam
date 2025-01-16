using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.VisualScripting;
public static class CrouchAndStand
{
    //存储当前协程
    public static Coroutine cameraCrouch;
    //下蹲时间
    public static float crouchTime;
    static CapsuleCollider cc;
    //在开启协程前检查是否有另一个协程正在进行
    public static void MyStartCoroutine(MonoBehaviour mono,ref CapsuleCollider ch,Vector3 ccFinalCenter, float ccFinalHeight, float ccFinalRadius, float time)
    {
        crouchTime = time;
        cc = ch;
        if (cameraCrouch != null)
        {
            //如果正在下蹲或起立则停止当前协程
            mono.StopCoroutine(cameraCrouch);
        }
        //开启新协程
        cameraCrouch = mono.StartCoroutine(Crouch(ccFinalCenter, ccFinalHeight, ccFinalRadius));
    }

    static IEnumerator Crouch(Vector3 ccFinalCenter, float ccFinalHeight, float ccFinalRadius)
    {
        Vector3 newCcCenter = cc.center;
        float currentCrouchTime = 0; // 记录已经蹲下的时间
        while (currentCrouchTime <= 1)
        {
            currentCrouchTime += Time.deltaTime/crouchTime; // 更新蹲下的时间
                                                            // 
                                                       //newCamPos.y = Mathf.Lerp(camOriginPos.y, camFinalPos.y, t);
            newCcCenter.y = Mathf.Lerp(cc.center.y, ccFinalCenter.y, currentCrouchTime);
            cc.radius = Mathf.Lerp(cc.radius, ccFinalRadius, currentCrouchTime);
            cc.height = Mathf.Lerp(cc.height, ccFinalHeight, currentCrouchTime);
            //更新center和cam位置
            cc.center = newCcCenter;
            //mainCam.transform.localPosition = newCamPos;
            yield return null;
        }

    }
}
