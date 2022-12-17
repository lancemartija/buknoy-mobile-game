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


  [SerializeField] public GameObject gameoverPanel, mainmenuPanel, quizPanel, quizResultsPanel;
  [SerializeField] public List<Text> QuizScore, QuizStreak;
  [SerializeField] private List<QuizQandA> quizData;
  [SerializeField] private Button choiceTrue, choiceFalse;
  [SerializeField] private List<Button> choiceMultiple;
  [SerializeField] private Text TrueAnswerText, FalseAnswerText, FinalAnswerText, RightAnswerText;
  [SerializeField] private Image questionIMG;
  [SerializeField] private Text factText, scoreText, timeText, streakText, finalscoreText, finalstreakText, questionnoText;
  [SerializeField] private float timeBetweenQuestions = 2.5f, timeLimit = 60;
  [SerializeField] Animator animator;
  [SerializeField] public QuizAudio quizaudio;
  [SerializeField] public AudioSettings audiosettings;

  public List <QuizResults> results = new List<QuizResults>();
  


  public Button ChoiceTrue {get {return  choiceTrue;}}
  public Button ChoiceFalse {get {return  choiceFalse;}}
  public Text ScoreText {get {return scoreText;}}
  public Text TimeText {get {return timeText;}}
  public Text StreakText {get {return streakText;}}
  public Text QuestionNoText {get {return questionnoText;}}
  public Text FinalScoreText {get {return finalscoreText;}}
  public Text FinalStreakText {get {return finalstreakText;}}

  public GameStatus gamestatus = GameStatus.Menu;

  public GameObject GameOverPanel {get {return gameoverPanel;}}

  public int  quizChoice = 0;
  public int scoreCount, streakCount, maxQuestions = 0, loopQuestions = 0;
  private float currentTimer;


  public void StartGame(int index)
  {
    scoreCount = 0;
    streakCount = 0;
    
    quizChoice = index;

    switch (quizChoice)
    {
      case 0: //Tutorial; Timer does not count down
        maxQuestions = 4;
        currentTimer = timeLimit;
        for (int i = 0; i < choiceMultiple.Count; i++)
        {
            Button localBtn = choiceMultiple[i];
            localBtn.onClick.AddListener(() => MultipleOnClick(localBtn));
        }
        break;
      case 1: // Quiz 1, True or False only
        maxQuestions = 5; //5
        currentTimer = timeLimit;
        gamestatus = GameStatus.Playing;

        break;
      case 2: //Quiz 2, Multiple Choice only
        maxQuestions = 7; //7
        currentTimer = timeLimit;
        for (int i = 0; i < choiceMultiple.Count; i++)
        {
            Button localBtn = choiceMultiple[i];
            localBtn.onClick.AddListener(() => MultipleOnClick(localBtn));
        }
        gamestatus = GameStatus.Playing;
        break;
      case 3://Quiz 3, Multiple Choice and True or False
        maxQuestions = 7; //7
        currentTimer = timeLimit;
        for (int i = 0; i < choiceMultiple.Count; i++)
        {
            Button localBtn = choiceMultiple[i];
            localBtn.onClick.AddListener(() => MultipleOnClick(localBtn));
        }
        gamestatus = GameStatus.Playing;
        break;
      case 4://Quiz 4, Multiple Choice and True or False; most questions with extra time
        maxQuestions = 10; //10
        currentTimer = 90;
        for (int i = 0; i < choiceMultiple.Count; i++)
        {
            Button localBtn = choiceMultiple[i];
            localBtn.onClick.AddListener(() => MultipleOnClick(localBtn));
        }
        gamestatus = GameStatus.Playing;
        break;
    }
    quizaudio.LoadSFXSettings();
    GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().QuizMenutoQuizGameBGM();
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

    QuestionNoText.text = "QUESTION #" + (loopQuestions + 1) +  " / " + maxQuestions; 

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
      quizaudio.CorrectSound();
      TrueAnswerText.text = "CORRECT!";
      Debug.Log("CORRECT!");
      streakCount++;
      StreakText.text = "STREAK: " + streakCount;
      scoreCount += 100 * streakCount;
      ScoreText.text = "SCORE:" + scoreCount;
    }
    else
    {
      quizaudio.IncorrectSound();
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
      quizaudio.CorrectSound();
      FalseAnswerText.text = "CORRECT!";
      Debug.Log("CORRECT!");
      streakCount++;
      StreakText.text = "STREAK: " + streakCount;
      scoreCount += 100 * streakCount;
      ScoreText.text = "SCORE:" + scoreCount;
    }
    else
    {
      quizaudio.IncorrectSound();
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
      quizaudio.CorrectSound();
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
      quizaudio.IncorrectSound();
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
    Debug.Log("Final Score:" + scoreCount);
    //Debug.Log("High Score:" + results[quizChoice - 1].highScore);
    gamestatus = GameStatus.Menu;
    GameOverPanel.SetActive(true);
    quizaudio.GameOverSound();
    FinalScoreText.text = "Final Score: " + scoreCount;
    FinalStreakText.text = "Final Streak: " + streakCount;

    if (quizChoice > 0)
    {
      if (scoreCount > results[quizChoice - 1].highScore || streakCount > results[quizChoice - 1].highStreak)
      {
        if (scoreCount > results[quizChoice - 1].highScore)
          {
            AddNewScore(scoreCount, quizChoice);
          }
          if (streakCount > results[quizChoice - 1].highStreak)
          {
            AddNewStreak(streakCount, quizChoice);
          }
          Save();
      }
    }

  
  }

  //View Results Methods
  public void SetDefaultResults(int score, int streak, int quizchoice)
  {
     results.Add(new QuizResults { highScore = score, highStreak = streak, quiznumber = quizchoice});
  }

  private void AddNewScore(int score, int quizchoice)
  {
    QuizResults updateresults = new QuizResults() { highScore = score, quiznumber = quizchoice};
    int resultsindex = results.FindIndex(results => results.quiznumber == updateresults.quiznumber);
    if (resultsindex != -1)
    {
        results[resultsindex].highScore = updateresults.highScore;
    }
  }

  private void AddNewStreak(int streak, int quizchoice)
  {
    QuizResults updateresults = new QuizResults() {highStreak = streak, quiznumber = quizchoice};
    int resultsindex = results.FindIndex(results => results.quiznumber == updateresults.quiznumber);
    if (resultsindex != -1)
    {
        results[resultsindex].highStreak = updateresults.highStreak;
    }
  }
  public void Save()
  {
    QuizResultsManager.instance.SaveScores(results);
  }
  public void Load()
  {
    results = QuizResultsManager.instance.LoadScores();
  }
  public void ViewResults()
  {
    Load();  
    results.Sort((QuizResults x, QuizResults y) => x.quiznumber.CompareTo(y.quiznumber));
    Debug.Log("RESULTS TOTAL:" + results.Count);
    for (int i = 0; i < results.Count; i++)
    {
      QuizScore[i].text = "HIGH SCORE:\n " + results[i].highScore;
      QuizStreak[i].text = "HIGHEST STREAK:\n " + results[i].highStreak;
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


[System.Serializable]
public class QuizResults
{
   //question info
    public int highScore;
    public int highStreak;
    public int quiznumber;
}

