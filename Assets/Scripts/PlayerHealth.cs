using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 10;

    [SerializeField]
    private Text healthText;

    [SerializeField]
    private AudioClip hitSFX;

    private AudioSource audioSource;


    public void Hit()
    {
        health--;

        audioSource.PlayOneShot(hitSFX);

        UpdateHealth();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        UpdateHealth();
    }

    private void UpdateHealth()
    {
        healthText.text = health.ToString();
    }
}