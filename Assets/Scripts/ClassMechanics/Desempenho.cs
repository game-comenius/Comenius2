﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desempenho : MonoBehaviour
{
    public static Desempenho instance;

    [Tooltip ("Acima de [excluindo]")]
    [SerializeField] private double bomDesempenho;
    [Tooltip("Abaixo de [incluindo]")]
    [SerializeField] private double mauDesempenho;

    [SerializeField] private SpriteRenderer[] objetos = new SpriteRenderer[1];
    [SerializeField] private Sprite[] spritesBons = new Sprite[1];
    [SerializeField] private Sprite[] spritesMaus = new Sprite[1];

    [SerializeField]
    private int missionID;

    private void Awake()
    {
#if UNITY_EDITOR
        if (instance != null)
        {
            Debug.Log("Mais de uma instancia.");
        }
#endif 

        instance = this;
    }

    public void TrocarSala()
    {
        Debug.Log("Executed");

        double points = Player.Instance.MissionHistory[missionID].totalMissionPoints;

        if (points > bomDesempenho)
        {
            for (int i = 0; i < objetos.Length; i++)
            {
                if (spritesBons[i] == null)
                {
                    Destroy(objetos[i].gameObject);
                }
                else
                {
                    objetos[i].sprite = spritesBons[i];
                }
            }
        }
        else if (points <= mauDesempenho)
        {
            for (int i = 0; i < objetos.Length; i++) 
            {
                if (spritesMaus[i] == null)
                {
                    Destroy(objetos[i].gameObject);
                }
                else
                {
                    objetos[i].sprite = spritesMaus[i];
                }
            }
        }
        else
        {
            foreach (SpriteRenderer sr in objetos)
            {
                if (sr.sprite == null)
                {
                    Destroy(sr.gameObject);
                }
            }
        }
    }
}
