using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;
    public ScanObjekt scanObjekt; // Referenz auf das ScanObjekt-Skript
    public activateQuiz activateQuiz; // Referenz auf das activateQuiz-Skript

    private bool hasScanned = false;
    private Vector2 movementInput;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 lastMovementInput;

    private bool canMove = true;

void Start()
{
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();

    if (scanObjekt == null)
    {
        Debug.LogError("ScanObjekt reference is missing in PlayerController.");
    }
    if (activateQuiz == null)
    {
        Debug.LogError("activateQuiz reference is missing in PlayerController.");
    }
}

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (movementInput != Vector2.zero)
            {
                lastMovementInput = movementInput;
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
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                    direction,
                    movementFilter,
                    new RaycastHit2D[1],
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
        hasScanned = true;
        if (scanObjekt != null)
        {
            scanObjekt.SetHasServer(true); // Update ScanObjekt state
        }
        else
        {
            Debug.LogError("ScanObjekt reference is missing.");
        }

        Destroy(server); // Remove server object from scene
        AchievementManager.Instance.SetHasServer(true); // Signal AchievementManager

        // Activate the pickup achievement object
        AchievementManager.Instance.ActivateObject("pickup_achievement", true);
    }
}
