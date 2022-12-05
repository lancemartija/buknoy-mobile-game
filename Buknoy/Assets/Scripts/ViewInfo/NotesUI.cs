using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NotesUI : MonoBehaviour
{
    [SerializeField] public GameObject inventoryPanel, notesPanel;
    [SerializeField] public Text quizlblText;
    [SerializeField] private List<Button> HubButtons;
    [SerializeField] public NotesAudio notesaudio;

    [SerializeField] Animator transition;
     

    public Text QuizLabelText {get {return quizlblText;}}
    public GameObject NotesPanel {get {return notesPanel;}}
    public GameObject InventoryPanel {get {return inventoryPanel;}}

    //Transition
    void Start()
    {
        transition.SetTrigger("Out");
        Invoke("EnableButtons", 0.7f);
    }
    void EnableButtons()
    {
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
    //Notes Hub Buttons
    public void NotesButton0Click()
    {
       QuizLabelText.text = "Quiz 1 Notes";
       inventoryPanel.SetActive(true);
       notesPanel.SetActive(false);
    }
    public void NotesButton1Click()
    {
       QuizLabelText.text = "Quiz 2 Notes";
       inventoryPanel.SetActive(true);
       notesPanel.SetActive(false);
    }
    public void NotesButton2Click()
    {
       QuizLabelText.text = "Quiz 3 Notes";
       inventoryPanel.SetActive(true);
       notesPanel.SetActive(false);
    }
    public void NotesButton3Click()
    {
       QuizLabelText.text = "Quiz 4 Notes";
       inventoryPanel.SetActive(true);
       notesPanel.SetActive(false);
    }
    public void BacktoNotesHub()
    {
       inventoryPanel.SetActive(false);
       notesPanel.SetActive(true);
    }
    public void BacktoMenuHub()
    {
        transition.SetTrigger("In");
        notesaudio.EndMusic();
        Invoke("DisableButtons", 0.2f);
        Invoke("LoadMenuHub", 2f);
    }
    //Load other Scenes after 2 seconds
    void LoadMenuHub()
    {
        SceneManager.LoadScene("MenuHub");
    }
}
