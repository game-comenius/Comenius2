using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonBar : MonoBehaviour
{
    public Sprite Selecionado;
    public Sprite Selecionado2;
    public Sprite NAO_Selecionado;
    public Text Descricao;
    public string piscando;
    public float timer = 0f;
    public float timer2 = 0f;
    public GameObject NPC;
    public GameObject Porta;

    public void Start()
    {
        Descricao.text = "";
        piscando = "";
    }

    public void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                if (piscando == "Sm")
                {
                    Selecao1();
                }
                else
                {
                    Selecao();
                }
            }
        }

        if (timer2 > 0f)
        {
            timer2 -= Time.deltaTime;

            if (timer2 <= 0f)
            {
                piscar();
            }
        }
    }

    public void Selecao()
    {
        GameObject.Find("Button (2)")
       .GetComponent<Image>()
       .sprite = Selecionado2;

        GameObject.Find("Button (1)")
       .GetComponent<Image>()
       .sprite = NAO_Selecionado;

        Descricao.text = "Pátio";
        piscando = "Pr";
        timer2 = 0.3f;
    }

    public void Selecao1()
    {
        GameObject.Find("Button (2)")
       .GetComponent<Image>()
       .sprite = NAO_Selecionado;

        GameObject.Find("Button (1)")
       .GetComponent<Image>()
       .sprite = Selecionado2;

        Descricao.text = "Sala de Multimeios";
        piscando = "Sm";
        timer2 = 0.3f;
    }

    public void piscar()
    {
        if (piscando == "Sm")
        {
            GameObject.Find("Button (1)")
           .GetComponent<Image>()
           .sprite = Selecionado;
        }

        if (piscando == "Pr")
        {
            GameObject.Find("Button (2)")
           .GetComponent<Image>()
           .sprite = Selecionado;
        }
        timer = 0.3f;
    }

    public void Com_Npc()
    {
        NPC.gameObject.SetActive(true);
        Porta.gameObject.SetActive(true);
    }

    public void Sem_Npc()
    {
        NPC.gameObject.SetActive(false);
        Porta.gameObject.SetActive(false);
    }
}