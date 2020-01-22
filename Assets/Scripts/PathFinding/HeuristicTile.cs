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
            return path.Count + directionChanges;
        }
    }

    public int totalTileValue
    {
        get
        {
            return (path.Count + distToGoal + (directionChanges + 1) * (directionChanges + 1));
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
        distToGoal = ((pathGoal.x - path[path.Count - 1].x) * (pathGoal.x - path[path.Count - 1].x)) + ((pathGoal.y - path[path.Count - 1].y) * (pathGoal.y - path[path.Count - 1].y));
    }
}
