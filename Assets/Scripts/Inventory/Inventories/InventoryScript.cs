
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript: MonoBehaviour
{
    private static InventoryScript instance;

    public static InventoryScript MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }
            return instance;
        }
    }

    private SlotScript fromSlot;

    private List<Bag> bags = new List<Bag>();

    [SerializeField]
    private BagButton bagButton;
    
    [SerializeField]
    private Item[] items;

    public bool CanAddBag
    {
        get { return bags.Count < 1; }
    }

    public SlotScript MyFromSlot
    {
        get => fromSlot;

        set
        {
            fromSlot = value;
            
            if (value != null)
            {
                fromSlot.MyIcon.color = Color.grey; 
            }
        }
    }

    

    private void Awake()
    {
        //FF8.Config.LoadAll();
        Bag bag = (Bag)Instantiate(items[0]);
        bag.Initialize(20);
        //bag.InitialDes();
        bag.Use();
    }

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.C))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(27);
            bag.Use();
        }*/
        if(Input.GetKeyDown(KeyCode.M))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(20);
            AddItem(bag);
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            Apple apple = (Apple)Instantiate(items[1]);
            AddItem(apple);
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            AddItem((Armor)Instantiate(items[2]));
            AddItem((Armor)Instantiate(items[3]));
            AddItem((Armor)Instantiate(items[4]));
            AddItem((Armor)Instantiate(items[5]));
        }
    }



    public void AddBag(Bag bag)
    {
        if (bagButton.Bag == null)
        {
            bagButton.Bag = bag;
            bags.Add(bag);
            
        }


    }

    /*public void OpenClose()
    {
        bool closedBag = bags.Find(x => !x.MyBagScript.IsOpen);

        foreach(Bag bag in bags)
        {
            if(bag.MyBagScript.IsOpen != closedBag)
            {
                bag.MyBagScript.OpenClose();
            }
        }
    }*/

    public void AddItem(Item item)
    {
        if(item.MyStackSize > 0)
        {
            if(PlaceInStack(item))
            {
                return;
            }
        }

        PlaceInEmpty(item);
    }

    public void PlaceInEmpty(Item item)
    {
        foreach (Bag bag in bags)
        {
            if (bag.MyBagScript.AddItem(item))
            {
                return;
            }
        }
    }

    public bool PlaceInStack(Item item)
    {
        foreach (Bag bag in bags)
        {
            foreach (SlotScript slots in bag.MyBagScript.MySlots)
            {
                if (slots.StackItem(item))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
