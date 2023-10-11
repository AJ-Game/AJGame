using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;

    Rigidbody2D rigidbody;

    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public float collisionOffset = 0.02f;
    public float speed = 1f;

    public Vector2 direction = new Vector2(1, 0);

    bool canMove;
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


    public float health = 1;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        bool canMove = TryMove(direction);
        animator.SetBool("isMoving", canMove);
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // Check for potential collisions
            int count = rigidbody.Cast(
                direction, // X and Y values between -1 and 1 that repreent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on. Such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                speed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if (count == 0)
            {
                rigidbody.MovePosition(rigidbody.position + direction * speed * Time.fixedDeltaTime);
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
    public void Defeated()
    {
        animator.SetTrigger("Defeated");
        print("Defeated");

    }
    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }
}
