using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerModel : Character,IDataPersistence
{
    static PlayerModel mInstance;
    public static PlayerModel Instance
    {
        get
        {           
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<PlayerModel>();
                
                if (mInstance == null)
                {
                    Debug.LogError("未找到PlayerModel实例");
                }
            }
            return mInstance;
        }
    }

    //生命值和血条
    public int maxHp = 8;

    [Header("击倒率%")]
    public int KnockDownRate;
    public override void attack(Character target, int index)
    {
        if (Random.Range(0, 101) <= KnockDownRate)
        {
            Skill skill = new Skill();
            skill.damageType = DamageType.KnockDownDamage;
            skill.damageAmount= skillList[index].damageAmount;
            skill.minDamage = skillList[index].minDamage;
            skill.maxDamage = skillList[index].maxDamage;
            skill.destructive = skillList[index].destructive;

            skill.effects = new List<Effect>(skillList[index].effects);
            skillList[index] = skill;
        }
        base.attack(target, index);
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.playerPos = transform.position;
        gameData.playerRot = transform.rotation;
        gameData.hp = hp;
    }

    public void LoadData(GameData gameData)
    {
        //Debug.Log("gameData.playerPos");
        transform.position = gameData.playerPos;
        transform.rotation = gameData.playerRot;
        hp = gameData.hp;
    }
}
