using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
    }
    public void PlayGame()
    {
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().StopMusic();
        Invoke("LoadLevel0", 2f);
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene("MenuHub");
    }
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
    void LoadLevel0()
    {
        SceneManager.LoadScene("Prologue");
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().MainMenutoGameBGM(0);
        Time.timeScale = 1;
    }
}
