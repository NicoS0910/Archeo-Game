using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    private Transform target;
    public float speed;

    public float Health { get; set; } = 1;

    private Rigidbody2D rb; // Rigidbody component for the enemy

    private void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        if (target != null)
        {
            // Move only if not in collision with player
            if (!IsCollidingWithPlayer())
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }
    }

    private bool IsCollidingWithPlayer()
    {
        // Check if there's a collision between the enemy and the player
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f); // Adjust radius as needed
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                return true; // Collision detected with player
            }
        }
        return false; // No collision with player
    }

    public void Defeated()
    {
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }
}
