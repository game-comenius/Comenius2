using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjudaMenuCustom : MonoBehaviour {

	private bool jaFoiExibida;

    private void OnEnable()
    {
        if (jaFoiExibida) return;

        Exibir();
        jaFoiExibida = true;
    }

    public void Exibir()
    {
        Debug.Log("exibir!");
    }
}
