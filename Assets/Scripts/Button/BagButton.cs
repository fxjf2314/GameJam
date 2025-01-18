using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagButton : MonoBehaviour,IPointerClickHandler
{
    private Bag bag;

    [SerializeField]
    private Sprite full, empty;

    private Image mask;

    private void Awake()
    {
        mask = GameObject.Find("Mask").GetComponent<Image>();
    }

    public Bag Bag 
    { 
        get
        {
            return bag;
        }

        set
        {
            if(value != null)
            {
                GetComponent<Image>().sprite = full;
            }
            else
            {
                GetComponent<Image>().sprite = empty;
            }
            bag = value;
        }
    }

    private void Start()
    {
        bag.MyBagScript.OpenClose();
        mask.enabled = mask.enabled == true ? false : true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        

    }
}
