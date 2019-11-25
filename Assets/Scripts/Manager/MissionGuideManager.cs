using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionGuideManager : MonoBehaviour
{

    private static MissionGuideManager _missionGuideManager;

    public static MissionGuideManager missionGuideManager
    {
        get
        {
            return _missionGuideManager;
        }
    }

    public string[] missions = new string[3];

    private TextMeshProUGUI missionTip;

    private void Awake()
    {
        if (_missionGuideManager == null)
        {
            _missionGuideManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        missionTip = GameObject.Find("TextoMissao").GetComponent<TextMeshProUGUI>();
    }

    public void SetMissionGuide(int index)
    {
        missionTip.SetText(missions[index]);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
