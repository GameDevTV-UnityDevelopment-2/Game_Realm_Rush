using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private int hitPoints = 10;

    [SerializeField]
    private ParticleSystem hitParticlePrefab;

    [SerializeField]
    private ParticleSystem deathParticlePrefab;

    [SerializeField]
    private ParticleSystem goalParticlePrefab;

    [SerializeField]
    private AudioClip destroyedSFX;

    
    public void SelfDestruct()
    {
        DisplayParticles(goalParticlePrefab);

        Destroy(gameObject);
    }

    private void OnParticleCollision(GameObject other)
    {
        Hit();

        if(hitPoints < 1)
        {
            KillEnemy();
        }
    }

    private void Hit()
    {
        hitPoints -= 1;
        hitParticlePrefab.Play();
    }

    private void KillEnemy()
    {
        DisplayParticles(deathParticlePrefab);

        AudioSource.PlayClipAtPoint(destroyedSFX, Camera.main.transform.position);

        Destroy(gameObject);
    }

    private void DisplayParticles(ParticleSystem particleSystem)
    {
        ParticleSystem particles = Instantiate(particleSystem, gameObject.transform.position, Quaternion.identity);

        particles.Play();
    }
}