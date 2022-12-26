using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUnLockerch3 : MonoBehaviour
{
    private int Pages = 0;
    [SerializeField] private Text CollectibleText;

    public int nextSceneLoad;

    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Page"))
        {
            Destroy(collision.gameObject);
            Pages++;
            CollectibleText.text = Pages + " / 5 Pages";
        }
    }

    void Update()
    {
        if(Pages == 5)
        {
            PlayerPrefs.SetInt("Chapter3", 1);

            GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().StopMusicNow();
            SceneManager.LoadScene(nextSceneLoad);
        }
    }

    
}