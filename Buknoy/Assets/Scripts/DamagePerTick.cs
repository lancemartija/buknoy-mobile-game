using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePerTick : MonoBehaviour
{
   [SerializeField] private float damage;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
