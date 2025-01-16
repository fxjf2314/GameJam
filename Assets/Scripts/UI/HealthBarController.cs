using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour, IDataPersistence
{
    //单个血量
    public GameObject healthPrefab;
    private List<HealthController> hearts = new List<HealthController>();//用List存储血条
    public List<HealthController> Hearts { get => hearts;}
    static HealthBarController mInstance;
    public static HealthBarController Instance { get => mInstance;}

    private void Start()
    {
        mInstance = this;
        //healthPrefab = transform.Find("Health").gameObject;
        hearts.Add(healthPrefab.GetComponent<HealthController>());
        //Debug.Log(hearts.Count);
        PlayerModel.Instance.hp = 1;
        Initialized(PlayerModel.Instance.maxHp);
    }

    private void Update()
    {
        if(PlayerModel.Instance.hp > hearts.Count)AddHeart(PlayerModel.Instance.hp - hearts.Count);
        if(PlayerModel.Instance.hp < hearts.Count)DecreaseHearts(hearts.Count - PlayerModel.Instance.hp);
    }

    //增加increaseHp点血量上限
    public void AddMaxHp(int increaseHp)
    {
        AddHeart(increaseHp);
        PlayerModel.Instance.maxHp += increaseHp;
    }

    //治疗加血
    public void AddHeart(int heartsCount)
    {
        for(int i = 1; i < heartsCount && PlayerModel.Instance.hp < PlayerModel.Instance.maxHp ; i++)
        {
            HealthController heart = Instantiate(healthPrefab,transform).GetComponent<HealthController>();
            hearts.Add(heart);
            PlayerModel.Instance.hp++;
        }
        
    }
    //受伤减血
    public void DecreaseHearts(int heartsCount)
    {
        for(int i = 0; i < heartsCount; i++)
        {
            if (hearts.Count - 1 < 0) break;
            var heart = hearts[hearts.Count - 1];
            hearts.Remove(heart);
            if (!heart.gameObject) Debug.Log("1");
            Destroy(heart.gameObject);
        }
    }
    //初始化
    public void Initialized(int initHealth)
    {
        AddHeart(initHealth);
    }

    void IDataPersistence.LoadData(GameData gameData)
    {
        
    }

    void IDataPersistence.SaveData(ref GameData gameData)
    {
        
    }
}
