using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalentTree : MonoBehaviour
{
    private static TalentTree instance;
    public static TalentTree MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TalentTree>();
            }
            return instance;
        }
    }

    private int points = 9;

    [SerializeField]
    private Talent[] talents;

    [SerializeField]
    private TextMeshProUGUI talentPointText;

    [SerializeField]
    private Talent unlockedTalentByDefault;

    public int MyPoints 
    {
        get => points; 
        set
        {
            points = value;
            UpdateTalentPointText();
        } 
    }

    private void Start()
    {
        ResetTalents();
    }

    public void TryUseTalent(Talent talent)
    {
        
        if(MyPoints > 0 && talent.Clickable())
        {

            return;
        }
    }

    public void ResetTalents()
    {
        UpdateTalentPointText();
        foreach(Talent talent in talents)
        {
            talent.Lock();
        }

        unlockedTalentByDefault.Unlock();
          
        
    }

    private void UpdateTalentPointText()
    {
        talentPointText.text = points.ToString();
    }

}
