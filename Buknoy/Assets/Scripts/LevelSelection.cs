using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private bool unlocked;
    public Image unlockImage;
    public GameObject[] pages;
    
    private void Update()
    {
        UpdateLevelImage();
    }

    private void UpdateLevelImage()
    {
        if(!unlocked)
        {
            unlockImage.gameObject.SetActive(true);
            for(int i = 0; i < pages.Length; i++)
            {
                pages[i].gameObject.SetActive(false);
            }
        }
        else
        {
            unlockImage.gameObject.SetActive(false);
            for(int i = 0; i < pages.Length; i++)
            {
                pages[i].gameObject.SetActive(true);
            }
        }
    }

    public void PressSelection(string _LevelName)
    {
        if(unlocked)
        {
            SceneManager.LoadScene(_LevelName);
        }
    }
}
