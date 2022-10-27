using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    [SerializeField] public QuizManager quizmanager;

    

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
}
