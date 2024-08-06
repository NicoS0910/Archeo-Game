using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    SpriteRenderer spriteRenderer;
    private Transform target;
    private PlayerHealth playerHealth; // Referenz zur PlayerHealth des Spielers
    public float speed;
    [SerializeField] private float health; // Privates Feld für die Gesundheit
    [SerializeField] private AudioClip damageSoundClip; // hinterlegter Sound
    private Rigidbody2D rb;
    private bool isChasing = false; // Gibt an, ob die Verfolgung aktiv ist
    private float lastAttackTime; // Zeitpunkt des letzten Angriffs
    public float attackCooldown = 1f; // Abklingzeit zwischen den Angriffen
    public Collider2D enemyCollider; // Spezifischer Collider des Gegners

    // Öffentliche Eigenschaft für Health
    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        // Initialisiere die Referenz zur PlayerHealth des Spielers
        playerHealth = target.GetComponent<PlayerHealth>();
        lastAttackTime = -attackCooldown; // Initialisieren damit der Gegner sofort angreifen kann
    }

    private void FixedUpdate()
    {
        if (target != null && isChasing)
        {
            animator.SetBool("isMoving", true);
            // Move only if not in collision with player
            if (!IsCollidingWithPlayer())
            {
                Vector2 direction = (target.position - transform.position).normalized;
                rb.MovePosition((Vector2)transform.position + direction * speed * Time.fixedDeltaTime);
            }
            else
            {
                // Angriff, wenn der Gegner in Reichweite ist und die Abklingzeit abgelaufen ist
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    AttackPlayer(1); // Hier fügst du dem Spieler 1 Schaden zu
                    lastAttackTime = Time.time; // Aktualisiere den Zeitpunkt des letzten Angriffs
                }
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private bool IsCollidingWithPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(enemyCollider.bounds.center, enemyCollider.bounds.size, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
            // Optional: Start einer Alarmanimation oder Soundeffekt
            animator.SetTrigger("PlayerDetected");
        }
    }

    public void Defeated()
    {
        // Play soundFX
        SoundFXManager.instance.PlaySoundFXClip(damageSoundClip, transform, 1f);

        animator.SetTrigger("Defeated");
        rb.velocity = Vector2.zero; // Stop movement when defeated
        GetComponent<Collider2D>().enabled = false; // Disable collider
        this.enabled = false; // Disable this script
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider2D>().size);
    }

    // Neue Angriffs-Funktion
    public void AttackPlayer(int damage)
    {
        if (playerHealth != null)
        {
            animator.SetTrigger("attack");
            playerHealth.TakeDamage(damage);
        }
    }
}
