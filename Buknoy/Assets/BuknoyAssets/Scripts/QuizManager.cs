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


  [SerializeField] public GameObject gameoverPanel, mainmenuPanel, quizPanel, pausePanel, confirmexitPanel, quizResultsPanel;
  [SerializeField] private List<QuizQandA> quizData;
  [SerializeField] private List<Text> QuizScore, QuizStreak;
  [SerializeField] private Button choiceTrue, choiceFalse;
  [SerializeField] private List<Button> choiceMultiple;
  [SerializeField] private Text TrueAnswerText, FalseAnswerText, FinalAnswerText, RightAnswerText;
  [SerializeField] private Image questionIMG;
  [SerializeField] private Text factText, scoreText, timeText, streakText, finalscoreText, finalstreakText;
  [SerializeField] private float timeBetweenQuestions = 2.5f, timeLimit = 60;
  [SerializeField] Animator animator;

  public List<int> HighScore = new List<int>() {0,0,0,0};
  public List<int> HighStreak = new List<int>() {0,0,0,0};

  public Button ChoiceTrue {get {return  choiceTrue;}}
  public Button ChoiceFalse {get {return  choiceFalse;}}
  public Text ScoreText {get {return scoreText;}}
  public Text TimeText {get {return timeText;}}
  public Text StreakText {get {return streakText;}}
  public Text FinalScoreText {get {return finalscoreText;}}
  public Text FinalStreakText {get {return finalstreakText;}}

  public GameStatus gamestatus = GameStatus.Menu;

  public GameObject GameOverPanel {get {return gameoverPanel;}}
  public GameObject PausePanel {get {return pausePanel;}}
  public GameObject ConfirmExitPanel {get {return confirmexitPanel;}}

  private int  quizChoice = 0;
  public int scoreCount, streakCount, maxQuestions = 0, loopQuestions = 0;
  private float currentTimer;


  public void StartGame(int index)
  {
    scoreCount = 0;
    streakCount = 0;
    
    quizChoice = index;
    currentTimer = timeLimit;

    switch (quizChoice)
    {
      case 0: //Tutorial; Timer does not count down
        maxQuestions = 4;
        for (int i = 0; i < choiceMultiple.Count; i++)
        {
            Button localBtn = choiceMultiple[i];
            localBtn.onClick.AddListener(() => MultipleOnClick(localBtn));
        }
        break;
      case 1: // Quiz 1, True or False only
        maxQuestions = 2; //5
        gamestatus = GameStatus.Playing;
        break;
      case 2: //Quiz 2, Multiple Choice only
        maxQuestions = 2; //7
        for (int i = 0; i < choiceMultiple.Count; i++)
        {
            Button localBtn = choiceMultiple[i];
            localBtn.onClick.AddListener(() => MultipleOnClick(localBtn));
        }
        gamestatus = GameStatus.Playing;
        break;
      case 3://Quiz 3, Multiple Choice and True or False
        maxQuestions = 2; //7
        for (int i = 0; i < choiceMultiple.Count; i++)
        {
            Button localBtn = choiceMultiple[i];
            localBtn.onClick.AddListener(() => MultipleOnClick(localBtn));
        }
        gamestatus = GameStatus.Playing;
        break;
    }

    unansweredQuestions = quizData[index].questions.ToList<Question>();
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
    Debug.Log("QUESTION #:" + (loopQuestions + 1));
    Debug.Log("MAX QUESTIONS:" + maxQuestions);
    int randomQuestionIndex = UnityEngine.Random.Range (0, unansweredQuestions.Count);
    currentQuestion = unansweredQuestions[randomQuestionIndex];

    unansweredQuestions.RemoveAt(randomQuestionIndex);

    factText.text = currentQuestion.fact;

    if (quizChoice != 1) //Quiz 1 is True or False only
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
        animator.SetTrigger("Idle");
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
    bool val = UserSelectMultiple(btn.name);
    StartCoroutine(TransitiontoNextQuestion());
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

    //Saved and Displayed in View Results panel
    HighScore.Insert(quizChoice, scoreCount);
    HighStreak.Insert(quizChoice, streakCount);
    //for (int i = 0; i < HighScore.Count; i++)
    //{
      //Debug.Log("RESULTS TOTAL:" + HighScore.Count);
      //Debug.Log("SCORE #" + i + ":" + HighScore[i]);
      //Debug.Log("STREAK #" + i + ":" + HighStreak[i]);
    //}
  }
  public void ViewResults()
  {
    Debug.Log("RESULTS TOTAL:" + HighScore.Count);
    for (int i = 0; i < HighScore.Count; i++)
    {
      QuizScore[i].text = "HIGH SCORE:\n " + HighScore[i];
      QuizStreak[i].text = "HIGHEST STREAK:\n " + HighStreak[i];
    }
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

