using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioSource checkpointSound;
    [SerializeField] private Text LivesText;
    [SerializeField]public int livesRemaining;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;


    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void LoseLife()
    {
        livesRemaining--;
        LivesText.text = livesRemaining + "x";

        if(livesRemaining == 0)
        {
            uiManager.GameOver();
            return;
        }
    }

    public void Respawn()
    {
        LoseLife();
        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            checkpointSound.Play();
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }

     
}
