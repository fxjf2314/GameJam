using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenHood : MonoBehaviour
{
    //玩家的刚体
    Rigidbody rb;
    //抽油烟机是否开启
    bool isOpen;
    //摄像机 
    CinemachineVirtualCamera virtualCamera;
    CinemachineFramingTransposer framingTransposer;
    Coroutine coroutine;
    //事件订阅
    public Transform hoodSwitch;
    KitchenHoodSwitch kitchenHoodSwitch;

    void Start()
    {
        virtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        isOpen = true;
        kitchenHoodSwitch = hoodSwitch.GetComponent<KitchenHoodSwitch>();
        kitchenHoodSwitch.SwitchEvent += SwitchEventListener;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isOpen && other.CompareTag("Player"))
        {
            rb = other.GetComponent<Rigidbody>();
            //模拟反重力
            rb.useGravity = false;
            rb.velocity = new Vector3(0, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * 2000);
            //rb.velocity = new Vector3(0,10,rb.velocity.z);
            MyStartCoroutine(framingTransposer.m_TrackedObjectOffset.y, -11, 3);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(isOpen && other.CompareTag("Player"))
        {
            rb.velocity = new Vector3(0, 0, rb.velocity.z);
            rb.useGravity = true;
            rb = null;
            MyStartCoroutine(framingTransposer.m_TrackedObjectOffset.y, 48, 2);
        }
    }

    void MyStartCoroutine(float originPos, float finalPos, float duringTime)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(CameraPosChange(originPos,finalPos,duringTime));
    }

    //改变相机视角
    IEnumerator CameraPosChange(float originPos, float finalPos, float duringTime)
    {
        float time = 0;
        while(time < duringTime)
        {
            framingTransposer.m_TrackedObjectOffset.y = Mathf.Lerp(originPos, finalPos, time);
            time += Time.deltaTime;
            yield return null;
        }
        coroutine = null;
    }

    void SwitchEventListener()
    {
        isOpen = false;
        this.enabled = false;
    }

}
