using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().StopMusic();
        Invoke("LoadLevel0", 0f);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("MenuHub");
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().BacktoMenuHubBGM();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    void LoadLevel0()
    {
        SceneManager.LoadSceneAsync("Chapter0");
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().MainMenutoGameBGM(0);
        Time.timeScale = 1;
    }
}
