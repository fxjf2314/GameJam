using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using QFramework.Example;

public class DataPersistenceManager : MonoBehaviour
{

    public static bool isExist = false;
    public static DataPersistenceManager Instance { get; private set; }
    GameData gameData;
    public GameData GameData { get { return gameData; } }
    //存储实现了接口的类
    List<IDataPersistence> dataPersistenceObjs;

    private void Awake()
    {
        if(!isExist)
        {
            Instance = this;
            //保证数据管理器在每个场景都存在
            DontDestroyOnLoad(gameObject);
            //避免重复实例化
            isExist = true;
        }
        else
        {
            Destroy(gameObject);
        }
        gameData = SaveTool.Load<GameData>(SaveTool.File_Name_01);
        //if(Instance != null)
        //{
        //    Debug.LogError("当前场景有多个数据存储管理器");
        //}
    }

    public void NewGame()
    {
        gameData = new GameData();
        //场景加载后寻找继承了IDataPersistence的脚本
        dataPersistenceObjs = FindAllDataPersistenceObjs();
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjs)
        {
            //调用每个类的LoadData方法
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame(string FileName)
    {
        //场景加载后寻找继承了IDataPersistence的脚本
        dataPersistenceObjs = FindAllDataPersistenceObjs();
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjs)
        {
            //调用每个类的SaveData方法
            dataPersistenceObj.SaveData(ref gameData);
        }
        SaveTool.Save(FileName, gameData);
    }

    public void LoadGame(string FileName)
    {
        //场景加载后寻找继承了IDataPersistence的脚本
        dataPersistenceObjs = FindAllDataPersistenceObjs();
        gameData = SaveTool.Load<GameData>(FileName);
        if (gameData == null)
        {
            Debug.Log("没有找到存档，自动开始新游戏");
            NewGame();
        }
        
            Debug.Log(dataPersistenceObjs.Count);
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjs)
        {
            //Debug.Log("345");
            //调用每个类的LoadData方法
            dataPersistenceObj.LoadData(gameData);
        }
    }

    List<IDataPersistence> FindAllDataPersistenceObjs()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjs = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjs);
    }
}
