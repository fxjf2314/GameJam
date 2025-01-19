using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerPickSound : MonoBehaviour
{
    private static PlayerPickSound instance;
    public static PlayerPickSound MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<PlayerPickSound>();
            }

            return instance;
        }
    }

    public AudioClip pickupSound; // 拾取音效
    private AudioSource audioSource; // 音频源

    private void Start()
    {
        // 添加音频源组件
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // 不在启动时自动播放
    }

    // 播放音效的方法
    public void PlayPickupSound()
    {
        audioSource.clip = pickupSound;
        audioSource.Play();
    }
}
