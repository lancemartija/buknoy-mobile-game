using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string LevelText;
        public int UnLocked;
        public bool IsInteractable;

    }
    public GameObject levelButton;
    public Transform Content;
    public List<Level> LevelList;


    void Start()
    {
        FillList();
    }
    void FillList()
    {
        foreach(var level in LevelList)
        {
            GameObject newbutton = Instantiate(levelButton) as GameObject;
            LevelButton button = newbutton.GetComponent<LevelButton>();
            button.LevelText.text = level.LevelText;

            if(PlayerPrefs.GetInt("Chapter" + button.LevelText.text) == 1)
            {
                level.UnLocked = 1;
                level.IsInteractable = true;
            }
            button.unlocked = level.UnLocked;
            button.GetComponent<Button>().interactable = level.IsInteractable;
            button.GetComponent<Button>().onClick.AddListener(() => loadLevels("Chapter" + button.LevelText.text));

            newbutton.transform.SetParent(Content);
        }
        SaveAll();
    }
    void SaveAll()
    {
        if(PlayerPrefs.HasKey("Chapter0"))
        {
            return;
        }
        else
        {
            GameObject[] allButtons = GameObject.FindGameObjectsWithTag("LevelButton");
            foreach(GameObject buttons in allButtons)
            {
                LevelButton button = buttons.GetComponent<LevelButton>();
                PlayerPrefs.SetInt("Chapter" + button.LevelText.text, button.unlocked);
            }
        }
    }
    void loadLevels(string value)
    {
        SceneManager.LoadScene(value);
    }
}
