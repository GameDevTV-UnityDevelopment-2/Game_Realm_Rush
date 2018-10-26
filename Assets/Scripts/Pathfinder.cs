using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField]
    private Waypoint startWaypoint;

    [SerializeField]
    private Waypoint endWaypoint;

    private Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    private Queue<Waypoint> queue = new Queue<Waypoint>();
    private bool isRunning = true;
    private Waypoint currentWaypoint;
    private List<Waypoint> path = new List<Waypoint>();         
    private Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };


    public Waypoint Goal
    {
        get { return endWaypoint; }
    }


    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            CalculatePath();
        }

        return path;
    }

    private void CalculatePath()
    {
        LoadBlocks();
        BreadthFirstSearch();
        CreatePath();
    }

    private void CreatePath()
    {
        SetAsPath(endWaypoint);

        Waypoint previous = endWaypoint.exploredFrom;

        while(previous != startWaypoint)
        {
            SetAsPath(previous);

            previous = previous.exploredFrom;
        }

        SetAsPath(startWaypoint);

        path.Reverse();
    }

    private void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
    }

    private void HaltIfEndFound()
    {
        if (currentWaypoint == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void LoadBlocks()
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();

        foreach (Waypoint waypoint in waypoints)
        {
            if (grid.ContainsKey(waypoint.GridPosition))
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
            else
            {
                grid.Add(waypoint.GridPosition, waypoint);
            }
        }
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRunning)
        {
            currentWaypoint = queue.Dequeue();
            currentWaypoint.isExplored = true;

            HaltIfEndFound();
            ExploreNeighbours();
        }
    }

    private void ExploreNeighbours()
    {
        if (isRunning)
        {
            foreach (Vector2Int direction in directions)
            {
                Vector2Int neighbourCoordinates = currentWaypoint.GridPosition + direction;

                if (grid.ContainsKey(neighbourCoordinates))
                {
                    QueueNeighbour(neighbourCoordinates);
                }
            }
        }
    }

    private void QueueNeighbour(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];

        if (!neighbour.isExplored && !queue.Contains(neighbour))
        {
            neighbour.exploredFrom = currentWaypoint;
            queue.Enqueue(neighbour);
        }
    }
}