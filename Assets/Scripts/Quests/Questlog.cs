//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class Questlog : MonoBehaviour {

//    [SerializeField]
//    private GameObject questPrefab;

//    [SerializeField]
//    private Transform questParent;

//    private Quest selected;

//    private static Questlog instance;

//    [SerializeField]
//    private Text questDescription;

//    [SerializeField]
//    private CanvasGroup canvasGroup;

//    private List<QuestScript> questScripts = new List<QuestScript>();

//    private List<Quest> quests = new List<Quest>();

//    private InventoryScript scriptaaaa;

//    public static Questlog MyInstance
//    {
//        get
//        {
//            if(instance == null)
//            {
//                instance = FindObjectOfType<Questlog>();
//            }
//            return instance;
//        }
//    }

//    public void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.J))
//        {
//            OpenClose();
//        }
//    }

//    public void AcceptQuest( Quest quest)
//    {
//        foreach (CollectObjective o in quest.MyCollectObjectives)
//        {
//           InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(o.UpdateItemCount);
//            o.UpdateItemCount();
//           // scriptaaaa.itemCountChangedEvent += new ItemCountChanged(o.UpdateItemCount);
//        }

//        quests.Add(quest);

//        GameObject go = Instantiate(questPrefab, questParent);

//        QuestScript qs = go.GetComponent<QuestScript>();
//        qs.MyQuest = quest;
//        quest.MyQuestScript = qs;
//        questScripts.Add(qs);
//        go.GetComponent<Text>().text = quest.MyTitle;
//        CheckCompletion();
//    }

//    public void UpdateSelected()
//    {
//        ShowDescription(selected);
//    }

//    public void ShowDescription(Quest quest)
//    {
//        if (quest != null)
//        {
//            if (selected != null && selected != quest)
//            {
//                selected.MyQuestScript.DeSelect();
//            }

//            string objectives = string.Empty;

//            selected = quest;

//            string title = quest.MyTitle;

//            foreach (Objective obj in quest.MyCollectObjectives)
//            {
//                objectives += obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
//            }

//            questDescription.text = string.Format("<b>{0}</b>\n<size=10>{1}</size>{2}\n\nMissão\n<size=10>{2}</size>", title, quest.MyDescription, objectives);
//        }
         
//    }

//    public void CheckCompletion()
//    {
//        foreach (QuestScript qs in questScripts)
//        {
//            qs.IsComplete();
//        }
//    }

//    public void OpenClose()
//    {
//        if (canvasGroup.alpha == 1)
//        {
//            Close();
//        }
//        else
//        {
//            canvasGroup.alpha = 1;
//            canvasGroup.blocksRaycasts = true;
//        }
//    }


//    public void Close()
//    {
//        canvasGroup.alpha = 0;
//        canvasGroup.blocksRaycasts = false;
//    }

//    public void AbandonQuest()
//    {

//    }

//    public bool HasQuest(Quest quest)
//    {
//        return quests.Exists(x => x.MyTitle == quest.MyTitle);
//    }

//}
