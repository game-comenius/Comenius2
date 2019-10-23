using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    public static GridScript gridScript;

    [SerializeField] private Color gridColor;

    [SerializeField] private Vector2 cellSize;

    [SerializeField] private float cellAngle;

    [SerializeField] private Vector2Int gridDim;

    [SerializeField] private float gridRotation;

    [SerializeField] private bool draw;

    [SerializeField] private Transform obj;

    public bool[,] vacancy;

    private void Awake()
    {
        if (gridScript == null)
        {
            gridScript = this;            
        }
        else
        {
            Debug.Log("Há dois GridScripts");
        }
    }

    #region Grid and transformations
    public Vector2[] Cell(Vector2Int r_cell)
    {
        Vector2[] infos = new Vector2[5];

        infos[0] = transform.position + Quaternion.Euler(0, 0, gridRotation) * (new Vector2(cellSize.x * r_cell.x + cellSize.x / 2, 0));

        infos[0] = infos[0] + (Vector2)(Quaternion.Euler(0, 0, gridRotation) * Quaternion.Euler(0, 0, cellAngle) * (new Vector2(0, cellSize.y * r_cell.y + cellSize.y / 2)));

        infos[1] = transform.position + Quaternion.Euler(0, 0, gridRotation) * (new Vector2(cellSize.x * r_cell.x, 0));

        infos[1] = infos[1] + (Vector2)(Quaternion.Euler(0, 0, gridRotation) * Quaternion.Euler(0, 0, cellAngle) * (new Vector2(0, cellSize.y * r_cell.y)));

        infos[2] = transform.position + Quaternion.Euler(0, 0, gridRotation) * (new Vector2(cellSize.x * (r_cell.x + 1), 0));

        infos[2] = infos[2] + (Vector2)(Quaternion.Euler(0, 0, gridRotation) * Quaternion.Euler(0, 0, cellAngle) * (new Vector2(0, cellSize.y * r_cell.y)));

        infos[3] = transform.position + Quaternion.Euler(0, 0, gridRotation) * (new Vector2(cellSize.x * r_cell.x, 0));

        infos[3] = infos[3] + (Vector2)(Quaternion.Euler(0, 0, gridRotation) * Quaternion.Euler(0, 0, cellAngle) * (new Vector2(0, cellSize.y * (r_cell.y + 1))));

        infos[4] = transform.position + Quaternion.Euler(0, 0, gridRotation) * (new Vector2(cellSize.x * (r_cell.x + 1), 0));

        infos[4] = infos[4] + (Vector2)(Quaternion.Euler(0, 0, gridRotation) * Quaternion.Euler(0, 0, cellAngle) * (new Vector2(0, cellSize.y * (r_cell.y + 1))));

        return infos;
    }

    public Vector2[] CellR(Vector2Int r_cell)
    {
        Vector2[] infos = new Vector2[5];

        infos[0] = (Vector2)transform.position + (new Vector2(cellSize.x * r_cell.x + cellSize.x / 2, 0));

        infos[0] = infos[0] + (new Vector2(0, cellSize.y * r_cell.y + cellSize.y / 2));

        infos[1] = (Vector2)transform.position + (new Vector2(cellSize.x * r_cell.x, 0));

        infos[1] = infos[1] + (new Vector2(0, cellSize.y * r_cell.y));

        infos[2] = (Vector2)transform.position + (new Vector2(cellSize.x * (r_cell.x + 1), 0));

        infos[2] = infos[2] + (new Vector2(0, cellSize.y * r_cell.y));

        infos[3] = (Vector2)transform.position + (new Vector2(cellSize.x * r_cell.x, 0));

        infos[3] = infos[3] + (new Vector2(0, cellSize.y * (r_cell.y + 1)));

        infos[4] = (Vector2)transform.position + (new Vector2(cellSize.x * (r_cell.x + 1), 0));

        infos[4] = infos[4] + (new Vector2(0, cellSize.y * (r_cell.y + 1)));

        return infos;
    }

    public Vector2 Regression(Vector2 position)
    {
        Vector2 regression = Vector2.zero;

        if (Quaternion.Euler(0, 0, cellAngle) != Quaternion.Euler(0, 0, 180) || Quaternion.Euler(0, 0, cellAngle) != Quaternion.Euler(0, 0, -180))
        {
            regression = Quaternion.Euler(0, 0, -gridRotation) * (position - ((Vector2)transform.position));

            float x = regression.x + regression.y * Mathf.Tan(Mathf.Deg2Rad * cellAngle);

            float y = regression.y / Mathf.Cos(Mathf.Deg2Rad * cellAngle);

            regression = new Vector2(x, y);

            regression += (Vector2)transform.position;
        }

        return regression;
    }

    public Vector2Int P2G(Vector2 point)
    {
        Vector2Int cell = Vector2Int.zero;

        point = Regression(point);

        point -= (Vector2)transform.position;

        point = new Vector2(Mathf.Clamp(point.x, 0, ((float)gridDim.x) * cellSize.x), Mathf.Clamp(point.y, 0, ((float)gridDim.y) * cellSize.y));

        cell = new Vector2Int(Mathf.Clamp(Mathf.FloorToInt(point.x / cellSize.x), 0, gridDim.x - 1), Mathf.Clamp(Mathf.FloorToInt(point.y / cellSize.y), 0, gridDim.y - 1));

        return cell;
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = gridColor;

        for (int i = 0; i <= gridDim.x; i++)
        {
            Vector2 ipos = (Vector2)transform.position + (new Vector2(cellSize.x * i, 0));

            Vector2 fpos = ipos + (new Vector2(0, cellSize.y * gridDim.y));

            Gizmos.DrawLine(ipos, fpos);
        }

        for (int j = 0; j <= gridDim.y; j++)
        {
            Vector2 ipos = (Vector2)transform.position + (new Vector2(0, cellSize.y * j));

            Vector2 fpos = ipos + (new Vector2(cellSize.x * gridDim.x, 0));

            Gizmos.DrawLine(ipos, fpos);
        }

        for (int i = 0; i <= gridDim.x; i++)
        {
            Vector2 ipos = transform.position + Quaternion.Euler(0, 0, gridRotation) * (new Vector2(cellSize.x * i, 0));

            Vector2 fpos = (Vector3)ipos + Quaternion.Euler(0, 0, gridRotation) * Quaternion.Euler(0, 0, cellAngle) * (new Vector2(0, cellSize.y * gridDim.y));

            Gizmos.DrawLine(ipos, fpos);
        }

        for (int j = 0; j <= gridDim.y; j++)
        {
            Vector2 ipos = transform.position + Quaternion.Euler(0, 0, gridRotation) * Quaternion.Euler(0, 0, cellAngle) * (new Vector2(0, cellSize.y * j));

            Vector2 fpos = (Vector3)ipos + Quaternion.Euler(0, 0, gridRotation) * (new Vector2(cellSize.x * gridDim.x, 0));

            Gizmos.DrawLine(ipos, fpos);
        }

        Gizmos.color = Color.red;

        try
        {
            for (int j = 0; j < gridDim.y; j++)
            {
                for (int i = 0; i < gridDim.x; i++)
                {
                    if (vacancy[i, j])
                    {
                        Gizmos.DrawSphere(Cell(new Vector2Int(i, j))[0], 0.1f);
                    }
                }
            }

            for (int j = 0; j < gridDim.y; j++)
            {
                for (int i = 0; i < gridDim.x; i++)
                {
                    if (vacancy[i, j])
                    {
                        Gizmos.DrawSphere(CellR(new Vector2Int(i, j))[0], 0.1f);
                    }
                }
            }
        }
        catch (System.NullReferenceException)
        {

        }

        if (draw && obj != null) 
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawSphere(obj.position, 0.1f);

            Gizmos.color = Color.green;

            Gizmos.DrawSphere(Regression(obj.position), 0.1f);

            Gizmos.DrawLine(transform.position, Regression(obj.position));

            Gizmos.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.5f);

            Gizmos.DrawCube(CellR(P2G(obj.position))[0], (Vector3)cellSize + Vector3.forward);
        }
    }
}