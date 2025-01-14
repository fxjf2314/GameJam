using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour,IPointerClickHandler,IClickable,IPointerEnterHandler,IPointerExitHandler
{
    private ObservableStack<Item> items = new ObservableStack<Item>();
    
    [SerializeField]
    private Image icon;

    [SerializeField]
    private TextMeshProUGUI stackSize;

    public bool IsEmpty
    {
        get { return MyCount == 0; }
    }

    public bool IsFull
    {
        get 
        {
            if(IsEmpty || MyCount < MyItem.MyStackSize)
            {
                return false;
            }
            return true;
                
        }
    }

    public Item MyItem
    {
        get
        {
            if(!IsEmpty)
            {
                return items.Peek();
            }
            return null;
        }
    }

   

    public Image MyIcon 
    { 
        get
        {
            return icon;
        }
        set
        {
            icon = value;
        }
    }

    public int MyCount{ get { return items.Count; } }

    public TextMeshProUGUI MyStackText => stackSize;

    private void Awake()
    {
        items.OnPop += new UpdateStackEvent(UpdateSlot);
        items.OnPush += new UpdateStackEvent(UpdateSlot);
        items.OnClear += new UpdateStackEvent(UpdateSlot);
    }

    public void RemoveItem(Item item)
    {
        if(!IsEmpty)
        {
            items.Pop();
            
        }
    }

    public bool AddItem(Item item)
    {
        items.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        item.MySlot = this;
        return true;
    }

    public bool AddItems(ObservableStack<Item> newItems)
    {
        if(IsEmpty || newItems.Peek().GetType() == MyItem.GetType())
        {
            int count = newItems.Count;

            for(int i = 0; i < count; i++)
            {
                if(IsFull)
                {
                    return false;
                }

                AddItem(newItems.Pop());
            }
            
            return true;
        }

        return false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(!IsEmpty && InventoryScript.MyInstance.MyFromSlot == null)
            {
                HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
                InventoryScript.MyInstance.MyFromSlot = this;
            }
            else if(InventoryScript.MyInstance.MyFromSlot != null)
            {
                if(PutItemBack() || MergeItems(InventoryScript.MyInstance.MyFromSlot) || SwapItems(InventoryScript.MyInstance.MyFromSlot) || AddItems(InventoryScript.MyInstance.MyFromSlot.items))
                {
                    HandScript.MyInstance.Drop();
                    InventoryScript.MyInstance.MyFromSlot = null;
                }
            }
            
        }
        
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }



    public void UseItem()
    {
        if(MyItem is IUseable)
        {
            (MyItem as IUseable).Use();
        }
    }

    public bool StackItem(Item item)
    {
        if(!IsEmpty && item.name == MyItem.name && items.Count < MyItem.MyStackSize)
        {
            items.Push(item);
            item.MySlot = this;
            return true;
        }
        return false;
        
        
    }

    private bool PutItemBack()
    {
        if(InventoryScript.MyInstance.MyFromSlot == this)
        {
            InventoryScript.MyInstance.MyFromSlot.MyIcon.color = Color.white;
            return true;
        }

        return false;
    }

    private bool SwapItems(SlotScript from)
    {
        if(IsEmpty)
        {
            return false;
        }
        if(from.MyItem.GetType() != MyItem.GetType() || from.MyCount + MyCount > MyItem.MyStackSize)
        {
            ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.items);
            from.items.Clear();
            from.AddItems(items);
            items.Clear();
            AddItems(tmpFrom); 

            return true;
        }

        return false;
    }

    private bool MergeItems(SlotScript from)
    {
        if(IsEmpty)
        {
            return false;
        }
        if(from.MyItem.GetType() == MyItem.GetType() && !IsFull)
        {
            int free = MyItem.MyStackSize - MyCount;
            
            for(int i = 0; i < free; i++)
            {
                AddItem(from.items.Pop());
            }

            return true;
        }
        
        return false;
    }

    public void UpdateSlot()
    {
        UIManager.MyInstance.UpdateStackSize(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!IsEmpty)
        {
            UIManager.MyInstance.ShowToolTip(transform.position,MyItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideToolTip();
    }
}
