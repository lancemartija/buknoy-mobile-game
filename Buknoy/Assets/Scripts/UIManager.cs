using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   [SerializeField] private GameObject gameOverScreen;

   public void Awake()
   {
      gameOverScreen.SetActive(false);
   }

   public void GameOver()
   {
      gameOverScreen.SetActive(true);
      Time.timeScale = 0;
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
}
