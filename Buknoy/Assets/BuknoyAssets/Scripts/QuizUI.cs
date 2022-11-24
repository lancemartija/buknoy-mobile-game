using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizUI : MonoBehaviour
{
    [SerializeField] public QuizManager quizmanager;
    [SerializeField] public QuizResultsManager quizresults;
    

    [SerializeField] public GameObject confirmexitPanel, confirmdeletePanel, pausePanel;

    public GameObject PausePanel {get {return pausePanel;}}
    public GameObject ConfirmExitPanel {get {return confirmexitPanel;}}
    public GameObject ConfirmDeletePanel {get {return confirmdeletePanel;}}

    
    //Quiz Hub Buttons
    public void Button0Click()
    {
        quizmanager.StartGame(0);
        quizmanager.mainmenuPanel.SetActive(false);
        quizmanager.quizPanel.SetActive(true);
    }
    
    public void Button1Click()
    {
        quizmanager.StartGame(1);
        quizmanager.Load();
        quizmanager.mainmenuPanel.SetActive(false);
        quizmanager.quizPanel.SetActive(true);
    }

    public void Button2Click()
    {
        quizmanager.StartGame(2);
        quizmanager.Load();
        quizmanager.mainmenuPanel.SetActive(false);
        quizmanager.quizPanel.SetActive(true);
    }

    public void Button3Click()
    {
        quizmanager.StartGame(3);
        quizmanager.Load();
        quizmanager.mainmenuPanel.SetActive(false);
        quizmanager.quizPanel.SetActive(true);
    }
    public void ButtonResultsClick() //View Results
    {
        quizmanager.quizResultsPanel.SetActive(true);
        quizmanager.ViewResults();
    }
    public void BacktoQuizHub()
    {
        quizmanager.quizResultsPanel.SetActive(false);
    }
    public void BacktoMenuHub()
    {
        Invoke("LoadMenuHub", 2f);
    }
    //Load other Scenes after 2 seconds
    void LoadMenuHub()
    {
        SceneManager.LoadScene("MenuHub");
    }
    void LoadQuizScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //Quiz Game Buttons
    public void PauseButton()
    {

       quizmanager.gamestatus = GameStatus.Menu;
       PausePanel.SetActive(true);
    }
    public void ConfirmExitButton()
    {
        ConfirmExitPanel.SetActive(true);
    }
    public void ResumeButton()
    {

        quizmanager.gamestatus = GameStatus.Playing;
        PausePanel.SetActive(false);
        ConfirmExitPanel.SetActive(false);
    }

    public void RetryButton ()
    {
        Invoke("LoadQuizScene", 2f);
    }
    public void ConfirmDeleteButton()
    {
        ConfirmDeletePanel.SetActive(true);
    }
    public void Resume2Button()
    {
        ConfirmDeletePanel.SetActive(false);
    }
    public void DeleteHighScoreDataButton()
    {
        ConfirmDeletePanel.SetActive(false);
        quizresults.DeleteHighScoreData();
        quizmanager.results.Clear();
        for (int i = 0; i < 4; i++)
        {
            quizmanager.QuizScore[i].text = "HIGH SCORE:\n 0";
            quizmanager.QuizStreak[i].text = "HIGHEST STREAK:\n 0";
        }
        Debug.Log("File deleted!");
    }
}
