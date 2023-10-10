using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    Vector2 currentPosition;
    BoxCollider2D swordCollider;

    // Start is called before the first frame update
    void Start()
    {
        swordCollider = GetComponent<BoxCollider2D>();
        Vector2 currentPosition = transform.position;

        // default to attack down 
        transform.position = new Vector2(currentPosition.x + 0f, currentPosition.y + -0.2f);
        swordCollider.size = new Vector2(0.25f, 0.1f);
    }

    public void AttackUp(){
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(currentPosition.x + 0f, currentPosition.y + -0.05f);
        swordCollider.size = new Vector2(0.25f, 0.15f);
        print("up");
    }
    public void AttackDown(){
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(currentPosition.x + 0f, currentPosition.y + -0.2f);
        swordCollider.size = new Vector2(0.25f, 0.1f);
        print("down");        
    }

    public void AttackLeft(){
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(currentPosition.x + -0.1f, currentPosition.y + -0.1f);
        swordCollider.size = new Vector2(0.2f, 0.25f);
        print("left");
    }

    public void AttackRight(){
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(0.1f, -0.1f);
        swordCollider.size = new Vector2(0.2f, 0.25f);
        print("right");
    }

    public void StopAttack(){
        swordCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
