using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float collisionOffset = 0.05f;
    [SerializeField] private ContactFilter2D movementFilter;
    public SwordAttack swordAttack;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;

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
                bool success = TryMove(movementInput);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));

                    if (!success)
                    {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }
                if (movementInput.x != 0)
                {
                    animator.SetBool("isMovingLeftOrRight", success);
                    if (movementInput.x < 0)
                    {
                        spriteRenderer.flipX = true;
                    }
                    else if (movementInput.x > 0)
                    {
                        spriteRenderer.flipX = false;
                    }
                }
                if (movementInput.y < 0)
                {
                    animator.SetBool("isMovingDown", success);
                }
                else if (movementInput.y > 0)
                {
                    animator.SetBool("isMovingUp", success);
                }
            }
            else
            {
                animator.SetBool("isMovingLeftOrRight", false);
                animator.SetBool("isMovingUp", false);
                animator.SetBool("isMovingDown", false);
            }
        }
    }


    private bool TryMove(Vector2 direction)
    {
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

    private void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    private void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack() 
    {
        LockMovement();
        if (spriteRenderer.flipX == true) 
        {
            swordAttack.AttackLeft();
        }
        else 
        { 
            swordAttack.AttackRight();
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
}