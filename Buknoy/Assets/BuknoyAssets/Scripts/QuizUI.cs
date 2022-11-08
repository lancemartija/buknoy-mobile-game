using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizUI : MonoBehaviour
{
    [SerializeField] public QuizManager quizmanager;
    [SerializeField] public QuizResultsManager quizresults;

    
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
        quizmanager.mainmenuPanel.SetActive(false);
        quizmanager.quizPanel.SetActive(true);
    }

    public void Button2Click()
    {
        quizmanager.StartGame(2);
        quizmanager.mainmenuPanel.SetActive(false);
        quizmanager.quizPanel.SetActive(true);
    }

    public void Button3Click()
    {
        quizmanager.StartGame(3);
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
        SceneManager.LoadScene("MenuHub");
    }
    //Quiz Game Buttons
    public void PauseButton()
    {

       quizmanager.gamestatus = GameStatus.Menu;
       quizmanager.PausePanel.SetActive(true);
    }
    public void ConfirmExitButton()
    {
        quizmanager.ConfirmExitPanel.SetActive(true);
    }
    public void ResumeButton()
    {

        quizmanager.gamestatus = GameStatus.Playing;
        quizmanager.PausePanel.SetActive(false);
        quizmanager.ConfirmExitPanel.SetActive(false);
    }

    public void RetryButton ()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
