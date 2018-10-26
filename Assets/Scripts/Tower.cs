using UnityEngine;

public class Tower : MonoBehaviour
{
    public Waypoint waypoint;

    [SerializeField]
    private Transform weapon;

    [SerializeField]
    private float attackRange = 10f;

    [SerializeField]
    private ParticleSystem projectileParticle;

    [SerializeField]
    private AudioClip firingSFX;

    private Transform target;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        SetTarget();

        if (target)
        {
            Aim();
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
        }
    }

    private void SetTarget()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();    // TODO: Remove need to FindObjects

        if(sceneEnemies.Length > 0)
        {
            Transform closestEnemy = sceneEnemies[0].transform;

            foreach (EnemyDamage enemy in sceneEnemies)
            {
                closestEnemy = GetClosest(closestEnemy, enemy.transform);
            }

            target = closestEnemy;
        }
    }

    private Transform GetClosest(Transform a, Transform b)
    {
        float distanceA = Vector3.Distance(gameObject.transform.position, a.position);
        float distanceB = Vector3.Distance(gameObject.transform.position, b.position);

        if (distanceA < distanceB)
        {
            return a;
        }
        else
        {
            return b;
        }
    }

    private void Aim()
    {
        weapon.LookAt(target);
    }

    private void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(target.position, gameObject.transform.position);

        if(distanceToEnemy <= attackRange)
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        if (isActive)
        {
            // TODO: Implement firing soundfx - requires sync with particles
            projectileParticle.Play();
        }
        else
        {
            projectileParticle.Stop();
        }
    }
}