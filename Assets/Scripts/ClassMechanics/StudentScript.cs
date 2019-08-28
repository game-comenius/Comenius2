using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentScript : MonoBehaviour
{
    [SerializeField] private Vector3 _cloudOffset;

    public Vector3 cloudOffset
    {
        get
        {
            return _cloudOffset;
        }
    }
    
    [SerializeField] private bool drawGizmos;

    private void Start()
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
