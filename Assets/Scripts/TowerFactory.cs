using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject towerContainer;

    [SerializeField]
    private Tower towerPrefab;

    [SerializeField]
    private int maxTowers = 5;

    private Queue<Tower> towers = new Queue<Tower>();


    public void BuildTower(Waypoint waypoint)
    {
        if(towers.Count < maxTowers)
        {
            CreateTower(waypoint);
        }
        else
        {
            MoveTower(waypoint);
        }
    }

    private void CreateTower(Waypoint waypoint)
    {
        waypoint.isPlaceable = false;

        Tower tower = Instantiate(towerPrefab, waypoint.transform.position, Quaternion.identity, towerContainer.transform);
        tower.waypoint = waypoint;

        towers.Enqueue(tower);
    }

    private void MoveTower(Waypoint waypoint)
    {
        Tower tower = towers.Dequeue();
        tower.waypoint.isPlaceable = true;

        waypoint.isPlaceable = false;

        tower.waypoint = waypoint;
        tower.transform.position = waypoint.transform.position;

        towers.Enqueue(tower);
    }
}