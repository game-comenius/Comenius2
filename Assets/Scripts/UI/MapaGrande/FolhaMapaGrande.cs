using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolhaMapaGrande : MonoBehaviour {

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

        gameObject.SetActive(false);
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
            GetComponent<SceneLoader>().LoadNewScene(cena);
            return true;
        }
    }
}
