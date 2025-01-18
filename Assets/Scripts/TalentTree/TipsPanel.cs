using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class TipsPanel : MonoBehaviour
{
    private static TipsPanel instance;
    public static TipsPanel MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TipsPanel>();
            }
            return instance;
        }
    }

    //public bool isPress;

    

    private CanvasGroup tips;

    private string detailTalentDes;//获取到的描述

    [SerializeField]
    private TextMeshProUGUI displayTalentDes;//展示出来的描述

    [SerializeField]
    private TextMeshProUGUI talentName;//名字

    [SerializeField]
    private  Button confirmButton;

    [SerializeField]
    private Button cancelButton;

    private System.Action onConfirm;

    private System.Action onCancel;

    private void Awake()
    {
        tips = GetComponent<CanvasGroup>();

        
    }

    public void Open(System.Action confirmAction,System.Action cancelAction,Talent talent)
    {
        GetTalentDetail(talent);
        //名字与描述
        talentName.text = talent.MyTitle;
        displayTalentDes.text = detailTalentDes; 
        //显示相关
        tips.alpha = 1;
        confirmButton.onClick.AddListener(OnConfirm);
        cancelButton.onClick.AddListener(OnCancel);
        tips.blocksRaycasts = true;
        onConfirm = confirmAction;
        onCancel = cancelAction;
    }

    private void OnConfirm()
    {
        
        //isClick();
        onConfirm?.Invoke();
        tips.alpha = 0;
        tips.blocksRaycasts = false;
        OnDestroy();
    }
    
    private void OnCancel()
    {
        //isClick();
        onCancel?.Invoke();
        tips.alpha = 0;
        tips.blocksRaycasts = false;
        OnDestroy ();
    }

    public void OnDestroy()
    {
        confirmButton.onClick.RemoveListener(OnConfirm);
        cancelButton.onClick.RemoveListener(OnCancel);
    }

    private void GetTalentDetail(Talent talent)
    {
        detailTalentDes = string.Empty;
        detailTalentDes += string.Format($"攻击力增加: {PlayerModel.Instance.initDamage}→{PlayerModel.Instance.initDamage + talent.addPower}");
        detailTalentDes += string.Format($"\n击倒率增加: {PlayerModel.Instance.KnockDownRate}→{PlayerModel.Instance.KnockDownRate + talent.addKnockDownRate}");
    }
}
