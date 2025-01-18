using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public CinemachineVirtualCamera virtualCamera;
    Transform followPoint;
    public static CameraFollow instance;

    private void Awake()
    {
        instance = this;
        // 创建一个空对象作为摄像机的跟随点
        followPoint = new GameObject("FollowPoint").transform;
        followPoint.position = player.position;
        
        // 将跟随点设置为虚拟相机的Follow目标
        virtualCamera.m_Follow = followPoint;
    }

    private void FixedUpdate()
    {
        followPoint.position = player.position;
    }
}
