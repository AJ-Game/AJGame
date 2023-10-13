using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public BoxCollider2D swordCollider;
    public float damage = 1;
    Vector2 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 currentPosition = transform.position;

        // default to attack down 
        // transform.localPosition = new Vector2(currentPosition.x + 0f, currentPosition.y + -0.2f);
        // swordCollider.size = new Vector2(0.25f, 0.1f);
    }

    public void AttackUp()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(currentPosition.x + 0f, currentPosition.y + -0.05f);
        swordCollider.size = new Vector2(0.25f, 0.15f);
    }
    public void AttackDown()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(currentPosition.x + 0f, currentPosition.y + -0.2f);
        swordCollider.size = new Vector2(0.25f, 0.1f);
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(currentPosition.x + -0.1f, currentPosition.y + -0.1f);
        swordCollider.size = new Vector2(0.2f, 0.25f);
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(0.1f, -0.1f);
        swordCollider.size = new Vector2(0.2f, 0.25f);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            // if (enemy != null)
            // {
            //     enemy.Health -= damage;
            // }
            other.SendMessage("OnHit", damage);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
