using System.Collections;
using System.Collections.Generic;
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
                    Debug.LogError("Î´ÕÒµ½PlayerModelÊµÀý");
                }
            }
            return mInstance;
        }
    }

    public int maxHp = 100;
    public int KnockDownRate;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
