using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AjudaMenuCustom : MonoBehaviour {

    public bool JaFoiExibida;

    private Canvas canvas;

    private BotaoAjudaMenuCustom botaoAjuda;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();

        botaoAjuda = FindObjectOfType<BotaoAjudaMenuCustom>();

        DefinirVisibilidadeDoCanvasAjuda(false);
    }

    public void Exibir()
    {
        DefinirVisibilidadeDoCanvasAjuda(true);
    }

    public void EsconderAjuda()
    {
        DefinirVisibilidadeDoCanvasAjuda(false);

        JaFoiExibida = true;
    }

    private void DefinirVisibilidadeDoCanvasAjuda(bool visibilidade)
    {
        canvas.enabled = visibilidade;
    }
}
