using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int squares = 0;
    [SerializeField] private Text CollectibleText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("square"))
        {
            Destroy(collision.gameObject);
            squares++;
            CollectibleText.text = "Pages: " + squares;
        }
    }

    
}