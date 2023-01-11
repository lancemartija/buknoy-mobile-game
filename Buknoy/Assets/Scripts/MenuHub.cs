using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHub : MonoBehaviour
{
    // void Awake()
    // {
    //     GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().BacktoMenuHubBGM();
    // }

    public void GoToMenu()
    {
        // GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().StopMusic();
        SceneManager.LoadScene("StartMenu");
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().BacktoMainMenuBGM();
    }

    public void GoToQuiz()
    {
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().StopMusic();
        Invoke("LoadQuiz", 0f);
    }

    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void GoToViewInfo()
    {
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().StopMusic();
        Invoke("LoadNotes", 0f);
    }

    public void GotoSettings()
    {
        Invoke("LoadSettings", 0f);
    }

    void LoadQuiz()
    {
        SceneManager.LoadSceneAsync("Quizzes");
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().MainMenutoQuizMenuBGM();
        Time.timeScale = 1;
    }

    void LoadNotes()
    {
        SceneManager.LoadSceneAsync("Notes");
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().MainMenutoNoteMenuBGM();
        Time.timeScale = 1;
    }

    void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
    }
}
