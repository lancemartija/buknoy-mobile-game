using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelUnLockerch3 : MonoBehaviour
{
    private int Pages = 0;

    [SerializeField]
    private TextMeshProUGUI CollectibleText;

    [SerializeField]
    private AudioSource collectionSoundFX;
    private UIManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Page"))
        {
            collectionSoundFX.Play();
            Destroy(collision.gameObject);
            Pages++;
            CollectibleText.text = Pages + " / 5 Pages";
        }
    }

    void Update()
    {
        if (Pages == 5)
        {
            PlayerPrefs.SetInt("Chapter3", 1);
            uiManager.Finish();
        }
    }
}
