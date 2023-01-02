using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizUI : MonoBehaviour
{
    [SerializeField] public QuizManager quizmanager;
    [SerializeField] public QuizResultsManager quizresults;
    [SerializeField] public QuizAudio quizaudio;
    [SerializeField] private float loadingtime = 2f;

    [SerializeField] Animator transition;

    [SerializeField] private List<Button> HubButtons, ExitButtons;
    

    [SerializeField] public GameObject confirmexitPanel, confirmdeletePanel, pausePanel, settingsPanel;

    public GameObject PausePanel {get {return pausePanel;}}
    public GameObject ConfirmExitPanel {get {return confirmexitPanel;}}
    public GameObject ConfirmDeletePanel {get {return confirmdeletePanel;}}
    public GameObject SettingsPanel {get {return settingsPanel;}}

    //Transition
    void Start()
    {
        StartCoroutine(InitializeScene());
    }
    
    IEnumerator InitializeScene()
    {
        yield return new WaitForSeconds(loadingtime);
        transition.SetTrigger("Out"); //Transition Scene
        for (int i = 0; i < HubButtons.Count; i++)
        {
            Button button = HubButtons[i];
            button.interactable = true;
        }
    }
    void DisableButtons()
    {
        for (int i = 0; i < HubButtons.Count; i++)
        {
            Button button = HubButtons[i];
            button.interactable = false;
        }
    }
    void DisableButtons2()
    {
        for (int i = 0; i < ExitButtons.Count; i++)
        {
            Button button = ExitButtons[i];
            button.interactable = false;
        }
    }
    
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
        quizmanager.Load();
        quizmanager.mainmenuPanel.SetActive(false);
        quizmanager.quizPanel.SetActive(true);
    }

    public void Button2Click()
    {
        quizmanager.StartGame(2);
        quizmanager.Load();
        quizmanager.mainmenuPanel.SetActive(false);
        quizmanager.quizPanel.SetActive(true);
    }

    public void Button3Click()
    {
        quizmanager.StartGame(3);
        quizmanager.Load();
        quizmanager.mainmenuPanel.SetActive(false);
        quizmanager.quizPanel.SetActive(true);
    }

    public void Button4Click()
    {
        quizmanager.StartGame(4);
        quizmanager.Load();
        quizmanager.mainmenuPanel.SetActive(false);
        quizmanager.quizPanel.SetActive(true);
    }
    public void ButtonResultsClick() //View Results
    {
        quizmanager.quizResultsPanel.SetActive(true);
        quizmanager.ViewResults();
    }
    public void ButtonSettingsClick() //View Settings
    {
       SettingsPanel.SetActive(true);
    }
    public void BacktoQuizHub()
    {
        quizmanager.quizResultsPanel.SetActive(false);
    }
    public void BacktoMenuHub()
    {
        transition.SetTrigger("In");
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().StopMusic();
        Invoke("DisableButtons", 0.2f);
        Invoke("LoadMenuHub", 2f);
    }
    //Load other Scenes after 2 seconds
    void LoadMenuHub()
    {
        SceneManager.LoadScene("MenuHub");
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().BacktoMainMenuBGM();
    }
    void LoadQuizScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().MainMenutoQuizMenuBGM();
    }
    //Quiz Game Buttons
    public void PauseButton()
    {
       GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().PauseBGM();
       quizmanager.gamestatus = GameStatus.Menu;
       PausePanel.SetActive(true);
    }
    public void ConfirmExitButton()
    {
        ConfirmExitPanel.SetActive(true);
    }
    public void ResumeButton()
    {
        if (quizmanager.quizChoice > 0)
        {
            quizmanager.gamestatus = GameStatus.Playing;
        }
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().UnPauseBGM();
        PausePanel.SetActive(false);
        ConfirmExitPanel.SetActive(false);
    }

    public void RetryButton ()
    {
        transition.SetTrigger("In");
        Invoke("DisableButtons2", 0.2f);
        Invoke("LoadQuizScene", 2f);
    }
    //Quiz Results Buttons
    public void ConfirmDeleteButton()
    {
        ConfirmDeletePanel.SetActive(true);
    }
    public void Resume2Button()
    {
        ConfirmDeletePanel.SetActive(false);
    }
    public void DeleteHighScoreDataButton()
    {
        ConfirmDeletePanel.SetActive(false);
        quizresults.DeleteHighScoreData();
        quizmanager.results.Clear();
        for (int i = 0; i < 4; i++)
        {
            quizmanager.QuizScore[i].text = "HIGH SCORE:\n 0";
            quizmanager.QuizStreak[i].text = "HIGHEST STREAK:\n 0";
        }
        Debug.Log("File deleted!");
    }

    // Quiz Settings Buttons
    public void BacktoMenuHub2()
    {
        SettingsPanel.SetActive(false);
    }
}
