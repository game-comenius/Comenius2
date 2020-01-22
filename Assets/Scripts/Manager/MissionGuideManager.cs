using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionGuideManager : MonoBehaviour
{

    GameObject guiaOBJ;
    GameObject countOBJ;

    private static MissionGuideManager _missionGuideManager;

    public static MissionGuideManager missionGuideManager
    {
        get
        {
            return _missionGuideManager;
        }
    }


    private TextMeshProUGUI missionTip;

    private void Awake()
    {
        if (_missionGuideManager == null)
        {
            _missionGuideManager = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Use this for initialization
    void Start()
    {
        missionTip = GameObject.Find("Guia").GetComponent<TextMeshProUGUI>();
        guiaOBJ = GameObject.Find("CanvasGuia");
        countOBJ = GameObject.Find("CanvasContador");
    }

    public void SetMissionGuide(string mission)
    {
        missionTip.SetText(mission);
    }

    public void ShowMissionGuide(bool action)
    {
        if(action)
        {
            guiaOBJ.GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            guiaOBJ.GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    public void ShowMidiaCounter(bool action)
    {
        if (action)
        {
            countOBJ.GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            countOBJ.GetComponent<CanvasGroup>().alpha = 0;
        }
    }

}
