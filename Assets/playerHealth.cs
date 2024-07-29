using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public float regenerationDelay = 5f;
    public Vector3 respawnPosition;
    [SerializeField] private AudioClip damageSoundClip;

    private bool isRegenerating = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Hier kannst du den Verlust von Leben testen, z.B. durch einen Tastendruck
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int amount)
    {
        SoundFXManager.instance.PlaySoundFXClip(damageSoundClip, transform, 1f);
        if (currentHealth > 0)
        {
            currentHealth -= amount;
            Debug.Log("Health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Respawn();
            }
            else if (!isRegenerating)
            {
                StartCoroutine(RegenerateHealth());
            }
        }
    }

    private void Respawn()
    {
        // Setze das Leben auf das maximale Leben zurück
        currentHealth = maxHealth;
        // Setze die Position des Spielers auf die Rücksetzposition
        transform.position = respawnPosition; // Passe die Position an
        Debug.Log("Player respawned at " + respawnPosition);
        // Starte die Regeneration nach dem Respawn
        StartCoroutine(RegenerateHealth());
    }

    private IEnumerator RegenerateHealth()
    {
        isRegenerating = true;

        while (currentHealth < maxHealth)
        {
            yield return new WaitForSeconds(regenerationDelay);
            currentHealth++;
            Debug.Log("Health Regenerated: " + currentHealth);
        }

        isRegenerating = false;

        if (currentHealth > maxHealth) {
            currentHealth = 3;
        }
    }
}
