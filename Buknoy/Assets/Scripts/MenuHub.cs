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
    }
    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
