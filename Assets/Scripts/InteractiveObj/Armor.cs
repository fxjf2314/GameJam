using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ArmorType { Head, Leg, Back, Chest}

[CreateAssetMenu(fileName = "Armor", menuName = "Item/Armor", order = 2)]
public class Armor : Item
{
    [SerializeField]
    private ArmorType armorType;
    
    [SerializeField]
    private int intellect;
    
    [SerializeField]
    private int strength;
    
    [SerializeField]
    private int stamina;

    [SerializeField]
    private int id;

    internal ArmorType MyArmorType 
    { 
        get => armorType; 
    }

    //套装名
    public override string GetDescription()
    {
        string suitName = string.Empty;
        
        if(id < 3 && id > 0)
        {
            suitName += string.Format("<b>土豆蘑菇</b>----");
            suitName += string.Format("\n两件套效果: \n抵挡一次致命伤 \n(冷却时间一分钟)");
        }
        if (id < 5 && id > 2)
        {
            suitName += string.Format("<b>鸡蘑菇</b>----");
            suitName += string.Format("\n两件套效果: \n跳跃高度提升 ");
        }
        return base.GetDescription() + suitName;
    }
    
}
