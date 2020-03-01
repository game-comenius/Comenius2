using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public class GridScript : MonoBehaviour
{
    public static GridScript gridScript;

    [SerializeField] private Vector2 cellSize;

    [SerializeField] private float cellAngle;

    [SerializeField] private Vector2Int gridDim; //O nome desta var é usado no GridScriptEditor em gridDimSP = serializedObject.FindProperty("gridDim").

    [SerializeField] private float gridRotation;

    [HideInInspector] [SerializeField] private uint[] vacancy = new uint[5]; //true = ocupado, false = livre. O nome desta var é usado no GridScriptEditor em vacancySP = serializedObject.FindProperty("vacancy").

    private void Awake()
    {
        if (gridScript == null)
        {
            gridScript = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #region Grid and transformations
    public Vector2[] Cell(Vector2Int r_cell) //Retorna as coordenadas distrocidas de: 0 - o centro da casa; 1,2,3,4 - os quatro cantos. A entrada são as coordenada da casa no grid. 
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

    public Vector2[] CellR(Vector2Int r_cell) //Retorna as coordenadas ortogonais aos eixos do mundo real de: 0 - o centro da casa; 1,2,3,4 - os quatro cantos. A entrada são as coordenada da casa no grid.
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

    public Vector2 Regression(Vector2 position) //Retorna as coordenadas de um ponto no grid reta apartir das suas coordenadas no grid distorcido
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

    public Vector2Int P2G(Vector2 point) //Retorna as coordenadas do grid de um ponto de coordenadas não distorcidas.
    {
        Vector2Int cell = Vector2Int.zero;

        point = Regression(point);

        point -= (Vector2)transform.position;

        point = new Vector2(Mathf.Clamp(point.x, 0, ((float)gridDim.x) * cellSize.x), Mathf.Clamp(point.y, 0, ((float)gridDim.y) * cellSize.y));

        cell = new Vector2Int(Mathf.Clamp(Mathf.FloorToInt(point.x / cellSize.x), 0, gridDim.x - 1), Mathf.Clamp(Mathf.FloorToInt(point.y / cellSize.y), 0, gridDim.y - 1));

        return cell;
    }

    public bool IsOccupied(Vector2Int position) //Retorna se uma determinada casa, de coordenadas "position", está ocupada (true) ou não (false).
    {
        return (vacancy[position.x] & (uint)(1 << position.y)) == (uint)(1 << position.y);
    }

    public void ChangeGrid(Vector2Int position, bool occupy)
    {
        uint i = (uint)(1 << position.y);

        if (occupy)
        {
            vacancy[position.x] = vacancy[position.x] & (~i);
        }
        else
        {
            vacancy[position.x] = vacancy[position.x] | i;
        }
    }
    #endregion

    #region Path Finding
    //a nálise é feita de forma reversa, não da origem ao destino mas do destino à origem.
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

            if (IsOccupied(destino))  
            {
                return UnavailableEnd(origem, paths);
            }
            else
            {
                return AvailableEnd(origem, paths);
            }
        }
    }

    private List<Vector2Int> UnavailableEnd(Vector2Int origem, List<HeuristicTile> paths) //Quando o destino da Lurdinha é uma casa já ocupada.
    {
        bool[,] wasAnalysed = new bool[gridDim.x, gridDim.y]; //Ter persistencia de quais casas já foram analisadas.

        wasAnalysed[paths[0].path[0].x, paths[0].path[0].y] = true; //paths[0].path[0] é a casa destino, já sabemos que elas é uma casa ocupada, então ela já foi analisada.

        List<Vector2Int> positions = new List<Vector2Int>(); //Lista de casas que serão analisadas.

        positions.Add(paths[0].path[0]);

        paths.RemoveAt(0);

        bool found = false;

        while (!found)
        {
            List<Vector2Int> newPositions = new List<Vector2Int>(); //Guardar e trabalhos com as novas possível posições.

            while (positions.Count > 0) //Gera as novas posições
            {
                newPositions.AddRange(NewPositionsUnavailableEnd(positions[0]));

                positions.RemoveAt(0);
            }

            for (int i = 0; i < newPositions.Count - 1; i++) //Verifica se há posições duplicadas e as remove
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
                if (wasAnalysed[newPositions[i].x, newPositions[i].y]) //Verifica se essa casa já foi analisada.
                {
                    newPositions.RemoveAt(i);

                    i--;
                }
                else if (!IsOccupied(newPositions[i])) //Verifica se alguma das casas pode ser ocupada pela Lurdinha.
                {
                    found = true;

                    i = newPositions.Count;
                }
            }

            positions.AddRange(newPositions);
        }

        for (int i = 0; i < positions.Count; i++)
        {
            if (positions[i] == origem) //Verifica se alguma das casas é onde a Lurdinha está.
            {
                return null;
            }
            else if (!IsOccupied(positions[i])) //Adiciona as casas que Lurdinha pode ocupar à lista paths.
            {
                paths.Add(new HeuristicTile(positions[i], origem));                
            }
        }

        return AvailableEnd(origem, paths); //Tendo os possíveis destinos, procura-se a melhor rota até a Lurdinha.
    }

    private List<Vector2Int> NewPositionsUnavailableEnd(Vector2Int origem) //Retorna uma lista de possíveis casas que pode-se ir apartir de origem.
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
            }

            if (VerifyTileIsInGrid(position))
            {
                newList.Add(position);
            }
        }

        return newList;
    }

    public List<Vector2Int> FindPathToInteractable(Vector3 _origemWorldpoint, Vector3[] _destinoWorldPoint) //É chamado por uma objeto interagível.
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

    //Procura um caminho.
    private List<Vector2Int> AvailableEnd(Vector2Int origem, List<HeuristicTile> paths)
    {
        while (true)
        {
            List<HeuristicTile> newList = new List<HeuristicTile>();

            newList.AddRange(NewPositionsAvailableEnd(origem, paths[0])); //Gera novas casa para serem analisadas

            paths.RemoveAt(0);  //Remove a casa que foi usada para gerar as novas.

            if (paths.Count == 0 && newList.Count != 0)
            {
                //Verifica se já chegou.
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
                    if (paths[j].path[paths[j].path.Count - 1] == newList[i].path[newList[i].path.Count - 1]) //Verifica se existe mais de uma caminhos para a mesma casa e remove a mais desvantajosa.
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
                        for (int k = 0; k < paths[j].path.Count - 1 && k < newList[i].path.Count; k++) //Verifica se algum caminhos já passou por ali.
                        {
                            if (paths[j].path[k] == newList[i].path[newList[i].path.Count - 1])
                            {
                                if (10 * k + 5 * (newList[i].directionChanges) * (newList[i].directionChanges) > 10 * newList[i].path.Count + 5 * (newList[i].directionChanges) * (newList[i].directionChanges))
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
                //Verifica se já chegou.
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

                    while (a <= b)
                    {
                        if (a + 1 == b)
                        {
                            paths.Insert(b, newList[i]);

                            break;
                        }

                        int j = (a + b) / 2;

                        if (paths[j].totalTileValue > newList[i].totalTileValue)
                        {
                            b = Mathf.Max(j, a + 1);
                        }
                        else if (paths[j].totalTileValue < newList[i].totalTileValue)
                        {
                            a = Mathf.Min(j, b - 1);
                        }
                        else if (paths[j].totalTileValue == newList[i].totalTileValue)
                        {
                            paths.Insert(j, newList[i]);

                            break;
                        }
                    }
                }
            }
        }
    }

    private List<HeuristicTile> NewPositionsAvailableEnd(Vector2Int destino, HeuristicTile tile) //Retorna novas casas.
    {
        List<HeuristicTile> newTiles = new List<HeuristicTile>();

        for (int i = 0; i < 4; i++)
        {
            Vector2Int newDir = Vector2Int.zero;

            Vector2Int newPosition = tile.path[tile.path.Count - 1];

            switch (i) //Gera a nova casa.
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

            //Vefirica-se se não já se passou alina, se faz parte do grid e se não está ocupada e consolida a nova casa.
            if (!tile.path.Contains(newPosition) && VerifyTileIsInGrid(newPosition) && (!IsOccupied(newPosition)))  
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

    private bool VerifyTileIsInGrid(Vector2Int newPosition) //Verifica se uma casa faz parte do grid.
    {
        return (newPosition.x >= 0 && newPosition.x < gridScript.gridDim.x && newPosition.y >= 0 && newPosition.y < gridScript.gridDim.y);
    }
    #endregion

    #region Gizmos
#if UNITY_EDITOR
    [SerializeField] private Color gridColor;

    [SerializeField] private bool debug;

    [SerializeField] private Vector2Int cell;

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.magenta;

            Gizmos.DrawSphere(Cell(cell)[0], 0.2f);
        }

        Gizmos.color = gridColor;

        //Desenha o grid reto.
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

        //Desenha o grid distorcido.
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

        //Desenha a esferar centrais que indicam se a casa está sendo usada.
        try
        {
            for (int j = 0; j < gridDim.y; j++)
            {
                for (int i = 0; i < gridDim.x; i++)
                {
                    if (IsOccupied(new Vector2Int(i, j))) 
                    {
                        Gizmos.color = Color.red;

                        Gizmos.DrawSphere(CellR(new Vector2Int(i, j))[0], 0.1f); //Desenha no grid reto.
                    }
                    else
                    {
                        Gizmos.color = Color.cyan;
                    }

                    Gizmos.DrawSphere(Cell(new Vector2Int(i, j))[0], 0.06f); //Desenha no grid distorcido.
                }
            }
        }
        catch (System.NullReferenceException)
        {

        }
    }
    #endif
    #endregion
}