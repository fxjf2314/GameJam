using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public CinemachineVirtualCamera virtualCamera;
    CinemachineFramingTransposer framingTransposer;
    Transform followPoint;
    [SerializeField]
    float ViewChangeAfterOpenCGDuringTime;
    [SerializeField]
    Vector3 targetPos;
    [SerializeField]
    Vector3 targetRot;
    public static CameraFollow instance;
    

    private void Awake()
    {
        instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        framingTransposer = PlayerController.Instance.virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        player = GameObject.Find("Player").transform;
        // 创建一个空对象作为摄像机的跟随点
        followPoint = new GameObject("FollowPoint").transform;
        followPoint.position = player.position;
    }

    private void FixedUpdate()
    {
        followPoint.position = player.position;
    }

    public void MyStartCoroutine()
    {
        StartCoroutine(ViewChangeAfterOpenCG());
    }

    IEnumerator ViewChangeAfterOpenCG()
    {
        yield return new WaitForSeconds(0.5f);
        virtualCamera.m_LookAt = null;
        virtualCamera.m_Follow = null;

        float time = 0;
        var pos = transform.position;
        var rot = transform.rotation;
        //float offX, offY, offZ;
        while (time < ViewChangeAfterOpenCGDuringTime)
        {
            transform.position = Vector3.Lerp(pos, targetPos, time / ViewChangeAfterOpenCGDuringTime);
            transform.rotation = Quaternion.Lerp(rot, Quaternion.Euler(targetRot), time / ViewChangeAfterOpenCGDuringTime);
            yield return null;
            time += Time.deltaTime;
        }

        PlayerController.Instance.playerCanMove = true;
        // 将跟随点设置为虚拟相机的Follow目标
        virtualCamera.m_Follow = followPoint;
    }
}
