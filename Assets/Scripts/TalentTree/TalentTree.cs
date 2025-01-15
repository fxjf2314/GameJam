using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalentTree : MonoBehaviour
{
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
        if(MyPoints > 0 && talent.Click())
        {
            MyPoints--;
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
