using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuControls : MonoBehaviour
{
    public GameObject pauseMenu;

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
         GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().PauseBGM();
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().UnPauseBGM();
        Time.timeScale = 1;
    }
    public void GotoMenuHub()
    {
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().StopMusicNow();
        SceneManager.LoadScene("MenuHub");
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().BacktoMainMenuBGM();
        Time.timeScale = 1;
    }
}
