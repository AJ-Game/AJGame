using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public DetectionZone detectionZone;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.02f;
    public float speed = 200f;
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
    bool canMove = true;
    bool isAlive = true;
    bool canAttack = false;
    PlayerController player;
    float attackCooldownDuration = 2;
    float attackCooldownTimer = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", true);
        rigidbody = GetComponent<Rigidbody2D>();
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
        if (canAttack)
        {
            Attack();
        }
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= .02f;
        }
    }

    void OnHit(float damage)
    {
        Health -= damage;
        animator.SetTrigger("hit");
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            canAttack = true;
            player = other.collider.GetComponent<PlayerController>();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        canAttack = false;
    }

}
