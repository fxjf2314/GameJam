using QFramework.Example;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private TextMeshPro tmp;

    private void Start()
    {
        tmp = transform.Find("Text").GetComponent<TextMeshPro>();
        tmp.alpha = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            tmp.alpha = 1;
            Interactive();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            tmp.alpha = 0;
        }
    }

    void Interactive()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            //»ØÂúÑª
            HealthBarController.Instance.AddHeart(PlayerModel.Instance.maxHp);
            //×Ô¶¯´æµµ
            DataPersistenceManager.Instance.SaveGame(SaveTool.File_Name_01);
        }
    }

}
