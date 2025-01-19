using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game1audio : MonoBehaviour
{
    AudioSource audioSource;
    bool a=false;
    // Start is called before the first frame update
    void Start()
    {
        a = true;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (a)
        {
            Invoke("playaudio", 4);
        }
        
    }

    void playaudio()
    {
        CancelInvoke("playaudio");
        a= false;
        audioSource.Play();
    }
}
