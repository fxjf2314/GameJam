using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject, IMoveable, IDescribable
{
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

    [SerializeField]
    protected string description;

    [SerializeField]
    private string type;

    protected SlotScript slot;

    public int MyStackSize { get => stackSize; }

    public Sprite MyIcon { get => icon; }


    public SlotScript MySlot { get => slot; set => slot = value; }

    public void Remove()
    {
        if (MySlot != null)
        {
            MySlot.RemoveItem(this);

        }
    }

    public virtual string GetDescription()
    {
        return description;
    }

    public Sprite GetSprite()
    {
        return icon;
    }

    public string GetType()
    {
        return type;
    }
}
