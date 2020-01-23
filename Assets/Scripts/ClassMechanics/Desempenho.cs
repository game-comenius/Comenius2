using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desempenho : MonoBehaviour
{
    [Tooltip ("Acima de [excluindo]")]
    [SerializeField] private double bomDesempenho;
    [Tooltip("Abaixo de [incluindo]")]
    [SerializeField] private double mauDesempenho;

    [SerializeField] private SpriteRenderer[] objetos = new SpriteRenderer[1];
    [SerializeField] private Sprite[] spritesBons = new Sprite[1];
    [SerializeField] private Sprite[] spritesMaus = new Sprite[1];

    private void Awake()
    {
        double points = Player.Instance.totalMissionPoints;

        if (points > bomDesempenho)
        {
            for (int i = 0; i < objetos.Length; i++)
            {
                if (spritesBons[i] == null)
                {
                    Destroy(objetos[i]);
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
                    Destroy(objetos[i]);
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
