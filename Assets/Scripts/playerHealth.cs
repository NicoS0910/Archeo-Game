using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    private Animator animator;
    public PlayerController playerController;
    private Rigidbody2D rb;
    public float regenerationDelay = 5f;
    public Vector3 respawnPosition;
    [SerializeField] private AudioClip damageSoundClip;
    [SerializeField] private AudioClip deathSoundClip;

    // // Neue Variablen für den Farbeffekt
    // [SerializeField] private Color damageColor = Color.red;
    // private Color originalColor;
    // private Renderer playerRenderer;
    private bool isRegenerating = false;
    public Inventory inventory;
    public Resource scorePoints;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        // playerRenderer = GetComponent<Renderer>();

        // if (playerRenderer != null)
        // {
        //     originalColor = playerRenderer.material.color;
        // }
    }

    void Update()
    {
    }

    public void TakeDamage(int amount)
    {
        SoundFXManager.instance.PlaySoundFXClip(damageSoundClip, transform, 1f);
        animator.SetBool("damage", true);
        if (currentHealth > 0)
        {
            currentHealth -= amount;
            Debug.Log("Health: " + currentHealth);

            // // Wende den Rot-Effekt an
            // if (playerRenderer != null)
            // {
            //     StartCoroutine(FlashRed());
            // }

            if (currentHealth <= 0)
            {
                Die();
            }
            else if (!isRegenerating)
            {
                StartCoroutine(RegenerateHealth());
            }
        }
    }

    public void stopDamageAnimation()
    {
        animator.SetBool("damage", false);
    }

    private void Die()
    {
        SoundFXManager.instance.PlaySoundFXClip(deathSoundClip, transform, 1f);
        animator.SetBool("damage", false);
        animator.SetBool("Defeated", true);
        playerController.LockMovement();
        inventory.AddResources(scorePoints, -20);
    }

    // private IEnumerator FlashRed()
    // {
    //     if (playerRenderer != null)
    //     {
    //         playerRenderer.material.color = damageColor; // Ändere die Farbe auf Rot
    //         yield return new WaitForSeconds(0.1f); // Dauer des Rot-Effekts
    //         playerRenderer.material.color = originalColor; // Setze die Originalfarbe zurück
    //     }
    // }

    private void Respawn()
    {
        animator.SetBool("Defeated", false);
        playerController.UnlockMovement();
        currentHealth = maxHealth;
        transform.position = respawnPosition;
        Debug.Log("Player respawned at " + respawnPosition);
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

        if (currentHealth > maxHealth)
        {
            currentHealth = 3;
        }
    }
}
