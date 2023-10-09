using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;


    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start(){
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
        
    }

    private void FixedUpdate(){
        // If movement input is not 0, try to move
        if(movementInput != Vector2.zero){
            bool success = TryMove(movementInput);
            animator.SetFloat("moveX", movementInput.x);
            animator.SetFloat("moveY", movementInput.y);

            if (!success){
                success = TryMove(new Vector2(movementInput.x, 0));
            }
            if (!success){
                success = TryMove(new Vector2(0, movementInput.y));
            }

            animator.Play("player_walk");
        } 
        else{
            animator.Play("player_idle");
        }
        
        // flip right animations to be left animations when going left
        if(movementInput.x < 0){
            spriteRenderer.flipX = true;
        } else if(movementInput.x > 0){
            spriteRenderer.flipX = false;
        } 

    }

    private bool TryMove(Vector2 direction){
        if(direction != Vector2.zero){
        // Check for potential collisions
            int count = rigidbody.Cast(
                direction, // X and Y values between -1 and 1 that repreent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on. Such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset
            
            if (count == 0){
                rigidbody.MovePosition(rigidbody.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else{
                return false;
            }
        }
        // can't move if theres no direction to move in
        return false;
    }
    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire(){
        animator.SetTrigger("attackTrigger");
        print("fire pressed");
    }

}
