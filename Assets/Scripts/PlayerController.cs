using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;
    public ScanObjekt scanObjekt; // Referenz auf das ScanObjekt-Skript hinzugefügt

    private bool hasServer = false; // Gibt an, ob der Spieler den Server hat
    private Vector2 movementInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private bool canMove = true;
    private Vector2 lastMovementInput; // New variable to store the last movement direction

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (movementInput != Vector2.zero)
            {
                lastMovementInput = movementInput; // Update the last movement direction
                bool success = TryMove(movementInput);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }
                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                animator.SetBool("isMoving", success);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }

        // Taste "E" wird reserviert für zukünftige Aktionen, wenn der Server aufgesammelt wurde
        if (Input.GetKeyDown(KeyCode.E) && hasServer)
        {
            Debug.Log("E Taste gedrückt und Server ist aufgesammelt.");
            // Füge hier zukünftige Aktionen hinzu
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // Check for collisions
            int count = rb.Cast(
                    direction,
                    movementFilter,
                    castCollisions,
                    moveSpeed * Time.fixedDeltaTime + collisionOffset);
            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            // Can't move if there is no direction to move in
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
        if (movementInput != Vector2.zero)
        {
            animator.SetFloat("XInput", movementInput.x);
            animator.SetFloat("YInput", movementInput.y);
        }
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack()
    {
        LockMovement();
        if (lastMovementInput.x > 0)
        {
            swordAttack.AttackRight();
        }
        else if (lastMovementInput.x < 0)
        {
            swordAttack.AttackLeft();
        }
        else if (lastMovementInput.y > 0)
        {
            swordAttack.AttackUp();
        }
        else if (lastMovementInput.y < 0)
        {
            swordAttack.AttackDown();
        }
    }

    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Server"))
        {
            PickUpServer(other.gameObject);
        }
    }

    private void PickUpServer(GameObject server)
    {
        hasServer = true;
        scanObjekt.SetHasServer(true); // Aktualisiere den Status des Servers im ScanObjekt
        Destroy(server); // Server-Objekt aus der Szene entfernen
        Debug.Log("Server aufgenommen!");
    }
}
