using UnityEngine;

public class npcDialogue : MonoBehaviour
{
    public Dialogue dialogoPrincipal;

    public Dialogue[] dialogosSecundarios;

    [HideInInspector]
    public bool jaFalou = false;

    public void OnMouseUp()
    {
        if (!jaFalou)
        {
            DialogueSystem.dialogue.dialogo = dialogoPrincipal;
            DialogueSystem.dialogue.IniciarConversa(this);
        }
        else
        {
            int i = Mathf.FloorToInt(Random.Range(0, dialogosSecundarios.Length));

            if (i == dialogosSecundarios .Length) { i -= 1; }

            DialogueSystem.dialogue.dialogo = dialogosSecundarios[i];
            DialogueSystem.dialogue.IniciarConversa(this);
        }
    }
}

