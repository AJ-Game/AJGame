using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = 0.1f;
    public LayerMask blockingLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2d;

    private float inverseMoveTime;

    //initialization
    //protected virtual methods can be overriden by their inheriting classes
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }
    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;

        //if space that we cast our line into is open then start the movement and return true movement was successful
        if (hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        return false;
    }
    //smooth movement coroutine. Moves unit from one space to the next
    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrtRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrtRemainingDistance > float.Epsilon)
        {
            Vector3 newPos = Vector3.MoveTowards(rb2d.position, end, inverseMoveTime * Time.deltaTime);
            rb2d.MovePosition(newPos);
            sqrtRemainingDistance = (transform.position - end).sqrMagnitude;
            //wait for a frame before reevaluating the loop condition
            yield return null;
        }

    }
    //T is the type of we expect the component is interacting with. Enemies is players. players is walls
    protected virtual void AttemptMove<T>(int xDir, int yDir)
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);
        //if we didnt hit anything then return and continue
        if (hit.transform == null)
        {
            return;
        }
        T hitComponent = hit.transform.GetComponent<T>();
        //if we hit something call the derived class for oncantmove functionality
        if (!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }
    }
    //on cant move ovveridden by inherited classes
    protected abstract void OnCantMove<T>(T component)
        where T : Component;
}