using UnityEngine;
using UnityEngine.SceneManagement;
using GameComenius.Dialogo;

public class NpcDialogoQA : MonoBehaviour
{
    [SerializeField] private DialogoQuizz dialogoPrincipal = new DialogoQuizz();

    [HideInInspector] public bool jaFalou = false;

    public void OnMouseUp()
    {
        SistemaDialogoQA.sistemaDialogo.ComecarDialogo(dialogoPrincipal, this);

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
