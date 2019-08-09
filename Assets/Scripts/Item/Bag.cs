//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//[CreateAssetMenu(fileName ="Bag",menuName ="Itens/Bags",order =1)]
//public class Bag : Item, IUseable
//{

//    private int slots;

//    [SerializeField]
//    protected GameObject bagPrefab;

//    public BagScript MyBagScript { get; set; }

//    public int Slots
//    {
//        get
//        {
//            return slots;
//        }
//    }

//    public void Initialize(int slots)
//    {
//        this.slots = slots;
//    }

//    public void Use()
//    {
//        Remove();
//        MyBagScript = Instantiate(bagPrefab, InventoryScript.MyInstance.transform).GetComponent<BagScript>();
//        MyBagScript.AddSlots(slots);
//    }
//}


