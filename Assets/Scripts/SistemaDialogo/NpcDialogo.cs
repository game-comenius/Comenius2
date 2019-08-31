using UnityEngine;
using UnityEngine.SceneManagement;
using GameComenius.Dialogo;

public class NpcDialogo : MonoBehaviour
{
    [SerializeField] private Dialogo dialogoPrincipal = new Dialogo();

    [HideInInspector] public bool jaFalou = false;

    public void OnMouseUp()
    {
        SistemaDialogo.sistemaDialogo.ComecarDialogo(dialogoPrincipal, this);

        if (!jaFalou)
        {
            //SistemaDialogoQA._sistemaDialogo.ComecarDialogo(dialogo, refenciaDialogo.Count, this);
            //DialogueSystem.dialogue.dialogo = dialogoPrincipal;
            //DialogueSystem.dialogue.IniciarConversa(this);
        }
        else
        {
            //int i = Mathf.FloorToInt(Random.Range(0, dialogosSecundarios.Length));

            //if (i == dialogosSecundarios.Length) { i -= 1; }

            //DialogueSystem.dialogue.dialogo = dialogosSecundarios[i];
            //DialogueSystem.dialogue.IniciarConversa(this);
        }
    }
}
