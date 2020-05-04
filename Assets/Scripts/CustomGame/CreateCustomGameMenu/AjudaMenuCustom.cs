using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjudaMenuCustom : MonoBehaviour {

	private bool jaFoiExibida;

    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        DefinirVisibilidadeDoCanvasAjuda(false);
    }

    public void Exibir()
    {
        if (jaFoiExibida) return;

        DefinirVisibilidadeDoCanvasAjuda(true);
    }

    public void EsconderAjuda()
    {
        DefinirVisibilidadeDoCanvasAjuda(false);

        jaFoiExibida = true;
    }

    private void DefinirVisibilidadeDoCanvasAjuda(bool visibilidade)
    {
        canvas.enabled = visibilidade;
    }
}
