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
  [SerializeField] public Button choiceTrue, choiceFalse;
  [SerializeField] public List<Button> choiceMultiple;
  [SerializeField] public Text TrueAnswerText, FalseAnswerText, FinalAnswerText, RightAnswerText;
  [SerializeField] private Text factText, scoreText, timeText, streakText, finalscoreText, finalstreakText;
  [SerializeField] private float timeBetweenQuestions = 2.5f, timeLimit = 60;
  [SerializeField] private QuizUI quizui;
  [SerializeField] private List<QuizQandA> quizData;
  [SerializeField] Animator animator;


  public Button ChoiceTrue {get {return  choiceTrue;}}
  public Button ChoiceFalse {get {return  choiceFalse;}}
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

    if (quizChoice > 1) // Multiple Choice Only
    {
       for (int i = 0; i < choiceMultiple.Count; i++)
        {
            Button localBtn = choiceMultiple[i];
            localBtn.onClick.AddListener(() => MultipleOnClick(localBtn));
        }
    }

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

    if (quizChoice > 1)
    {
       List<string> ansOptions = ShuffleList.ShuffleListItems<string>(currentQuestion.answers);

        //assign options to respective option buttons
        for (int i = 0; i < choiceMultiple.Count; i++)
        {
            //set the child text
            choiceMultiple[i].GetComponentInChildren<Text>().text = ansOptions[i];
            choiceMultiple[i].name = ansOptions[i];    //set the name of button
        }
    }

  }

  IEnumerator TransitiontoNextQuestion ()
  {
     yield return new WaitForSeconds(timeBetweenQuestions);

     if (unansweredQuestions.Count > 0)
     {
        TrueAnswerText.text = "";
        FalseAnswerText.text = "";
        FinalAnswerText.text = "";
        RightAnswerText.text = "";
        Invoke("SetCurrentQuestion", 0.3f);
        animator.SetTrigger("Idle");
     }
     else
     {
      GameOver();
     }

  }

  //True or False Only
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

  //Multiple Choice Only

  void MultipleOnClick(Button btn)
  {
    if (gamestatus == GameStatus.Playing)
    {
       bool val = UserSelectMultiple(btn.name);
       StartCoroutine(TransitiontoNextQuestion());
    }
  }


  public bool UserSelectMultiple(string selectedChoice)
  {
    bool correct = false;

    animator.SetTrigger("Multiple");
    if (currentQuestion.correctAnswer == selectedChoice)
    {
      correct = true;
      FinalAnswerText.text = "CORRECT!";
      Debug.Log("CORRECT!");
      streakCount++;
      StreakText.text = "STREAK: " + streakCount;
      scoreCount += 100 * streakCount;
      ScoreText.text = "SCORE:" + scoreCount;
    }
    else
    {
      FinalAnswerText.text = "INCORRECT!";
      RightAnswerText.text = "Correct answer was: " + currentQuestion.correctAnswer;
      streakCount *= 0;
      StreakText.text = "STREAK: " + streakCount;
      Debug.Log("INCORRECT!");
    }

    return correct;
  }


  //Miscellaneous
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

