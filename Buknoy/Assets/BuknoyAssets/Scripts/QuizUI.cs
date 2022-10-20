using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    [SerializeField] public QuizManager quizmanager;

    

    public void Button1Click()
    {
        quizmanager.StartGame(0);
        quizmanager.mainmenuPanel.SetActive(false);
        quizmanager.quizPanel.SetActive(true);
        //
        quizmanager.choiceTrue.gameObject.SetActive(true);
        quizmanager.choiceFalse.gameObject.SetActive(true);
        quizmanager.TrueAnswerText.gameObject.SetActive(true);
        quizmanager.FalseAnswerText.gameObject.SetActive(true);
        //
        quizmanager.choiceA.gameObject.SetActive(false);
        quizmanager.choiceB.gameObject.SetActive(false);
        quizmanager.choiceC.gameObject.SetActive(false);
        quizmanager.choiceD.gameObject.SetActive(false);
        quizmanager.FinalAnswerText.gameObject.SetActive(false);
    }

    public void Button2Click()
    {
        quizmanager.StartGame(1);
        quizmanager.mainmenuPanel.SetActive(false);
        quizmanager.quizPanel.SetActive(true);
        //
        quizmanager.choiceTrue.gameObject.SetActive(false);
        quizmanager.choiceFalse.gameObject.SetActive(false);
        quizmanager.TrueAnswerText.gameObject.SetActive(false);
        quizmanager.FalseAnswerText.gameObject.SetActive(false);
        //
        quizmanager.choiceA.gameObject.SetActive(true);
        quizmanager.choiceB.gameObject.SetActive(true);
        quizmanager.choiceC.gameObject.SetActive(true);
        quizmanager.choiceD.gameObject.SetActive(true);
        quizmanager.FinalAnswerText.gameObject.SetActive(true);
    }
}
