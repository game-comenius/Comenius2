using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgenteAulaScript : MonoBehaviour
{
    virtual protected void Start()
    {
        if (GetComponent<NpcDialogo>() && GetComponent<BoxCollider2D>()) 
        {
            ClassManager.EndClass += HabilitarDialogo;
        }
    }

    virtual protected void HabilitarDialogo()
    {
        GetComponent<NpcDialogo>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    virtual protected void OnDisable()
    {
        if (GetComponent<NpcDialogo>() && GetComponent<BoxCollider2D>())
        {
            ClassManager.EndClass += HabilitarDialogo;
        }
    }
}
