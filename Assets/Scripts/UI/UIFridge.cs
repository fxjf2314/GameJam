using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class UIFridge : MonoBehaviour
{
    [SerializeField]
    private CharButton head, chest, back, leg;

    private static UIFridge instance;
    public static UIFridge MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<UIFridge>();
            }

            return instance;
        }
    }

    private CanvasGroup canvasGroup;

    private Mask mask;

    public CharButton MySelectedButton { get;set; }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        mask = GameObject.Find("Mask").GetComponent<Mask>();
    }
    public void OpenClose()
    {
        if(canvasGroup.alpha <= 0)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
            mask.enabled = true;
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
            mask.enabled = false;
        }
    }

    public void EquipArmor(Armor armor)
    {
        switch(armor.MyArmorType)
        {
            case ArmorType.Head:
                head.EquipArmor(armor);
                break;
            case ArmorType.Chest: 
                chest.EquipArmor(armor);
                break;
            case ArmorType.Back:
                back.EquipArmor(armor);
                break;
            case ArmorType.Leg: 
                leg.EquipArmor(armor);
                break;
            default: 
                break;
        }
    }
}
