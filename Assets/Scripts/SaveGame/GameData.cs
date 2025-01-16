using QFramework.Example;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class GameData
{
    #region 存档相关
    //public int currentSaveIndex;
    //public int currentSaveCount;
    #endregion

    #region 玩家相关数据
    //位置
    public Vector3 playerPos;
    public Quaternion playerRot;

    //血量
    public int hp;

    //装备穿戴能力相关,在PlayerItemCheck脚本中
    public bool isGetGlideItem;

    #endregion

    //保存新存档的初始值
    public GameData()
    {
        playerPos = new Vector3(0,0.434f,0);
        playerRot = Quaternion.Euler(0,0,0);
        hp = 100;
        isGetGlideItem = false;
    }

}
