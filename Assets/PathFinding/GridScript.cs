using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    public static GridScript gridScript;

    [SerializeField] private Color gridColor;

    [SerializeField] private Vector2 cellSize;

    [SerializeField] private float cellAngle;

    [SerializeField] private Vector2Int gridDim;

    [SerializeField] private float gridRotation;

    [HideInInspector] [SerializeField] private string save;

    public bool[,] vacancy;

    [SerializeField] List<HeuristicTile> paths = new List<HeuristicTile>();//deletar

    Vector2Int _destino = Vector2Int.zero;
    Vector2Int _origem = Vector2Int.zero;

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

        Load();
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

    public void Load()
    {
        if (File.Exists(save))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(save, FileMode.Open);

            vacancy = bf.Deserialize(file) as bool[,];

            file.Close();
        }
    }
    #endregion

    #region Path Finding

    //a nálise é feita de forma reversa, não da origam ao destino mas do destino à origem
    public List<Vector2Int> FindPath(Vector3 _origemWorldpoint, Vector3 _destinoWorldPoint)
    {
        paths.Clear();

        Vector2Int origem = P2G(_origemWorldpoint);
        Vector2Int destino = P2G(_destinoWorldPoint);
        _origem = origem;
        _destino = destino;

        if (origem == destino)
        {
            Debug.Log("Não anda");

            return null;
        }
        else
        {
            paths.Add(new HeuristicTile(destino, origem));

            if (vacancy[destino.x, destino.y])
            {
                return UnavailableEnd(origem, paths);

                //AvailableEnd(origem, paths);

                //return AvailableEnd(origem, paths);
            }
            else
            {
                //AvailableEnd(origem, paths);

                return AvailableEnd(origem, paths);
            }
        }
    }

    private List<Vector2Int> UnavailableEnd(Vector2Int origem, List<HeuristicTile> paths)
    {
        while (true) 
        {
            List<HeuristicTile> newList = new List<HeuristicTile>();

            while (paths.Count > 0)
            {
                HeuristicTile tile = new HeuristicTile(paths[paths.Count - 1], origem);

                paths.RemoveAt(paths.Count - 1);

                newList.AddRange(NewPositionsUnavailableEnd(origem, tile));
            }

            bool achouLugar = false;

            for (int i = 0; i < newList.Count; i++)
            {
                Vector2Int position = newList[i].path[newList[i].path.Count - 1];

                if (!vacancy[position.x, position.y])
                {
                    achouLugar = true;

                    break;
                }
            }

            if (achouLugar)
            {
                for (int i = 0; i < newList.Count; i++)
                {
                    Vector2Int position = newList[i].path[newList[i].path.Count - 1];

                    if (vacancy[position.x, position.y])
                    {
                        newList.RemoveAt(i);

                        i--;
                    }
                    else
                    {
                        while (newList[i].path.Count > 1)
                        {
                            newList[i].path.RemoveAt(0);
                        }

                        newList[i].CalculateDistToGoal(origem);

                        if (paths.Count == 0)
                        {
                            paths.Add(newList[i]);
                        }
                        else
                        {
                            for (int j = 0; j < paths.Count; j++)
                            {
                                if (newList[i].path[newList[i].path.Count - 1] == origem)
                                {
                                    paths.Clear();

                                    return newList[i].path;
                                }
                                else if (paths[j].totalTileValue > newList[i].totalTileValue)
                                {
                                    paths.Insert(j, newList[i]);

                                    break;
                                }
                                else if (j == paths.Count - 1)
                                {
                                    paths.Add(newList[i]);

                                    break;
                                }
                            }

                            newList.RemoveAt(i);
                        }
                    }
                }

                return AvailableEnd(origem, paths);
            }
            else
            {
                paths.AddRange(newList);
            }
        }
    }

    private List<HeuristicTile> NewPositionsUnavailableEnd(Vector2Int destino, HeuristicTile tile)
    {
        List<HeuristicTile> newTiles = new List<HeuristicTile>();

        Vector2Int newPosition = tile.path[tile.path.Count - 1] + new Vector2Int(1, 0);

        if (!tile.path.Contains(newPosition) && VerifyTileIsInGrid(newPosition))
        {
            HeuristicTile newTile = new HeuristicTile(tile, destino);

            newTile.path.Add(newPosition);

            newTiles.Add(newTile);
        }

        newPosition = tile.path[tile.path.Count - 1] + new Vector2Int(-1, 0);

        if (!tile.path.Contains(newPosition) && VerifyTileIsInGrid(newPosition))
        {
            HeuristicTile newTile = new HeuristicTile(tile, destino);

            newTile.path.Add(newPosition);

            newTiles.Add(newTile);
        }

        newPosition = tile.path[tile.path.Count - 1] + new Vector2Int(0, 1);

        if (!tile.path.Contains(newPosition) && VerifyTileIsInGrid(newPosition))
        {
            HeuristicTile newTile = new HeuristicTile(tile, destino);

            newTile.path.Add(newPosition);

            newTiles.Add(newTile);
        }

        newPosition = tile.path[tile.path.Count - 1] + new Vector2Int(0, -1);

        if (!tile.path.Contains(newPosition) && VerifyTileIsInGrid(newPosition))
        {
            HeuristicTile newTile = new HeuristicTile(tile, destino);

            newTile.path.Add(newPosition);

            newTiles.Add(newTile);
        }

        return newTiles;
    }

    private List<Vector2Int> AvailableEnd(Vector2Int origem, List<HeuristicTile> paths)
    {
        while(true)
        {
            List<HeuristicTile> newList = new List<HeuristicTile>();

            newList.AddRange(NewPositionsAvailableEnd(origem, paths[0]));

            paths.RemoveAt(0);

            if (paths.Count == 0)
            {
                paths.Add(newList[0]);

                newList.RemoveAt(0);
            }

            for (int i = 0; i < newList.Count; i++)
            {
                for (int j = 0; j < paths.Count; j++)
                {
                    if (paths[j].path.Count > 0 && paths[j].path[paths[j].path.Count - 1] == newList[i].path[newList[i].path.Count - 1])
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
                }
            }

            for (int i = 0; i < newList.Count; i++)
            {
                if (newList[i].path[newList[i].path.Count - 1] == origem)
                {
                    paths.Clear();

                    return newList[i].path;

                    //paths.Add(newList[i]);

                    //return;
                }

                for (int j = 0; j < paths.Count; j++)
                {
                    if (paths[j].totalTileValue > newList[i].totalTileValue)
                    {
                        paths.Insert(j, newList[i]);

                        break;
                    }
                    else if (j == paths.Count - 1)
                    {
                        paths.Add(newList[i]);

                        break;
                    }
                }
            }
        }
    }

    private List<HeuristicTile> NewPositionsAvailableEnd(Vector2Int destino, HeuristicTile tile)
    {
        List<HeuristicTile> newTiles = new List<HeuristicTile>();

        Vector2Int newDir = Vector2Int.up;

        Vector2Int newPosition = tile.path[tile.path.Count - 1] + newDir;

        if (!tile.path.Contains(newPosition) && VerifyTileIsInGrid(newPosition) && !vacancy[newPosition.x, newPosition.y]) 
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

        newDir = Vector2Int.down;

        newPosition = tile.path[tile.path.Count - 1] + newDir;

        if (!tile.path.Contains(newPosition) && VerifyTileIsInGrid(newPosition) && !vacancy[newPosition.x, newPosition.y])
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

        newDir = Vector2Int.left;

        newPosition = tile.path[tile.path.Count - 1] + newDir;

        if (!tile.path.Contains(newPosition) && VerifyTileIsInGrid(newPosition) && !vacancy[newPosition.x, newPosition.y])
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

        newDir = Vector2Int.right;

        newPosition = tile.path[tile.path.Count - 1] + newDir;

        if (!tile.path.Contains(newPosition) && VerifyTileIsInGrid(newPosition) && !vacancy[newPosition.x, newPosition.y])
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

        return newTiles;
    }

    private bool VerifyTileIsInGrid(Vector2Int newPosition)
    {
        return (newPosition.x >= 0 && newPosition.x < GridScript.gridScript.vacancy.GetLength(0) && newPosition.y >= 0 && newPosition.y < GridScript.gridScript.vacancy.GetLength(1));
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

        if (paths.Count > 0)
        {
            for (int i = paths.Count - 1; i >= 0; i--)
            {
                Gizmos.color = Color.Lerp(Color.cyan, Color.yellow, ((float)i) / Mathf.Clamp((float)paths.Count - 1, 1, paths.Count));

                Gizmos.DrawCube(GridScript.gridScript.CellR(paths[i].path[paths[i].path.Count - 1])[0], new Vector3(0.3f, 0.3f, 0.3f));

            }

            Gizmos.color = Color.green;
            Gizmos.DrawCube(GridScript.gridScript.CellR(_origem)[0], new Vector3(0.3f, 0.3f, 0.3f));

            Gizmos.color = Color.red;
            Gizmos.DrawCube(GridScript.gridScript.CellR(_destino)[0], new Vector3(0.3f, 0.3f, 0.3f));

            //if (paths[0].path.Count > 1)
            //{
            //    Gizmos.color = Color.blue;

            //    for (int i = 1; i < paths[0].path.Count; i++)
            //    {
            //        Gizmos.DrawLine(GridScript.gridScript.CellR(paths[0].path[i - 1])[0], GridScript.gridScript.CellR(paths[0].path[i])[0]);
            //        Gizmos.DrawLine(GridScript.gridScript.Cell(paths[0].path[i - 1])[0], GridScript.gridScript.Cell(paths[0].path[i])[0]);
            //    }

            //    Gizmos.DrawSphere(GridScript.gridScript.Cell(paths[0].path[0])[0], 0.15f);
            //}
        }
    }
}