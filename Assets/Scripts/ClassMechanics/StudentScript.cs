using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentScript : AgenteAulaScript
{
    [SerializeField] private Vector3 _cloudOffset;

    [SerializeField] private bool drawGizmos;

    public Vector3 cloudOffset
    {
        get
        {
            return _cloudOffset;
        }
    }

    protected void Awake()
    {
        ClassManager.AddStundent(this);
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawSphere(transform.position + _cloudOffset, 0.1f);
        }
    }
}
