using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharButton : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private ArmorType armorType;
    
    public Armor armor;

    public Armor partnericon;

    [SerializeField]
    private Image armorIcon;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(HandScript.MyInstance.MyMoveable is Armor)
            {
                Armor tmp = (Armor)HandScript.MyInstance.MyMoveable;
                
                if(tmp.MyArmorType == armorType) 
                { 
                    EquipArmor(tmp);
                    if(tmp.MyArmorType == ArmorType.Partner)
                    {
                        partnericon = tmp;
                    }
                }

            }
            else if(HandScript.MyInstance.MyMoveable == null && armor != null)
            {
                HandScript.MyInstance.TakeMoveable(armor);
                UIFridge.MyInstance.MySelectedButton = this;
                armorIcon.color = Color.gray;

            }
        }
    }
    public void EquipArmor(Armor armor)
    {
        armorIcon.enabled = true;
        armorIcon.sprite = armor.MyIcon;
        armorIcon.color = Color.white;
        this.armor = armor;

        HandScript.MyInstance.DeleteItem();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(armor != null)
        {
            UIManager.MyInstance.ShowToolTip(transform.position,armor);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideToolTip();
    }

    public void DequipArmor()
    {
        armorIcon.color = Color.white;
        armorIcon.enabled = false;
        armor = null;
    }
}
