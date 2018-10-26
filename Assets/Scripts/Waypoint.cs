using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool isExplored = false;
    public Waypoint exploredFrom;
    public bool isPlaceable = true; // TODO: Consider renaming

    private const int gridSize = 10;
    private Vector2Int gridPosition;
    

    public int GridSize
    {
        get { return gridSize; }
    }

    public Vector2Int GridPosition
    {
        get { return GetGridPosition(); }
    }


    private Vector2Int GetGridPosition()
    {
        return new Vector2Int
            (
                Mathf.RoundToInt(transform.position.x / gridSize),
                Mathf.RoundToInt(transform.position.z / gridSize)
            );
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlaceable)
            {
                GameObject.FindObjectOfType<TowerFactory>().BuildTower(this);
            }
            else
            {
                // TODO: Provide alternative affordance to play (cursor change etc)
                Debug.Log("Can't build here");
            }            
        }
    }
}