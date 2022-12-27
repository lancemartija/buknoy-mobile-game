using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUnLockerch2 : MonoBehaviour
{
    private int Pages = 0;
    [SerializeField] private Text CollectibleText;
    private UIManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
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
            PlayerPrefs.SetInt("Chapter2", 1);

            uiManager.Finish();
            GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().StopMusicNow();
        }
    }

    
}