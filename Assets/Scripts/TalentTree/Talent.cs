using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Talent : MonoBehaviour
{
    private Button sprite;

    [SerializeField]
    private int maxCount;

    private int currentCount;

    [SerializeField]
    private TextMeshProUGUI countText;

    [SerializeField]
    private bool unlocked;

    [SerializeField]
    private string title;

    public int addPower;

    public int addKnockDownRate;

    public bool Click;

    [SerializeField]
    private Talent[] toUnlock;

    public string MyTitle { get => title; set => title = value; }

    private void Awake()
    {
        sprite = GetComponent<Button>();
        countText.text = $"{currentCount}/{maxCount}";
        if (unlocked)
        {
            Unlock();
        }
    }

    public bool Clickable()
    {
        //isPress = false;
        TipsPanel.MyInstance.Open(OnConfirm, OnCancel,this);
        //TipsPanel.MyInstance.detailTalentDes.text = description;
        return Click;
    }

    /*private IEnumerator WaitForIsPress()
    {
        yield return TipsPanel.MyInstance.WaitForButtonPress();

        isPress = true;
    }*/

    private void OnConfirm()
    {
        //isConfirm = true;
        if (currentCount < maxCount && unlocked)
        {
            currentCount++;
            countText.text = $"{currentCount}/{maxCount}";

            if (currentCount == maxCount)
            {
                if (toUnlock.Length > 0)
                {
                    foreach (Talent t in toUnlock)
                    {
                        t.Unlock();
                    }
                }
            }
            Click = true;
            TalentTree.MyInstance.MyPoints--;
            PlayerModel.Instance.initDamage += addPower;
            PlayerModel.Instance.KnockDownRate += addKnockDownRate;
        }

        Click = false;
    }

    private void OnCancel()
    {
        Click = false;
    }


    public void Lock()
    {
        sprite.interactable = false;
        countText.color = Color.gray;
    }

    public void Unlock()
    {
        sprite.interactable = true;
        countText.color = Color.white;
        unlocked = true;
    }

    
}
