using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class UIFridge : MonoBehaviour
{
    [SerializeField]
    private CharButton[] Armors = new CharButton[5];

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

    public int point;

    public int chickenMashroomCount;

    public int PotatoMashroomCount;

    private float originJumpSpeed;

    private float originMoveSpeed;

    private float currentJumpSpeed;

    private float currentMoveSpeed;

    private CanvasGroup canvasGroup;

    private Mask mask;

    public CharButton MySelectedButton { get;set; }

    private void Awake()
    {
        originJumpSpeed = PlayerController.Instance.jumpSpeed;
        originMoveSpeed = PlayerController.Instance.moveSpeed;
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
                Armors[0].EquipArmor(armor);
                break;
            case ArmorType.Chest:
                Armors[1].EquipArmor(armor);
                break;
            case ArmorType.Back:
                Armors[2].EquipArmor(armor);
                break;
            case ArmorType.Leg:
                Armors[3].EquipArmor(armor);
                break;
            case ArmorType.Partner:
                Armors[4].EquipArmor(armor);
                break;
            default: 
                break;
        }
    }

    public void UpdatePlayerAttribute()
    {
        
        if (Armors[2].armor != null && Armors[2].armor.GetTitle() == "¼¦³á")
        {
            PlayerItemCheck.Instance.isGetGlideItem = true;
        }
        if (Armors[3].armor != null && Armors[3].armor.GetTitle() == "¼¦ÍÈ" && currentMoveSpeed <= originMoveSpeed)
        {
            PlayerItemCheck.Instance.SpeedUp((int)(originMoveSpeed * 2));
            currentMoveSpeed = originMoveSpeed + (int)(originMoveSpeed * 2);
        }
        

        foreach(CharButton detailarmor in Armors)
        {
            if(detailarmor.armor != null )
            {
                if (detailarmor.armor.id == 2)
                {
                    chickenMashroomCount++;
                }
                if (detailarmor.armor.id == 1)
                {
                    PotatoMashroomCount++;
                }
            }
            
        }

        if(chickenMashroomCount < 2 && currentJumpSpeed <= originJumpSpeed)
        {
            PlayerController.Instance.jumpSpeed = originJumpSpeed;
        }
        else if(chickenMashroomCount >= 2 && currentJumpSpeed <= originJumpSpeed)
        {
            PlayerItemCheck.Instance.JumpSpeedUp((int)(originJumpSpeed * 1.5));
            currentJumpSpeed = originJumpSpeed + (int)(originJumpSpeed * 1.5);
        }
        if(PotatoMashroomCount >= 2)
        {
            PlayerItemCheck.Instance.canDefense = true;
            PlayerItemCheck.Instance.potatoMashroom = true;
        }
        else if(PotatoMashroomCount < 2) 
        { 
            PlayerItemCheck.Instance.potatoMashroom = false;

        }
    }
}
