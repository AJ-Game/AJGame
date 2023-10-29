using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public DetectionZone detectionZone;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.02f;
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
    public float health = 3;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator animator;
    Rigidbody2D rigidbody;
    float damage = 1;
    float speed = 50f;
    bool canMove = true;
    bool isAlive = true;
    bool canAttack = false;
    PlayerController player;
    float attackCooldownDuration = 1;
    float attackCooldownTimer = 0;
    bool inCollision = false;
    /// <summary>
    /// Used when enemy is above player to account for player hitbox being at bottom of player sprite.
    /// </summary>
    float lowerRange = .05f;
    /// <summary>
    /// Used when enemy is below player to account for player hitbox being at bottom of player sprite.
    /// </summary>
    float upperRange = .25f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", true);
        rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (detectionZone.detectedObjs.Count > 0)
        {
            animator.SetBool("isMoving", true);
            //calc direction to target
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            //move toward detected obj
            rigidbody.AddForce(direction * speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (InRange())
        {
            Attack();
        }
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= .02f;
        }
    }

    bool InRange()
    {
        print(Vector3.Distance(player.transform.position, transform.position));
        if (transform.position.y > player.transform.position.y)
        {
            if ((Vector3.Distance(player.transform.position, transform.position)) <= lowerRange &&
                Mathf.Abs(player.transform.position.x - transform.position.x) <= .15)
            {
                return true;
            }
        }
        else
        {
            if ((Vector3.Distance(player.transform.position, transform.position)) <= upperRange &&
                Mathf.Abs(player.transform.position.x - transform.position.x) <= .15)
            {
                return true;
            }
        }

        return false;
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        animator.SetTrigger("hit");
        rigidbody.AddForce(knockback);
    }

    void Attack()
    {

        if (attackCooldownTimer <= 0)
        {

            if (player != null)
            {
                player.SendMessage("OnHit", damage);
            }
            attackCooldownTimer = attackCooldownDuration;
        }

    }

    public void Defeated()
    {
        animator.SetBool("isAlive", false);
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }
}
