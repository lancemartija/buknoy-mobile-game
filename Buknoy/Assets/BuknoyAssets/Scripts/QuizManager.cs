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
  [SerializeField] private Button choiceTrue, choiceFalse;
  [SerializeField] private List<Button> choiceMultiple;
  [SerializeField] private Text TrueAnswerText, FalseAnswerText, FinalAnswerText, RightAnswerText;
  [SerializeField] private Image questionIMG;
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


  private int  scoreCount = 0, streakCount = 0, quizChoice = 0, maxQuestions = 0, loopQuestions = 0;
  private float currentTimer;


  public void StartGame(int index)
  {
    scoreCount = 0;
    streakCount = 0;
    quizChoice = index;
    currentTimer = timeLimit;

    switch (quizChoice)
    {
      case 0:
        maxQuestions = 3;
        break;
      case 1:
        maxQuestions = 5;
        break;

      case 2:
        maxQuestions = 7;
        for (int i = 0; i < choiceMultiple.Count; i++)
        {
            Button localBtn = choiceMultiple[i];
            localBtn.onClick.AddListener(() => MultipleOnClick(localBtn));
        }

        break;
      case 3:
        maxQuestions = 7;
        for (int i = 0; i < choiceMultiple.Count; i++)
        {
            Button localBtn = choiceMultiple[i];
            localBtn.onClick.AddListener(() => MultipleOnClick(localBtn));
        }

        break;
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

    if (quizChoice >= 2)
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

    QuestionType();
  }

  void QuestionType()
  {
    if (currentQuestion.needsImage)
    {
      questionIMG.gameObject.SetActive(true);

      questionIMG.sprite = currentQuestion.questionImage;
    }
    else
    {
      questionIMG.gameObject.SetActive(false);
    }

    if (currentQuestion.isTrueorFalse)
    {
      //Activate True or False Buttons and Text
      choiceTrue.gameObject.SetActive(true);
      choiceFalse.gameObject.SetActive(true);
      TrueAnswerText.gameObject.SetActive(true);
      FalseAnswerText.gameObject.SetActive(true);

      //Deactivates Multiple Choice Buttons and Text
      for (int i = 0; i < choiceMultiple.Count; i++)
      {
        choiceMultiple[i].gameObject.SetActive(false);
      }
      FinalAnswerText.gameObject.SetActive(false);
      RightAnswerText.gameObject.SetActive(false);
    }
    else if (currentQuestion.isMultipleChoice)
    {
      //Activates Multiple Choice Buttons and Text
      for (int i = 0; i < choiceMultiple.Count; i++)
      {
        choiceMultiple[i].gameObject.SetActive(true);
      }
        FinalAnswerText.gameObject.SetActive(true);
        RightAnswerText.gameObject.SetActive(true);

      //Deactivates True or False Buttons and Text
      choiceTrue.gameObject.SetActive(false);
      choiceFalse.gameObject.SetActive(false);
      TrueAnswerText.gameObject.SetActive(false);
      FalseAnswerText.gameObject.SetActive(false);
    }
  }

  IEnumerator TransitiontoNextQuestion ()
  {
     yield return new WaitForSeconds(timeBetweenQuestions);
     loopQuestions++;

     if (loopQuestions != maxQuestions)
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
      streakCount++;
      StreakText.text = "STREAK: " + streakCount;
      scoreCount += 100 * streakCount;
      ScoreText.text = "SCORE:" + scoreCount;
      Debug.Log("CORRECT!");
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
    loopQuestions *= 0;
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
   //question info
    public string fact;
    public Sprite questionImage;
    public bool needsImage;
    public bool isTrueorFalse;
    public bool isMultipleChoice;

    //true or false
    public bool isTrue;

    //multiple choice
    public List<string> answers;
    public string correctAnswer;
}

