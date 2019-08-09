﻿//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[System.Serializable]
//public class Quest
//{

//    [SerializeField]
//    private string title;

//    [SerializeField]
//    private string description;

//    [SerializeField]
//    private CollectObjective[] collectObjectives;

//    public QuestScript MyQuestScript { get; set; }

//    public string MyTitle
//    {


//        get
//        {
//            return title;
//        }

//        set
//        {
//            title = value;
//        }


//    }

//    public string MyDescription
//    {
//        get
//        {
//            return description;
//        }

//        set
//        {
//            description = value;
//        }
//    }

//    public CollectObjective[] MyCollectObjectives
//    {
//        get
//        {
//            return collectObjectives;
//        }
//    }

//    public bool IsComplete
//    {
//        get
//        {
//            foreach (Objective o in collectObjectives)
//            {
//                if (!o.IsComplete)
//                {
//                    return false;
//                }
//            }
//            return true; 
//        }
//    }
    
//    void Start () {
		
//	}
	
//	// Update is called once per frame
//	void Update () {
		
//	}
//}


//[System.Serializable]
//public abstract class Objective
//{
//    [SerializeField]
//    private int Amount;

    
//    private int currentAmount;

//    [SerializeField]
//    private string type;

//    public int MyAmount
//    {
//        get
//        {
//            return Amount;
//        }
//    }

//    public int MyCurrentAmount
//    {
//        get
//        {
//            return currentAmount;
//        }

//        set
//        {
//            currentAmount = value;
//        }
//    }

//    public string MyType
//    {
//        get
//        {
//            return type;
//        }
//    }

//    public bool IsComplete
//    {
//        get
//        {
//            return MyCurrentAmount >= MyAmount; 
//        }
//    } 
//}

//[System.Serializable]
//public class CollectObjective : Objective
//{
//    public void UpdateItemCount(Item item)
//    {
//        if (MyType.ToLower() == item.MyTitle.ToLower())
//        {
//            MyCurrentAmount = InventoryScript.MyInstance.GetItemCount(item.MyTitle);

//            if(MyCurrentAmount <= MyAmount)
//            {
//                MessageFeedManager.MyInstance.WriteMessage(string.Format("{0}: {1}/{2}", item.MyTitle, MyCurrentAmount, MyAmount));
//            }

//            Questlog.MyInstance.UpdateSelected();
//            Questlog.MyInstance.CheckCompletion();
//            Debug.Log(MyCurrentAmount);
//        }
//    }

//    public void UpdateItemCount()
//    {
//        MyCurrentAmount = InventoryScript.MyInstance.GetItemCount(MyType);

//        Questlog.MyInstance.UpdateSelected();
//        Questlog.MyInstance.CheckCompletion();
//    }

//    public void Complete()
//    {
//        Stack<Item> items = InventoryScript.MyInstance.GetItems(MyType, MyAmount);

//        foreach(Item item in items)
//        {
//            item.Remove();
//        }
//    }
//}