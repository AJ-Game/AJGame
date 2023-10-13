using UnityEngine;
using System.Collections;

public class EnemyTest : MovingObject
{
    public int damage;

    private Animator animator;
    private Transform target;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }
    void Update()
    {

    }
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        //move the enemy
        base.AttemptMove<T>(xDir, yDir);
    }
    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;
        //is th enemy and player in the same column?
        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
        {
            //move up or down depending on player position
            yDir = target.position.y > transform.position.y ? 1 : -1;
        }
        else
        {
            //move left or right depending on player position
            yDir = target.position.x > transform.position.x ? 1 : -1;
        }
        //AttemptMove<Player>(xDir, yDir);
    }
    protected override void OnCantMove<T>(T component)
    {
        //Player hitPlayer = component as Player;

        //this is where we make the player take damage
    }
}