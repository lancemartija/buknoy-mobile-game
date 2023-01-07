using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelSelectMenu : MonoBehaviour
{
    public GameObject scrollbar;
    float scroll_pos = 0;
    float[]pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for(int i = 0; i < pos.Length; i++)
        {
            pos [i] = distance * i;
        }
        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar> ().value;
        }
        else
        {
            for(int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos [i] + (distance / 2) && scroll_pos > pos [i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar> ().value = Mathf.Lerp (scrollbar.GetComponent<Scrollbar> ().value, pos [i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++){
            if (scroll_pos <pos [i] + (distance / 2) && scroll_pos > pos [i] - (distance / 2)) {
                transform.GetChild (i).localScale = Vector2.Lerp (transform.GetChild(i).localScale,new Vector2(1f,1f), 0.1f);
                for (int a = 0; a < pos.Length; a++) {
                    if (a!= i) {
                        transform.GetChild (a).localScale = Vector2.Lerp (transform.GetChild(a).localScale, new Vector2(0.8f,0.8f), 0.1f);
                    }
                
                }
            }
        }

    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuHub");
    }
    public void GoToPrologue()
    {
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().StopMusic();
        Invoke("LoadLevel0", 0f);
    }
    public void GoToLevel1()
    {
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().StopMusic();
        Invoke("LoadLevel1", 0f);
    }
    public void GoToLevel2()
    {
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().StopMusic();
        Invoke("LoadLevel2", 0f);
    }
    public void GoToLevel3()
    {
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().StopMusic();
        Invoke("LoadLevel3", 0f);
    }
    void LoadLevel0()
    {
        SceneManager.LoadScene("Chapter0");
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().MainMenutoGameBGM(0);
        Time.timeScale = 1;
    }
    void LoadLevel1()
    {
        SceneManager.LoadScene("Chapter1");
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().MainMenutoGameBGM(1);
        Time.timeScale = 1;
    }
    void LoadLevel2()
    {
        SceneManager.LoadScene("Chapter2");
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().MainMenutoGameBGM(2);
        Time.timeScale = 1;
    }
    void LoadLevel3()
    {
        SceneManager.LoadScene("Chapter3");
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().MainMenutoGameBGM(3);
        Time.timeScale = 1;
    }
}
