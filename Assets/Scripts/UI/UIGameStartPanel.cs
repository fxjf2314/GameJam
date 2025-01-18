using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using QFramework.Example;

public partial class UIGameStartPanel:MonoBehaviour
{
	void Start()
	{
        if (DataPersistenceManager.Instance.GameData == null)
        {
            ContinueGameBtn.interactable = false;
        }
        ButtonAddListener();
    }

    void ButtonAddListener()
    {
        NewGameBtn.onClick.AddListener(() =>
        {
            SceneManager.sceneLoaded += StartNewGame;
            //开始新游戏
            SceneManager.LoadScene("TeachingLevel");
            Physics.gravity = new Vector3(0, -9.81f, 0);
        });

        ContinueGameBtn.onClick.AddListener(() =>
        {
            //通过订阅事件来避免场景加载过慢导致读档时继承了IDataPersistence的脚本还没加载
            SceneManager.sceneLoaded += LoadSave;
            SceneManager.LoadScene("Game");
            //读取存档
            //Debug.Log("222");
        });

        SettingBtn.onClick.AddListener(() =>
        {

        });

        GameExitBtn.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            // 如果是在Unity编辑器中，调用Unity的关闭方法
            UnityEditor.EditorApplication.isPlaying = false;
#else
			// 如果是在构建的游戏中，调用Application的退出方法
			Application.Quit();
#endif
        });
    }

    private void LoadSave(Scene scene, LoadSceneMode mode)
    {
        // 场景加载完成后执行的逻辑
        DataPersistenceManager.Instance.LoadGame(SaveTool.File_Name_01);

        // 取消订阅事件，避免重复调用
        SceneManager.sceneLoaded -= LoadSave;
    }

    private void StartNewGame(Scene scene, LoadSceneMode mode)
    {
		// 场景加载完成后执行的逻辑
		DataPersistenceManager.Instance.NewGame();

        // 取消订阅事件，避免重复调用
        SceneManager.sceneLoaded -= StartNewGame;
    }
}

