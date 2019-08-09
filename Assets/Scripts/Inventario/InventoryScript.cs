//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public delegate void ItemCountChanged(Item item);

//public class InventoryScript : MonoBehaviour  
//{
//    public Text descricao;
//    public List<string> textDesc;
//    public List<string> allItens;
//    private int index = 0;

//    public event ItemCountChanged itemCountChangedEvent;

//    private static InventoryScript instance;

//    private List<Item> bags = new List<Item>();

//    public bool InteractionPermiting = false;

//    public static InventoryScript MyInstance
//    {
//        get
//        {
//            if (instance == null)
//            {
//                instance = FindObjectOfType<InventoryScript>();
//            }
//            return instance;
//        }
//    }

//    private SlotScript fromSlot;

//    [SerializeField]
//    private Item[] items;



//    public SlotScript FromSlot
//    {
//        get
//        {
//            return fromSlot;
//        }

//        set
//        {
//            fromSlot = value;

//            if (value != null)
//            {
//                fromSlot.MyIcon.color = Color.grey;
//            }
//            fromSlot = value;
//        }
//    }

//    private void Awake()
//    {
//        Bag bag = (Bag)Instantiate(items[0]);
//        bag.Initialize(3);
//        bags.Add(bag);
//        bag.Use();
//    }

//    public void AddItem (Item item)
//    {
//        if (item.MyStacksize > 0)
//        {
//            if (PlaceInStack(item))
//            {
//                return;
//            }
//        }
//        PlaceInEmpty(item);
//    }

//    private void PlaceInEmpty(Item item)
//    {
//        foreach (Bag bag in bags)
//        {
//            if (bag.MyBagScript.AddItem(item))
//            {
//                OnItemCountChanged(item);
//                return;
//            }
//        }
//    }

//    private bool PlaceInStack(Item item)
//    {
//        foreach (Bag bag in bags)
//        {
//            foreach (SlotScript slots in bag.MyBagScript.MySlots)
//            {
//                if (slots.StackItem(item))
//                {
//                    OnItemCountChanged(item);
//                    return true;
//                }
//            }
//        }
//        return false;
//    }

//    // Use this for initialization
//    void Start () {
//	}
	
//	// Update is called once per frame
//	void Update () {
//        descricao.text = textDesc[index];
//		if (Input.GetKeyDown(KeyCode.K) && InteractionPermiting == true)
//        {
//            Bag bag = (Bag)Instantiate(items[0]);
//            bag.Initialize(4);
//            AddItem(bag);
//        }

//        if (Input.GetKeyDown(KeyCode.L))
//        {
//            Livro livro = (Livro)Instantiate(items[1]);
//            AddItem(livro);
//        }
//    }

//    public void pickupItem(int index) {
//        AddItem(items[index]);
//    }

//    void OnMouseUp()
//    {
//        Debug.Log("funciona?");
//    }

//    public Stack<IUseable> GetUseables(IUseable type)
//    {
//        Stack<IUseable> useables = new Stack<IUseable>();

//        foreach (Bag bag in bags)
//        {
//            foreach (SlotScript slot in bag.MyBagScript.MySlots)
//            {
//                if (!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
//                {
//                    foreach (Item item in slot.MyItems)
//                    {
//                        useables.Push(item as IUseable);
//                    }
//                }
//            }
//        }
//        return useables;
//    }

//    public int GetItemCount (string type)
//    {
//        int itemCount = 0;

//        foreach (Bag bag in bags)
//        {
//            foreach (SlotScript slot in bag.MyBagScript.MySlots)
//            {
//                if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
//                {
//                    itemCount += slot.MyItems.Count;
//                }
//            }
//        }
//        return itemCount;
//    }

//    public Stack<Item> GetItems(string type, int count)
//    {
//        Stack<Item> items = new Stack<Item>();

//        foreach (Bag bag in bags)
//        {
//            foreach (SlotScript slot in bag.MyBagScript.MySlots)
//            {
//                if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
//                {
//                    foreach (Item item in slot.MyItems)
//                    {
//                        items.Push(item);

//                        if(items.Count == count)
//                        {
//                            return items;
//                        }
//                    }
//                }
//            }
//        }

//        return items; 

//    }

//    public void OnItemCountChanged(Item item)
//    {
//        if(itemCountChangedEvent != null)
//        {
//            itemCountChangedEvent.Invoke(item);
//        }
//    }

//    public void UpdateText(string name) {
//        index = 0;
//        for (int i = 0; i < allItens.Count; i++) {
//            if(name == allItens[i]){
//                index = i;
//            }
//        }
//    }
//}
