using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NotesUI : MonoBehaviour
{
    [SerializeField] public GameObject inventoryPanel, notesPanel, zoomPanel;
    [SerializeField] public Text quizlblText;
    [SerializeField] private List<Button> HubButtons;

    public NotesManager notesmanager;

    [SerializeField] Animator transition;
     

    public Text QuizLabelText {get {return quizlblText;}}
    public GameObject NotesPanel {get {return notesPanel;}}
    public GameObject InventoryPanel {get {return inventoryPanel;}}
    public GameObject ZoomPanel {get {return zoomPanel;}}
    [SerializeField] private float loadingtime = 2f;
    //Transition
    void Start()
    {
        StartCoroutine(InitializeScene());
    }
    IEnumerator InitializeScene()
    {
        yield return new WaitForSeconds(loadingtime);
        transition.SetTrigger("Out"); //Transition
        for (int i = 0; i < HubButtons.Count; i++)
        {
            Button button = HubButtons[i];
            button.interactable = true;
        }
    }
    // void DisableButtons()
    // {
    //     for (int i = 0; i < HubButtons.Count; i++)
    //     {
    //         Button button = HubButtons[i];
    //         button.interactable = false;
    //     }
    // }
    //Notes Hub Buttons
    public void NotesButton0Click()
    {
       notesmanager.NotesArrange(0);
       QuizLabelText.text = "Quiz 1 Notes";
       inventoryPanel.SetActive(true);
       notesPanel.SetActive(false);
    }
    public void NotesButton1Click()
    {
       notesmanager.NotesArrange(1);
       QuizLabelText.text = "Quiz 2 Notes";
       inventoryPanel.SetActive(true);
       notesPanel.SetActive(false);
    }
    public void NotesButton2Click()
    {
       notesmanager.NotesArrange(2);
       QuizLabelText.text = "Quiz 3 Notes";
       inventoryPanel.SetActive(true);
       notesPanel.SetActive(false);
    }
    public void NotesButton3Click()
    {
       notesmanager.NotesArrange(3);
       QuizLabelText.text = "Quiz 4 Notes";
       inventoryPanel.SetActive(true);
       notesPanel.SetActive(false);
    }
    public void NotesZoomButtonClick(int index)
    {
        notesmanager.NotesZoom(index);
        zoomPanel.SetActive(true);
    }
    public void ZoomBackClick()
    {
        zoomPanel.SetActive(false);
    }
    public void BacktoNotesHub()
    {
       notesmanager.NotesErase();
       inventoryPanel.SetActive(false);
       notesPanel.SetActive(true);
    }
    public void BacktoMenuHub()
    {
        transition.SetTrigger("In");
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().StopMusic();
        // Invoke("DisableButtons", 0.2f);
        Invoke("LoadMenuHub", 0f);
    }
    //Load other Scenes after 2 seconds
    void LoadMenuHub()
    {
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().BacktoMainMenuBGM();
        SceneManager.LoadScene("MenuHub");
    }
}
