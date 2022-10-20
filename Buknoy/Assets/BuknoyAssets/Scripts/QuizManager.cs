using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class QuizManager : MonoBehaviour
{
  private List<Question> unansweredQuestions;
  public Question currentQuestion;


  [SerializeField] public GameObject gameoverPanel, mainmenuPanel, quizPanel;
  [SerializeField] public Button choiceTrue, choiceFalse, choiceA, choiceB, choiceC, choiceD;
  [SerializeField] public Text TrueAnswerText, FalseAnswerText, FinalAnswerText;
  [SerializeField] private Text factText, scoreText, timeText, streakText, finalscoreText, finalstreakText;
  [SerializeField] private float timeBetweenQuestions = 2f, timeLimit = 60;
  [SerializeField] private QuizUI quizui;
  [SerializeField] private List<QuizQandA> quizData;
  [SerializeField] Animator animator;


  public Button ChoiceTrue {get {return  choiceTrue;}}
  public Button ChoiceFalse {get {return  choiceFalse;}}
  public Button ChoiceA {get {return  choiceA;}}
  public Button ChoiceB {get {return  choiceB;}}
  public Button ChoiceC {get {return  choiceC;}}
  public Button ChoiceD {get {return  choiceD;}}
  public Text ScoreText {get {return scoreText;}}
  public Text TimeText {get {return timeText;}}
  public Text StreakText {get {return streakText;}}
  public Text FinalScoreText {get {return finalscoreText;}}
  public Text FinalStreakText {get {return finalstreakText;}}

  public GameStatus gamestatus = GameStatus.Menu;

  public GameObject GameOverPanel {get {return gameoverPanel;}}

  public int scoreCount = 0, streakCount = 0, quizChoice = 0;
  private float currentTimer;


  public void StartGame(int index)
  {
    scoreCount = 0;
    streakCount = 0;
    quizChoice = index;
    currentTimer = timeLimit;

   unansweredQuestions = quizData[index].questions.ToList<Question>();
    gamestatus = GameStatus.Playing;
    SetCurrentQuestion();
  
  }

  public void Update()
  {
    if (gamestatus == GameStatus.Playing)
    {
      currentTimer -= Time.deltaTime;
      SetTimer(currentTimer);
    }
  }

  void SetCurrentQuestion ()
  {
    int randomQuestionIndex = UnityEngine.Random.Range (0, unansweredQuestions.Count);
    currentQuestion = unansweredQuestions[randomQuestionIndex];

    unansweredQuestions.RemoveAt(randomQuestionIndex);

    factText.text = currentQuestion.fact;

  }

  IEnumerator TransitiontoNextQuestion ()
  {
     yield return new WaitForSeconds(timeBetweenQuestions);

     if (unansweredQuestions.Count > 0)
     {
        TrueAnswerText.text = "";
        FalseAnswerText.text = "";
        Invoke("SetCurrentQuestion", 0.4f);
        animator.SetTrigger("Idle");
     }
     else
     {
      GameOver();
     }

  }

  public void UserSelectTrue()
  {
    animator.SetTrigger("True"); 
    if (currentQuestion.isTrue)
    {
      TrueAnswerText.text = "CORRECT!";
      Debug.Log("CORRECT!");
      streakCount++;
      StreakText.text = "STREAK: " + streakCount;
      scoreCount += 100 * streakCount;
      ScoreText.text = "SCORE:" + scoreCount;
    }
    else
    {
      TrueAnswerText.text = "INCORRECT!";
      streakCount *= 0;
      StreakText.text = "STREAK: " + streakCount;
      Debug.Log("INCORRECT!");
    }

    StartCoroutine(TransitiontoNextQuestion());
  }

  public void UserSelectFalse()
  {
    animator.SetTrigger("False");
    if (!currentQuestion.isTrue)
    {
      FalseAnswerText.text = "CORRECT!";
      Debug.Log("CORRECT!");
      streakCount++;
      StreakText.text = "STREAK: " + streakCount;
      scoreCount += 100 * streakCount;
      ScoreText.text = "SCORE:" + scoreCount;
    }
    else
    {
      FalseAnswerText.text = "INCORRECT!";
      streakCount *= 0;
      StreakText.text = "STREAK: " + streakCount;
      Debug.Log("INCORRECT!");
    }

    StartCoroutine(TransitiontoNextQuestion());
  }

  private void SetTimer (float value)
  {
    TimeSpan time = TimeSpan.FromSeconds(value);
    TimeText.text = time.ToString("mm' : 'ss");
    if (currentTimer <= 0)
    {
      GameOver();
    }
  }
  private void GameOver()
  {

    gamestatus = GameStatus.Menu;
    GameOverPanel.SetActive(true);
    FinalScoreText.text = "Final Score: " + scoreCount;
    FinalStreakText.text = "Final Streak: " + streakCount;
  }

  public void RetryButton ()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}

[System.Serializable]
public enum GameStatus
{
  Menu, //Timer will not count down while this GameStatus is active
  Playing //Timer will count down while this GameStatus is active
}

[System.Serializable]
public class Question
{
   //true or false
    public string fact;
    public bool isTrue;

    //multiple choice
    public List<string> answers;
    public string correctAnswer;
}

