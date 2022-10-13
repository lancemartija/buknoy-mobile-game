using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    [SerializeField] public QuizManager quizmanager;
    [SerializeField] private Button btn;
    

    void Start()
    {
         //GetComponent<Button>().onClick.AddListener(ButtonClick);
    }

    public void ButtonClick()
    {
        //quizmanager.StartGame(0);
        //quizmanager.mainmenuPanel.SetActive(false);
       // quizmanager.quizPanel.SetActive(true);
    }
}
