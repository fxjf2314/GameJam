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
    private Talent[] toUnlock;

    private void Awake()
    {
        sprite = GetComponentInChildren<Button>();
        countText.text = $"{currentCount}/{maxCount}";
        if (unlocked)
        {
            Unlock();
        }
    }

    public bool Click()
    {
        if(currentCount < maxCount && unlocked)
        {
            currentCount++;
            countText.text = $"{currentCount}/{maxCount}";

            if(currentCount == maxCount)
            { 
                if(toUnlock.Length > 0)
                {
                    foreach(Talent t in toUnlock)
                    {
                        t.Unlock();
                    }
                }
            }
            return true;
        }

        return false;
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
