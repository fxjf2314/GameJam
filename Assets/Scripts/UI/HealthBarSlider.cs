using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSlider : MonoBehaviour
{
    static HealthBarSlider instance;

    public static HealthBarSlider Instance { get => instance;}
    Slider healthBar;

    void Start()
    {
        healthBar = transform.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = PlayerModel.Instance.hp;
    }
}
