using UnityEngine;
using UnityEngine.SceneManagement;
using GameComenius.Dialogo;

public class NpcDialogo : MonoBehaviour
{
    [SerializeField] private Dialogo dialogoPrincipal = new Dialogo();

    [SerializeField] private Dialogo[] dialogosSecundarios = new Dialogo[1];

    [HideInInspector] public bool jaFalou = false;

    public void OnMouseUp()
    {
        if (!jaFalou)
        {
            SistemaDialogo.sistemaDialogo.ComecarDialogo(dialogoPrincipal, this);
        }
        else
        {
            int i = Random.Range(0, dialogosSecundarios.Length);

            if (i == dialogosSecundarios.Length) { i -= 1; }

            SistemaDialogo.sistemaDialogo.dialogo = dialogosSecundarios[i];
            SistemaDialogo.sistemaDialogo.ComecarDialogo(dialogosSecundarios[i], this);
        }
    }
}
