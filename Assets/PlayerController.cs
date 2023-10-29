using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// movement speed
    /// </summary>
    float moveSpeed = 1f;

    public int Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Death();
            }
        }
        get
        {
            return health;
        }
    }

    public int health = 3;


    /// <summary>
    /// "Safety" distance to give spacer in calculate collision
    /// </summary>
    public float collisionOffset = 0.02f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;

    float attackCooldownDuration = 1;
    float attackCooldown = 0;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    string direction = "down";
    bool canMove = true;
    bool isAlive = true;
    int damageIndicatorDuration = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Updates ~50 fps. Used instead of Update to better handle physics.
    /// </summary>
    private void FixedUpdate()
    {
        if (canMove)
        {
            // If movement input is not 0, try to move
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);
                animator.SetFloat("moveX", movementInput.x);
                animator.SetFloat("moveY", movementInput.y);

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

            // flip right animations to be left animations when going left
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }

        if (attackCooldown > 0)
        {
            attackCooldown -= .02f;
        }

        // set player sprite color back to default after a fixed amount of time in case we took damage 
        if (damageIndicatorDuration == 0)
        {
            spriteRenderer.color = Color.white;
        }
        if (damageIndicatorDuration > 0)
        {
            damageIndicatorDuration--;
        }

    }

    /// <summary>
    /// Helper method to check if player will collide with anything
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // Check for potential collisions
            int count = rigidbody.Cast(
                direction, // X and Y values between -1 and 1 that repreent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on. Such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if (count == 0)
            {
                rigidbody.MovePosition(rigidbody.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        // can't move if theres no direction to move in
        return false;
    }

    /// <summary>
    /// Handles logic for movement
    /// </summary>
    /// <param name="movementValue"></param>
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
        direction = FindPlayerDirection(movementValue.Get<Vector2>());
    }

    /// <summary>
    /// Handles logic for main attack
    /// </summary>
    void OnFire()
    {
        if (attackCooldown <= 0)
        {
            string direction = FindPlayerDirection(movementInput);
            // will call the attack action on the direction the player was last facing.
            switch (direction)
            {
                case "up":
                    animator.SetTrigger("attackTriggerUp");
                    swordAttack.AttackUp();
                    break;
                case "down":
                    animator.SetTrigger("attackTriggerDown");
                    swordAttack.AttackDown();
                    break;
                case "left":
                    animator.SetTrigger("attackTrigger");
                    swordAttack.AttackLeft();
                    break;
                case "right":
                    animator.SetTrigger("attackTrigger");
                    swordAttack.AttackRight();
                    break;
            }
            attackCooldown = attackCooldownDuration;
        }
    }

    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    /// <summary>
    /// Used to find the direction the player is facing
    /// </summary>
    /// <param name="movementInput"></param>
    /// <returns></returns>
    private string FindPlayerDirection(Vector2 movementInput)
    {
        if (movementInput.x < 0)
        {
            return "left";
        }
        else if (movementInput.x > 0)
        {
            return "right";
        }

        if (movementInput.y < 0)
        {
            return "down";
        }
        else if (movementInput.y > 0)
        {
            return "up";
        }

        return direction;
    }

    private void OnHit(int damage)
    {
        if (isAlive)
        {
            Health -= damage;
            // highlights the player red when taking damage for damageIndicatorDuration amount of updates.
            spriteRenderer.color = Color.red;
            damageIndicatorDuration = 5;
        }

    }

    private void Death()
    {
        isAlive = false;
        animator.SetBool("isDead", true);
        canMove = false;
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
