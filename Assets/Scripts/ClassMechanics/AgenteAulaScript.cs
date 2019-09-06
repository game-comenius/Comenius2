using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgenteAulaScript : MonoBehaviour
{
    virtual protected void Start()
    {
        if (GetComponent<Collider2D>())
        {
            ClassManager.EndClass += EnableCollider;
        }
    }

    virtual protected void EnableCollider()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    virtual protected void OnDisable()
    {
        if (GetComponent<Collider>())
        {
            ClassManager.EndClass -= EnableCollider;
        }
    }
}
