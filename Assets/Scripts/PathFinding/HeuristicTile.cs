using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeuristicTile
{
    public List<Vector2Int> path = new List<Vector2Int>();

    public Vector2Int direction = Vector2Int.zero;

    public int directionChanges = 0;

    public int distToGoal = 0;

    public int partialTileValue
    {
        get
        {
            return 10 * path.Count + 10 * directionChanges;
        }
    }

    public int totalTileValue
    {
        get
        {
            return 10 * path.Count + 5 * (directionChanges * directionChanges) + 15 * distToGoal;
        }
    }

    public HeuristicTile(Vector2Int position, Vector2Int goal)
    {
        direction = Vector2Int.zero;

        directionChanges = 0;

        path.Add(position);

        CalculateDistToGoal(goal);
    }

    public HeuristicTile(HeuristicTile tile, Vector2Int goal)
    {
        path.AddRange(tile.path);

        direction = tile.direction;

        directionChanges = tile.directionChanges;

        CalculateDistToGoal(goal);
    }

    public void CalculateDistToGoal(Vector2Int pathGoal)
    {
        distToGoal = Mathf.Abs(pathGoal.x - path[path.Count - 1].x) + Mathf.Abs(pathGoal.y - path[path.Count - 1].y);
    }
}
