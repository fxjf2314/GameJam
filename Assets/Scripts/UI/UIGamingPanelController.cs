using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGamingPanelController : MonoBehaviour
{
    Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = TransformFind.TransformFindChild(transform, "HealthBar").GetComponent<Slider>();
        healthBar.maxValue = PlayerModel.Instance.maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthBar.value = PlayerModel.Instance.hp;
    }
}
