using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHub : MonoBehaviour
{
    
    public void GoToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void GoToQuiz()
    {
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().StopMusic();
        Invoke("LoadQuiz", 2f);
    }
    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    public void GoToViewInfo()
    {
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().StopMusic();
        Invoke("LoadNotes", 2f);
    }
    public void GotoSettings()
    {
        Invoke("LoadSettings", 2f);
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
