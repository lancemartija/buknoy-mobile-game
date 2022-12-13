using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set;}
    private Animator anim;
    private bool dead;

    [Header("iframes")]
    [SerializeField] private float iframesDuration;
    [SerializeField] private int numberofFlashes;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("death");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }
    }

    public IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberofFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iframesDuration / (numberofFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iframesDuration / (numberofFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
    
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
