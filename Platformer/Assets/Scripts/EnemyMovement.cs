using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(speed, 0); 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        speed = -speed;
        FlipEnemySprite();
    }

    void FlipEnemySprite()
    {
        transform.localScale = new Vector2(Mathf.Sign(speed), 1f);
    }
}
