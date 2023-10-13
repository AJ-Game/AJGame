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
    }

    void OnHit(float damage)
    {
        Health -= damage;
        animator.SetTrigger("hit");
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
        PlayerController player = other.collider.GetComponent<PlayerController>();
        if (player != null){
            player.SendMessage("OnHit", damage);

        }

        // if (other.tag == "Player")
        // {
        // }
    }

}
