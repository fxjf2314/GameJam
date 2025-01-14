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

    //套装名
    public override string GetDescription()
    {
        string suitName = string.Empty;
        
        if(id < 3 && id > 0)
        {
            suitName += string.Format("\n土豆蘑菇----");
            suitName += string.Format("\n两件套效果: 抵挡一次致命伤 (冷却时间一分钟)");
        }
        return base.GetDescription() + suitName;
    }
    
}
