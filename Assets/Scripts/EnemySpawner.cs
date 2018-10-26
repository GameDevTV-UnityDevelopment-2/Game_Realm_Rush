using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnDelay;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject enemies;

    [SerializeField]
    private Text score;

    [SerializeField]
    private AudioClip spawnSFX;

    private bool spawn;
    private int spawned;

    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        UpdateScore();

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity, enemies.transform);

            audioSource.PlayOneShot(spawnSFX);

            spawned++;
            UpdateScore();

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void UpdateScore()
    {
        score.text = spawned.ToString();
    }
}