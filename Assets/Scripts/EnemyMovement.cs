using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D enemy;
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(isFacingRight())
            MoveRight();
        else    
            MoveLeft();
    }

    void MoveRight()
    { 
        enemy.velocity = new Vector2(moveSpeed, enemy.velocity.y);
    }

    void MoveLeft()
    {
        enemy.velocity = new Vector2(-moveSpeed, enemy.velocity.y);
    }

    bool isFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    { 
        transform.localScale = new Vector2(-(Mathf.Sign(enemy.velocity.x)), 1.0f);    
    }
}
