using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFridge : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private Mask mask;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        mask = GameObject.Find("Mask").GetComponent<Mask>();
    }
    public void OpenClose()
    {
        if(canvasGroup.alpha <= 0)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
            mask.enabled = true;
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
            mask.enabled = false;
        }
    }
}
