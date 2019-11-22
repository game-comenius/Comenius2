using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public class GridScript : MonoBehaviour
{
    public static GridScript gridScript;

    [SerializeField] private Color gridColor;

    [SerializeField] private Vector2 cellSize;

    [SerializeField] private float cellAngle;

    [SerializeField] private Vector2Int gridDim;

    [SerializeField] private float gridRotation;

    [HideInInspector] [SerializeField] private string save;

    public uint[] newVacancy = new uint[5];

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

        //Load();
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

    //public void Load()
    //{
    //    if (File.Exists(Application.streamingAssetsPath + save))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();

    //        FileStream file = File.Open(Application.streamingAssetsPath + save, FileMode.Open);

    //        vacancy = bf.Deserialize(file) as bool[,];

    //        file.Close();

    //        newVacancy = new uint[vacancy.GetLength(0)];

    //        if (vacancy.GetLength(1) <= 32)
    //        {
    //            for (int i = 0; i < vacancy.GetLength(0); i++)
    //            {
    //                for (int j = 0; j < vacancy.GetLength(1); j++)
    //                {
    //                    if (vacancy[i, j])
    //                    {
    //                        newVacancy[i] = (newVacancy[i] | (uint)(1 << j));
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            Debug.Log("Grid grande demais");
    //        }

    //        //UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
    //    }
    //}

    #endregion

    #region Path Finding

    //a nálise é feita de forma reversa, não da origam ao destino mas do destino à origem
    public List<Vector2Int> FindPath(Vector3 _origemWorldpoint, Vector3 _destinoWorldPoint)
    {
        Vector2Int origem = P2G(_origemWorldpoint);
        Vector2Int destino = P2G(_destinoWorldPoint);

        if (origem == destino)
        {
            return null;
        }
        else
        {
            List<HeuristicTile> paths = new List<HeuristicTile>();

            paths.Add(new HeuristicTile(destino, origem));

            if ((newVacancy[destino.x] & (uint)(1 << destino.y)) == (uint)(1 << destino.y)) 
            {
                return UnavailableEnd(origem, paths);
            }
            else
            {
                return AvailableEnd(origem, paths);
            }
        }
    }

    public List<Vector2Int> FindPathToInteractable(Vector3 _origemWorldpoint, Vector3[] _destinoWorldPoint)
    {
        Vector2Int origem = P2G(_origemWorldpoint);

        Vector2Int[] destino = new Vector2Int[_destinoWorldPoint.Length];

        List<HeuristicTile> paths = new List<HeuristicTile>();

        for (int i = 0; i < destino.Length; i++)
        {
            destino[i] = P2G(_destinoWorldPoint[i]);

            if (destino[i] == origem)
            {
                return null;
            }
            else
            {
                paths.Add(new HeuristicTile(destino[i], origem));
            }
        }

        return AvailableEnd(origem, paths);
    }

    private List<Vector2Int> UnavailableEnd(Vector2Int origem, List<HeuristicTile> paths)
    {
        bool[,] wasAnalysed = new bool[gridDim.x, gridDim.y];

        wasAnalysed[paths[0].path[0].x, paths[0].path[0].y] = true;

        List<Vector2Int> positions = new List<Vector2Int>();

        positions.Add(paths[0].path[0]);

        bool found = false;

        while (!found)
        {
            List<Vector2Int> newPositions = new List<Vector2Int>();

            while (positions.Count > 0)
            {
                newPositions.AddRange(NewPositionsUnavailableEnd(positions[0]));

                positions.RemoveAt(0);
            }

            for (int i = 0; i < newPositions.Count - 1; i++) 
            {
                for (int j = i + 1; j < newPositions.Count; j++)
                {
                    if (newPositions[i] == newPositions[j])
                    {
                        newPositions.RemoveAt(j);

                        j--;
                    }
                }
            }

            for (int i = 0; i < newPositions.Count; i++)
            {
                if (wasAnalysed[newPositions[i].x, newPositions[i].y])
                {
                    newPositions.RemoveAt(i);

                    i--;
                }
                else if ((newVacancy[newPositions[i].x] & (uint)(1 << newPositions[i].y)) != (uint)(1 << newPositions[i].y))
                {
                    found = true;

                    i = newPositions.Count;
                }
            }

            positions.AddRange(newPositions);
        }

        for (int i = 0; i < positions.Count; i++)
        {
            if (positions[i] == origem)
            {
                return null;
            }
            else if ((newVacancy[positions[i].x] & (uint)(1 << positions[i].y)) != (uint)(1 << positions[i].y)) 
            {
                paths.Add(new HeuristicTile(positions[i], origem));                
            }
        }

        return AvailableEnd(origem, paths);
    }

    private List<Vector2Int> NewPositionsUnavailableEnd(Vector2Int origem)
    {
        List<Vector2Int> newList = new List<Vector2Int>();

        for (int i = 0; i < 4; i++)
        {
            Vector2Int position = Vector2Int.zero;

            switch (i)
            {
                case 0:
                    position = origem + Vector2Int.up;
                    break;
                case 1:
                    position = origem + Vector2Int.down;
                    break;
                case 2:
                    position = origem + Vector2Int.left;
                    break;
                case 3:
                    position = origem + Vector2Int.right;
                    break;
                //case 4:
                //    position = origem + Vector2Int.down + Vector2Int.left;
                //    break;
                //case 5:
                //    position = origem + Vector2Int.down + Vector2Int.right;
                //    break;
                //case 6:
                //    position = origem + Vector2Int.up + Vector2Int.left;
                //    break;
                //case 7:
                //    position = origem + Vector2Int.up + Vector2Int.right;
                //    break;
            }

            if (VerifyTileIsInGrid(position))
            {
                newList.Add(position);
            }
        }

        return newList;
    }

    private List<Vector2Int> AvailableEnd(Vector2Int origem, List<HeuristicTile> paths)
    {
        int z = 0;

        while (true)
        {
            List<HeuristicTile> newList = new List<HeuristicTile>();

            newList.AddRange(NewPositionsAvailableEnd(origem, paths[0]));

            paths.RemoveAt(0);            

            if (paths.Count == 0 && newList.Count != 0)
            {
                if (newList[0].path[newList[0].path.Count - 1] == origem)
                {
                    return newList[0].path;
                }

                paths.Add(newList[0]);

                newList.RemoveAt(0);
            }

            for (int i = 0; i < newList.Count; i++)
            {
                for (int j = 0; j < paths.Count; j++)
                {
                    if (paths[j].path[paths[j].path.Count - 1] == newList[i].path[newList[i].path.Count - 1])
                    {
                        if (paths[j].partialTileValue < newList[i].partialTileValue)
                        {
                            newList.RemoveAt(i);

                            i--;

                            j = paths.Count;
                        }
                        else if (paths[j].partialTileValue > newList[i].partialTileValue)
                        {
                            paths.Remove(paths[j]);

                            j--;
                        }
                    }
                    else
                    {
                        for (int k = 0; k < paths[j].path.Count - 1 && k < newList[i].path.Count; k++)
                        {
                            if (paths[j].path[k] == newList[i].path[newList[i].path.Count - 1])
                            {
                                if (10 * k + 5 * (newList[i].directionChanges + 1) * (newList[i].directionChanges + 1) <
                                    10 * newList[i].path.Count + 5 * (newList[i].directionChanges + 1) * (newList[i].directionChanges + 1))
                                {
                                    newList.RemoveAt(i);

                                    i--;

                                    j = paths.Count;

                                    break;
                                }
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < newList.Count; i++)
            {
                if (newList[i].path[newList[i].path.Count - 1] == origem)
                {
                    return newList[i].path;
                }

                if (newList[i].totalTileValue < paths[0].totalTileValue)
                {
                    paths.Insert(0, newList[i]);
                }
                else if (newList[i].totalTileValue > paths[paths.Count - 1].totalTileValue)
                {
                    paths.Add(newList[i]);
                }
                else
                {
                    int a = 0;
                    int b = paths.Count - 1;

                    while (a < b)
                    {
                        if (a + 1 == b)
                        {
                            paths.Insert(b, newList[i]);

                            break;
                        }

                        int j = (a + b) / 2;

                        if (paths[j].totalTileValue > newList[i].totalTileValue)
                        {
                            b = j;
                        }
                        else if (paths[j].totalTileValue < newList[i].totalTileValue)
                        {
                            a = j;
                        }
                        else if (paths[j].totalTileValue == newList[i].totalTileValue)
                        {
                            paths.Insert(j, newList[i]);

                            break;
                        }
                    }
                }
            }

            z++;
        }
    }

    private List<HeuristicTile> NewPositionsAvailableEnd(Vector2Int destino, HeuristicTile tile)
    {
        List<HeuristicTile> newTiles = new List<HeuristicTile>();

        for (int i = 0; i < 4; i++)
        {
            Vector2Int newDir = Vector2Int.zero;

            Vector2Int newPosition = tile.path[tile.path.Count - 1];

            switch (i)
            {
                case 0:
                    newDir = Vector2Int.up;
                    newPosition += newDir;
                    break;
                case 1:
                    newDir = Vector2Int.down;
                    newPosition += newDir;
                    break;
                case 2:
                    newDir = Vector2Int.left;
                    newPosition += newDir;
                    break;
                case 3:
                    newDir = Vector2Int.right;
                    newPosition += newDir;
                    break;
            }

            if (!tile.path.Contains(newPosition) && VerifyTileIsInGrid(newPosition) &&
                (newVacancy[newPosition.x] & (uint)(1 << newPosition.y)) != (uint)(1 << newPosition.y))
            {
                HeuristicTile newTile = new HeuristicTile(tile, destino);

                newTile.path.Add(newPosition);

                if (tile.direction != newDir)
                {
                    newTile.direction = newDir;

                    newTile.directionChanges++;
                }

                newTile.CalculateDistToGoal(destino);

                newTiles.Add(newTile);
            }
        }

        return newTiles;
    }

    private bool VerifyTileIsInGrid(Vector2Int newPosition)
    {
        return (newPosition.x >= 0 && newPosition.x < gridScript.gridDim.x && newPosition.y >= 0 && newPosition.y < gridScript.gridDim.y);
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

        try
        {
            for (int j = 0; j < gridDim.y; j++)
            {
                for (int i = 0; i < gridDim.x; i++)
                {
                    if ((newVacancy[i] & (uint)(1 << j)) == (uint)(1 << j)) 
                    {
                        Gizmos.color = Color.red;
                    }
                    else
                    {
                        Gizmos.color = Color.cyan;
                    }

                    Gizmos.DrawSphere(Cell(new Vector2Int(i, j))[0], 0.06f);
                }
            }

            Gizmos.color = Color.red;

            for (int j = 0; j < gridDim.y; j++)
            {
                for (int i = 0; i < gridDim.x; i++)
                {
                    if ((newVacancy[i] & (uint)(1 << j)) == (uint)(1 << j))
                    {
                        Gizmos.DrawSphere(CellR(new Vector2Int(i, j))[0], 0.1f);
                    }
                }
            }
        }
        catch (System.NullReferenceException)
        {

        }
    }
}