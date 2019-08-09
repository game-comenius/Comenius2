//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//public class SlotScript : MonoBehaviour , IPointerClickHandler, IClickable
//{
    
//    private ObservableStack<Item> items = new ObservableStack<Item>();

    

//    [SerializeField]
//    private Image icon;
//    GameObject Group;

//    [SerializeField]
//    private Text stackSize;

//    public void Start()
//    {
       
//        /////////////////////////////////////////////////////////// -----XXXXXXXXXXXXXXXXXXXXXXXXX----- ////////////////////////////////////////////////////
//    }

//    public bool IsEmpty
//    {
//        get
//        {
//            return MyItems.Count == 0;
//        }
//    }

//    public bool IsFull
//    {
//        get
//        {
//            if (IsEmpty || MyCount < MyItem.MyStacksize)
//            {
//                return false;
//            }

//            return true;
//        }
//    }

//    public Item MyItem
//    {
//        get
//        {
//            if (!IsEmpty)
//            {
//                return MyItems.Peek();
//            }

//            return null;
//        }
//    }

//    public Image MyIcon
//    {
//        get
//        {
//            return icon;
//        }

//        set
//        {
//            icon = value;
//        }
//    }

//    public int MyCount
//    {
//        get { return MyItems.Count; }
//    }

//    public Text MyStackText
//    {
//        get
//        {
//            return stackSize;
//        }
//    }

//    public ObservableStack<Item> MyItems
//    {
//        get
//        {
//            return items;
//        }
//    }

//    private void Awake()
//    {
//        MyItems.OnPop += new UpdateStackEvent(UpdateSlot);

//        MyItems.OnPush += new UpdateStackEvent(UpdateSlot);

//        MyItems.OnClear += new UpdateStackEvent(UpdateSlot);
//    }

//    public bool AddItem (Item item)
//    {
//        MyItems.Push(item);
//        icon.sprite = item.MyIcon;
//        icon.color = Color.white;
//        item.MySlot = this;
//        return true;
//    }

//    public bool AddItems (ObservableStack<Item> newItems)
//    {
//        if (IsEmpty || newItems.Peek().GetType() == MyItem.GetType())
//        {
//            int count = newItems.Count;

//            for (int i = 0; i < count; i++)
//            {
//                if (IsFull)
//                {
//                    return false;
//                }

//                AddItem(newItems.Pop());
//            }

//            return true;
//        }

//        return false;
//    }

//    public void RemoveItem(Item item)
//    {
//        if (!IsEmpty)
//        {
//            InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
//        }
//    }

//    public void OnPointerClick (PointerEventData eventData)
//    {
//        if (eventData.button == PointerEventData.InputButton.Left)
//        {
//            Debug.Log("mouseover");
//            Debug.Log(MyItem.name);
//            if (MyItem.name == "Quadro")
//            {
//                Debug.Log("FOI!");
//            }

//            GameObject.Find("Painel de Inventário 1").GetComponent<InventoryScript>().UpdateText(MyItem.name);


//            if (InventoryScript.MyInstance.FromSlot == null && !IsEmpty)
//            {
//                HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
//                InventoryScript.MyInstance.FromSlot = this;
//            }
//            else if (InventoryScript.MyInstance.FromSlot  != null)
//            {
//                if (PutItemBack() || MergeItems(InventoryScript.MyInstance.FromSlot) ||SwapItmes(InventoryScript.MyInstance.FromSlot) ||AddItems(InventoryScript.MyInstance.FromSlot.MyItems))
//                {
//                    HandScript.MyInstance.Drop();
//                    InventoryScript.MyInstance.FromSlot = null;
//                }
//            }
//        }

//        if (eventData.button == PointerEventData.InputButton.Left)
//        {
//            UseItem();
//        }
//    }

//    public void Clear()
//    {
//        int initCount = MyItems.Count;

//        if (initCount > 0)
//        {
//            for (int i = 0; i < initCount; i++)
//            {

//                InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
//            }
//        }
//    }

//    public void UseItem()
//    {
//        if(MyItem is IUseable)
//        {
//            (MyItem as IUseable).Use();
//        }
//    }

//    public bool StackItem (Item item)
//    {
//        if (!IsEmpty && item.name == MyItem.name && MyItems.Count < MyItem.MyStacksize)
//        {
//            MyItems.Push(item);
//            item.MySlot = this;
//            return true;
//        }
//        return false;
//    }

//    private bool PutItemBack()
//    {
//        if (InventoryScript.MyInstance.FromSlot == this)
//        {
//            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
//            return true;
//        }
//        return false;
//    }

//    private bool SwapItmes (SlotScript from)
//    {
//        if (IsEmpty)
//        {
//            return false;
//        }
//        if (from.MyItem.GetType() != MyItem.GetType() || from.MyCount+MyCount > MyItem.MyStacksize)
//        {
//            ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.MyItems);
//            from.MyItems.Clear();
//            from.AddItems(MyItems);
//            MyItems.Clear();
//            AddItems(tmpFrom);

//            return true;
//        }
//        return false;
//    }

//    private bool MergeItems (SlotScript from)
//    {
//        if (IsEmpty)
//        {
//            return false;
//        }

//        if (from.MyItem.GetType() == MyItem.GetType() && !IsFull)
//        {
//            int free = MyItem.MyStacksize - MyCount;

//            for (int i = 0; i < free; i++)
//            {
//                AddItem(from.MyItems.Pop());
//            }
//            return true;
//        }
//        return false;
//    }

//    private void UpdateSlot()
//    {
//        UIManager.MyInstance.UpdateStackSize(this);
//    }



//}
