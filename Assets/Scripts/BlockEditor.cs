using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class BlockEditor : MonoBehaviour
{
    private Waypoint waypoint;


    private void Awake()
    {
        waypoint = gameObject.GetComponent<Waypoint>();
    }

    private void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GridSize;

        transform.position = new Vector3
            (
                waypoint.GridPosition.x * gridSize,
                0f,
                waypoint.GridPosition.y * gridSize
            );
    }

    private void UpdateLabel()
    {
        int gridSize = waypoint.GridSize;
        string gridPosition = waypoint.GridPosition.x + "," + waypoint.GridPosition.y;

        TextMesh textMesh = gameObject.GetComponentInChildren<TextMesh>();

        textMesh.text = gridPosition;

        gameObject.name = gridPosition;
    }
}