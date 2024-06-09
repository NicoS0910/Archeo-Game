using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private Transform target;
    public float speed;
    private float health = 1; // Privates Feld für die Gesundheit
    private Rigidbody2D rb;
    private bool isChasing = false; // Gibt an, ob die Verfolgung aktiv ist

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
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (target != null && isChasing)
        {
            // Move only if not in collision with player
            if (!IsCollidingWithPlayer())
            {
                Vector2 direction = (target.position - transform.position).normalized;
                rb.MovePosition((Vector2)transform.position + direction * speed * Time.fixedDeltaTime);
            }
        }
    }

    private bool IsCollidingWithPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
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
}
