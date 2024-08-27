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

    private bool isRegenerating = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        //Verlust von Leben testen, z.B. durch einen Tastendruck
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    TakeDamage(1);
        //}
    }

    public void TakeDamage(int amount)
    {
        SoundFXManager.instance.PlaySoundFXClip(damageSoundClip, transform, 1f);
        animator.SetBool("damage", true);
        if (currentHealth > 0)
        {
            currentHealth -= amount;
            Debug.Log("Health: " + currentHealth);

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
        animator.SetBool("damage", false);
        animator.SetBool("Defeated", true);
        playerController.LockMovement();

    }

    private void Respawn()
    {
        animator.SetBool("Defeated", false);
        playerController.UnlockMovement();
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

        if (currentHealth > maxHealth)
        {
            currentHealth = 3;
        }
    }
}
