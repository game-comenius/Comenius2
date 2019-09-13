using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private static QuestManager _questManager;

    public static QuestManager questManager
    {
        get
        {
            return _questManager;
        }
    }

    private static byte[] questControl = new byte[1];

    [System.Serializable]
    public class QuestDescription
    {
        [TextArea (2,10)]
        public string[] descriptions = new string[8];
    }

    //[SerializeField] 
    public QuestDescription[] quests = new QuestDescription[1];

    private void Awake()
    {
        if (_questManager == null)
        {
            _questManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        questControl = new byte[quests.Length];

        DontDestroyOnLoad(gameObject);
    }

    //x corresponde à posição no array e y, no bit;
    public static bool GetQuestControl(Vector2Int r_questControlIndex)
    {
        return ((questControl[r_questControlIndex.x] & 1 << r_questControlIndex.y) == 1 << r_questControlIndex.y);
    }

    public static void SetQuestControl(Vector2Int r_questControlIndex, bool questFeita)
    {
        if (questFeita) 
        {
            questControl[r_questControlIndex.x] = (byte)(questControl[r_questControlIndex.x] | 1 << r_questControlIndex.y);
        }
        else
        {
            questControl[r_questControlIndex.x] = (byte)(((questControl[r_questControlIndex.x] ^ 255) | 1 << r_questControlIndex.y) ^ 255);
        }
    }
}