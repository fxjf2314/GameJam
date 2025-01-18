using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Scrollbar horiScrollbar;
    private Scrollbar vertiScrollbar;
    
    private static UIManager instance;

    private Image mask;

    private Bag bag;

    [SerializeField]
    private GameObject tooltip;

    [SerializeField]
    private GameObject TalentTree;

    private TextMeshProUGUI tooltipDes;

    private TextMeshProUGUI tooltipType;

    private TextMeshProUGUI tooltipTitle;

    [SerializeField]
    private BagButton bagButton;

    [SerializeField]
    private UIFridge uiFridge;

    private Image tooltipIcon;

    private void Awake()
    { 
        horiScrollbar = GameObject.Find("FridgeHoriScrollbar").GetComponent<Scrollbar>();
        vertiScrollbar = GameObject.Find("FridgeVertiScrollbar").GetComponent<Scrollbar>();
        mask = GameObject.Find("Mask").GetComponent<Image>();
        

        //工具提示标题
        tooltipTitle = tooltip.transform.GetChild(0).GetComponent<TextMeshProUGUI>();        
        //工具提示类别
        
        //工具提示图标
        Transform desIcon = tooltip.transform.GetChild(1);
        tooltipIcon = desIcon.GetComponentInChildren<Image>();
        tooltipType = desIcon.GetComponentInChildren<TextMeshProUGUI>();
        //工具提示详细
        Transform desChild = tooltip.transform.GetChild(2);
        tooltipDes = desChild.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            PartnerButton.MyInstance.UpdatePartnerIcon();
            UIFridge.MyInstance.chickenMashroomCount = 0;
            UIFridge.MyInstance.PotatoMashroomCount = 0;
            UIFridge.MyInstance.UpdatePlayerAttribute();
            //mask.color.a = 1;
            horiScrollbar.value = 1;
            vertiScrollbar.value = 0;
            //scrollbar.interactable = scrollbar.interactable == false ? true : false;
            mask.enabled = mask.enabled == true ? false : true;
            UIFridge.MyInstance.OpenClose();
            bagButton.Bag.MyBagScript.OpenClose();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //bagButton.Bag.MyBagScript.Close();
               
            UIFridge.MyInstance.OpenClose();
            TalentTree.SetActive(TalentTree.activeSelf == true ? false : true);

        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            bagButton.Bag.MyBagScript.OpenClose();
            mask.enabled = mask.enabled == true ? false : true;
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

        tooltipTitle.text = description.GetTitle();
        tooltipType.text = description.GetType();
        tooltipIcon.sprite = description.GetSprite();
        tooltipDes.text = description.GetDescription();
    }

    public void HideToolTip()
    {

        tooltip.SetActive(false);
    }


}
