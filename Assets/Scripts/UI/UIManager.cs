using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [SerializeField]
    private GameObject tooltip;

    private TextMeshProUGUI tooltipDes;

    private TextMeshProUGUI tooltipType;

    [SerializeField]
    private UIFridge uiFridge;

    private Image tooltipIcon;

    private void Awake()
    {
        //工具提示类别
        tooltipType = tooltip.GetComponentInChildren<TextMeshProUGUI>();
        //工具提示图标
        Transform desIcon = tooltip.transform.GetChild(0);
        tooltipIcon = desIcon.GetComponentInChildren<Image>();
        //工具提示详细
        Transform desChild = tooltip.transform.GetChild(1);
        tooltipDes = desChild.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            uiFridge.OpenClose();
        }
    }

    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<UIManager>();
            }

            return instance;
        }
    }

    public void UpdateStackSize(IClickable clickable)
    {
        if (clickable.MyCount > 1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
            clickable.MyIcon.color = Color.white;
        }
        if(clickable.MyCount == 0)
        {
            clickable.MyIcon.color = new Color(0,0,0,0);
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
            
        }
    }

    public void ShowToolTip(Vector3 position,IDescribable description)
    {
        tooltip.SetActive(true);
        tooltip.transform.position = position;

        tooltipType.text = description.GetType();
        tooltipIcon.sprite = description.GetSprite();
        tooltipDes.text = description.GetDescription();
    }

    public void HideToolTip()
    {

        tooltip.SetActive(false);
    }
}
