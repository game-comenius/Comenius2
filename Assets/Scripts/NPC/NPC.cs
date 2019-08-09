using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour ,IInteractable
{
    [SerializeField]
    private Window window;
    private int teste = 0;

    public bool IsInteracting { get; set; }

    public virtual void Interact()
    {
        if (!IsInteracting)
        {
            IsInteracting = true;
            window.Open(this);
        }
    }

    public virtual void StopInteract()
    {
        if (!IsInteracting)
        {
            IsInteracting = false;
            window.Close();
        }
    }
}

