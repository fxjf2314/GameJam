using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    private static HandScript instance;
    public static HandScript MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindAnyObjectByType<HandScript>();
            }

            return instance;
        }
    }    
    public IMoveable MyMoveable {  get; set; }

    private Image icon;

    [SerializeField]
    private Vector3 offset;

    private void Start()
    {
        icon = GetComponent<Image>();
    }

    private void Update()
    {
        icon.transform.position = Input.mousePosition + offset;
    }

    public void TakeMoveable(IMoveable moveable)
    {
        this.MyMoveable = moveable;
        icon.sprite = moveable.MyIcon;
        icon.color = Color.white;
    }

    public IMoveable Put()
    {
        IMoveable tmp = MyMoveable;
        MyMoveable = null;
        icon.color = new Color(0, 0, 0, 0);
        return tmp;
    }

    public void Drop()
    {
        MyMoveable = null;
        icon.color = new Color(0, 0, 0, 0);
    }

    public void DeleteItem()
    {
        if(MyMoveable is Item && InventoryScript.MyInstance.MyFromSlot != null)
        {
            (MyMoveable as Item).MySlot.Clear();

        }

        Drop();

        InventoryScript.MyInstance.MyFromSlot = null;
    }
}
