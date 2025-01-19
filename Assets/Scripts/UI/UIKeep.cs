using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIKeep : MonoBehaviour
{

    GameObject ui;
    GameObject DefenseParticle;
    private void Awake()
    {
        DefenseParticle = GameObject.Find("DefenseParticle");
        ui = GameObject.Find("UI");
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(ui);
        DontDestroyOnLoad(DefenseParticle);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("RealGame");
        }   
    }
}
