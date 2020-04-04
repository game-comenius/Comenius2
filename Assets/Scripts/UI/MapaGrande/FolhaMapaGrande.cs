using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolhaMapaGrande : MonoBehaviour {

    private bool aberto;
    public bool Aberto
    {
        get { return aberto; }
        set
        {
            aberto = value;
            canvasGroup.blocksRaycasts = aberto;
            canvasGroup.alpha = aberto ? 1 : 0;
        }
    }

    public LocalNoMapa PatioEsquerdo;
    public LocalNoMapa PatioDireito;
    public LocalNoMapa SalaDiretor;
    public LocalNoMapa Coordenacao;
    public LocalNoMapa SalaProfessores;
    public LocalNoMapa Biblioteca;
    public LocalNoMapa SalaMultimeios;
    public LocalNoMapa SalaInformatica;
    public LocalNoMapa SalaAulaCelestino;
    public LocalNoMapa SalaAulaPaulino;
    public LocalNoMapa SalaAulaVladmir;
    public LocalNoMapa SalaAulaJean;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        Aberto = false;
    }

    // Use this for initialization
    void Start () {
        //PatioEsquerdo.NomeDaCena = "Patio_1";
        //PatioDireito.NomeDaCena = null;
        //SalaDiretor.NomeDaCena = null;
        //Coordenacao.NomeDaCena = "CoordenacaoPedagogica";
        //SalaProfessores.NomeDaCena = "SalaProfessores";
        //Biblioteca.NomeDaCena = "Biblioteca";
        SalaMultimeios.NomeDaCena = "Multimeios";
        //SalaInformatica.NomeDaCena = null;
        //SalaAulaCelestino.NomeDaCena = null;
        //SalaAulaPaulino.NomeDaCena = null;
        //SalaAulaVladmir.NomeDaCena = null;
        //SalaAulaJean.NomeDaCena = "Sala_de_aula_Jean";
    }

    public bool Teletransportar(LocalNoMapa local)
    {
        var cena = local.NomeDaCena;
        if (cena == null)
        {
            return false;
        }
        else
        {
            this.gameObject.SetActive(false);
            GetComponent<SceneLoader>().LoadNewScene(cena);
            return true;
        }
    }
}
