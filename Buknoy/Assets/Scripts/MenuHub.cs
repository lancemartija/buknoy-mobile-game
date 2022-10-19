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
        SceneManager.LoadScene("Quizzes");
        Time.timeScale = 1;
    }
    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
