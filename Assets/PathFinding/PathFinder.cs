using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour 
{
    [SerializeField] Camera _camera = null;

    [SerializeField] private Vector3 footbaseOffset = Vector3.zero;

    [SerializeField] List<Vector2Int> path = new List<Vector2Int>();

    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonUp(1))
        {
            System.DateTime t = System.DateTime.UtcNow;

             path = GridScript.gridScript.FindPath(transform.position + footbaseOffset, _camera.ScreenToWorldPoint(Input.mousePosition));

            System.TimeSpan s = System.DateTime.UtcNow - t;

            Debug.Log(s.TotalMilliseconds + " ms");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position + footbaseOffset, 0.1f);

        if (path.Count > 1)
        {
            Gizmos.color = Color.blue;

            for (int i = 1; i < path.Count; i++)
            {
                Gizmos.DrawLine(GridScript.gridScript.CellR(path[i - 1])[0], GridScript.gridScript.CellR(path[i])[0]);
                Gizmos.DrawLine(GridScript.gridScript.Cell(path[i - 1])[0], GridScript.gridScript.Cell(path[i])[0]);
            }

            Gizmos.DrawSphere(GridScript.gridScript.Cell(path[0])[0], 0.15f);
        }
    }
}
