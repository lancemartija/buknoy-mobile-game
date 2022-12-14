using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f;

    [SerializeField]
    private float damage;

    Rigidbody2D enmyRigidbody;

    void Start()
    {
        enmyRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsFacingLeft())
        {
            enmyRigidbody.velocity = new Vector2(-moveSpeed, 0f);
        }
        else
        {
            enmyRigidbody.velocity = new Vector2(moveSpeed, 0f);
        }
    }

    private bool IsFacingLeft()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") { }
        else
        {
            transform.localScale = new Vector2(
                (Mathf.Sign(enmyRigidbody.velocity.x)),
                transform.localScale.y
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
