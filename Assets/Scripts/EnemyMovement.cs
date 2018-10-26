using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;

    private Pathfinder pathfinder;


    private void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();

        List<Waypoint> path = pathfinder.GetPath();

        StartCoroutine(Follow(path));
    }

    private IEnumerator Follow(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;

            if (waypoint == pathfinder.Goal)
            {
                FindObjectOfType<PlayerHealth>().Hit();     // TODO: Remove need to FindObject
                GetComponent<EnemyDamage>().SelfDestruct();
            }

            yield return new WaitForSeconds(speed);
        }
    }
}