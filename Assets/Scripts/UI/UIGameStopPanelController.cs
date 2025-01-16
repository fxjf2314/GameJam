using QFramework.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameStopPanelController : MonoBehaviour
{
    GameObject UIGameStopPanel;

    UnityEngine.UI.Button BackToGameBtn;
    UnityEngine.UI.Button SettingBtn;
    UnityEngine.UI.Button GotoTitleBtn;

    // Start is called before the first frame update
    void Start()
    {
        UIGameStopPanel = TransformFind.TransformFindChild(transform, "UIGameStopPanel").gameObject;
        BackToGameBtn = TransformFind.TransformFindChild(transform, "BackToGameBtn").GetComponent<Button>();
        SettingBtn = TransformFind.TransformFindChild(transform, "SettingBtn").GetComponent<Button>();
        GotoTitleBtn = TransformFind.TransformFindChild(transform, "GotoTitleBtn").GetComponent<Button>();
        ButtonAddListener();
    }

    // Update is called once per frame
    void Update()
    {
        StopPanel();
    }

    void StopPanel()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!UIGameStopPanel.activeSelf)
            {
                //打开暂停界面
                UIGameStopPanel.SetActive(true);
                //暂停游戏
                Time.timeScale = 0;
            }
            else
            {
                BackToGameBtn.onClick.Invoke();
            }
        }
    }

    void ButtonAddListener()
    {
        //按钮添加事件
        BackToGameBtn.onClick.AddListener(() =>
        {
            UIGameStopPanel.SetActive(false);
            Time.timeScale = 1;
        });
        SettingBtn.onClick.AddListener(() =>
        {
            //Test存档
            DataPersistenceManager.Instance.SaveGame(SaveTool.File_Name_01);
        });
        GotoTitleBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameStartUI");
            Time.timeScale = 1;
        });
    }
}
