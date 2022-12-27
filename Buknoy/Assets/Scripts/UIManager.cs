using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   [SerializeField] private GameObject gameOverScreen;
   [SerializeField] private GameObject FinishScreen;

   public void Awake()
   {
      gameOverScreen.SetActive(false);
      FinishScreen.SetActive(false);
   }

   public void GameOver()
   {
      gameOverScreen.SetActive(true);
      Time.timeScale = 0;
   }

   public void Finish()
   {
      FinishScreen.SetActive(true);
      Time.timeScale = 1;
   }

   public void Restart()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      Time.timeScale = 1;
   }

   public void MainMenu()
   {
      SceneManager.LoadScene("MenuHub");
      Time.timeScale = 1;
   }

   public void TakeQuiz()
   {
      SceneManager.LoadScene("Quizzes");
      Time.timeScale = 1;
   }
}
