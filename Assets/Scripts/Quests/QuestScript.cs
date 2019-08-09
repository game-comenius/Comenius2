//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class QuestScript : MonoBehaviour {

//    public Quest MyQuest { get; set; }

//    public bool MarkedComplete { get; set; }
    
//    public void Select()
//    {
//        GetComponent<Text>().color = Color.red; //Marca de vermelho as Quests desejadas;
//        Debug.Log("Select");
//        Questlog.MyInstance.ShowDescription(MyQuest);
//    }

//    public void DeSelect()
//    {
//        GetComponent<Text>().color = Color.white;  //Marca na cor branca a desceleção das Quest
//        Debug.Log("Descelect");
//    }

//    public void IsComplete()
//    {
//        if (MyQuest.IsComplete && !MarkedComplete)
//        {
//            GetComponent<Text>().text += "(Complete)";
//            MessageFeedManager.MyInstance.WriteMessage(string.Format("{0} (Complete)",MyQuest.MyTitle));
//            Debug.Log("COMPLETE");
//        }
//        else if (!MyQuest.IsComplete)
//        {
//            MarkedComplete = false;
//            GetComponent<Text>().text = MyQuest.MyTitle;
//        }
//    }
//}
