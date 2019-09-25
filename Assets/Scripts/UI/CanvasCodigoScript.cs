using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasCodigoScript : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    [SerializeField] private Text questLog;

    [SerializeField] private Text uiSendoUsada;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        canvas.SetActive(false);
    }

    void Update ()
    {
        uiSendoUsada.enabled = GameManager.uiSendoUsada;
       
        if(canvas.activeSelf)
        {
            if (questLog.gameObject.activeSelf) 
            {
                string text = "QuestLog - F2\n\n";

                for (int i = 0; i < QuestManager.questManager.quests.Length * 8; i++)
                {
                    text += i + " - ";

                    text += QuestManager.questManager.quests[i / 8].descriptions[i % 8];

                    if (QuestManager.GetQuestControl(new Vector2Int(i / 8, i % 8)))
                    {
                        text += " - OK";
                    }

                    text += "\n\n";
                }

                questLog.text = text;
            }

            if(Input.GetKeyDown(KeyCode.F2))
            {
                questLog.gameObject.SetActive(!questLog.gameObject.activeSelf);
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            canvas.SetActive(!canvas.activeSelf);
        }
	}
}
