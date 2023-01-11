using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private GameObject FinishScreen;

    public void Awake()
    {
        gameOverScreen.SetActive(false);
        FinishScreen.SetActive(false);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void Finish()
    {
        FinishScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().BacktoMenuHubBGM();
        SceneManager.LoadScene("MenuHub");
    }

    public void TakeQuiz()
    {
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().MainMenutoQuizMenuBGM();
        SceneManager.LoadScene("Quizzes");
    }
}
