using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharButton : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private ArmorType armorType;
    
    private Armor armor;

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
                }
            }
        }
    }
    public void EquipArmor(Armor armor)
    {
        armorIcon.enabled = true;
        armorIcon.sprite = armor.MyIcon;
        this.armor = armor;

        HandScript.MyInstance.DeleteItem();
    }
}
