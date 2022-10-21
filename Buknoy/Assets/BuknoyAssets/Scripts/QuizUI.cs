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
        //
        quizmanager.choiceTrue.gameObject.SetActive(true);
        quizmanager.choiceFalse.gameObject.SetActive(true);
        quizmanager.TrueAnswerText.gameObject.SetActive(true);
        quizmanager.FalseAnswerText.gameObject.SetActive(true);
        //
        for (int i = 0; i < quizmanager.choiceMultiple.Count; i++)
        {
             quizmanager.choiceMultiple[i].gameObject.SetActive(false);
        }
        quizmanager.FinalAnswerText.gameObject.SetActive(false);
        quizmanager.RightAnswerText.gameObject.SetActive(false);
    }

    public void Button2Click()
    {
        quizmanager.StartGame(2);
        quizmanager.mainmenuPanel.SetActive(false);
        quizmanager.quizPanel.SetActive(true);
        //
        quizmanager.choiceTrue.gameObject.SetActive(false);
        quizmanager.choiceFalse.gameObject.SetActive(false);
        quizmanager.TrueAnswerText.gameObject.SetActive(false);
        quizmanager.FalseAnswerText.gameObject.SetActive(false);
        //
        for (int i = 0; i < quizmanager.choiceMultiple.Count; i++)
        {
             quizmanager.choiceMultiple[i].gameObject.SetActive(true);
        }
        quizmanager.FinalAnswerText.gameObject.SetActive(true);
        quizmanager.RightAnswerText.gameObject.SetActive(true);
    }
}
