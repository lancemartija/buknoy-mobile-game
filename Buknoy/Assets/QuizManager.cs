using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
  public Question[] questions;
  private static List<Question> unansweredQuestions;

  private Question currentQuestion;

  [SerializeField]
  private Text factText;

  [SerializeField]
  private float timeBetweenQuestions = 2f;

   [SerializeField]
  private Text TrueAnswerText;

   [SerializeField]
  private Text FalseAnswerText;

  [SerializeField]
  Animator animator;

  void Start()
  {
    if (unansweredQuestions == null || unansweredQuestions.Count == 0)
    {
        unansweredQuestions = questions.ToList<Question>();
    }

    SetCurrentQuestion();
  }

  void SetCurrentQuestion ()
  {
    int randomQuestionIndex = Random.Range (0, unansweredQuestions.Count);
    currentQuestion = unansweredQuestions[randomQuestionIndex];

    factText.text = currentQuestion.fact;

    if (currentQuestion.isCorrect)
    {
      TrueAnswerText.text = "INCORRECT!";
      FalseAnswerText.text = "CORRECT!";
    }
    else
    {
      TrueAnswerText.text = "CORRECT!";
      FalseAnswerText.text = "INCORRECT!";
    }

  }

  IEnumerator TransitiontoNextQuestion ()
  {
     unansweredQuestions.Remove(currentQuestion);

     yield return new WaitForSeconds(timeBetweenQuestions);

     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

  }

  public void UserSelectTrue()
  {
    animator.SetTrigger("True"); 
    if (currentQuestion.isCorrect)
    {
      Debug.Log("CORRECT!");
    }
    else
    {
      Debug.Log("INCORRECT!");
    }

    StartCoroutine(TransitiontoNextQuestion());
  }
  public void UserSelectFalse()
  {
    animator.SetTrigger("False");
    if (!currentQuestion.isCorrect)
    {
      Debug.Log("CORRECT!");
    }
    else
    {
      Debug.Log("INCORRECT!");
    }

    StartCoroutine(TransitiontoNextQuestion());
  }
}
