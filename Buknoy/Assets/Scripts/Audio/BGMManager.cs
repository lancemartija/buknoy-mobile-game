using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource BGM;
    private float fadeTime = 0f;
    public AudioClip mainmenuBGM,
        menuhubBGM,
        quizmenuBGM,
        quizgameBGM,
        notesBGM,
        chapter0BGM,
        chapter1BGM,
        chapter2BGM,
        chapter3BGM;
    private static BGMManager instance = null;
    public static BGMManager Instance
    {
        get { return instance; }
    }

    void Start()
    {
        BGM.clip = mainmenuBGM;

        PlayerPrefs.SetFloat("musicVolumeSlider", 0.5f);
        PlayerPrefs.SetFloat("soundVolumeSlider", 0.75f);
        PlayerPrefs.SetInt("musicVolumeToggle", 1);
        PlayerPrefs.SetInt("soundVolumeToggle", 1);

        if (PlayerPrefs.HasKey("musicVolumeSlider"))
        {
            BGM.volume = PlayerPrefs.GetFloat("musicVolumeSlider");
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        BGM = GetComponent<AudioSource>();
    }

    //Default Triggers
    public void PlayMusic()
    {
        StartCoroutine(FadeIn(BGM, fadeTime));
    }

    public void StopMusic()
    {
        StartCoroutine(FadeOut(BGM, fadeTime));
    }

    public void StopMusicNow()
    {
        if (BGM.isPlaying)
        {
            BGM.UnPause();
        }
        BGM.Stop();
    }

    //Switch BGM
    public void MainMenutoQuizMenuBGM()
    {
        BGM.clip = quizmenuBGM;
        PlayMusic();
    }

    public void MainMenutoNoteMenuBGM()
    {
        BGM.clip = notesBGM;
        PlayMusic();
    }

    public void BacktoMenuHubBGM()
    {
        BGM.clip = menuhubBGM;
        PlayMusic();
    }

    public void BacktoMainMenuBGM()
    {
        BGM.clip = mainmenuBGM;
        PlayMusic();
    }

    public void QuizMenutoQuizGameBGM()
    {
        BGM.clip = quizgameBGM;
        PlayMusic();
    }

    public void MainMenutoGameBGM(int chapterno)
    {
        switch (chapterno)
        {
            case 0:
                BGM.clip = chapter0BGM;
                break;
            case 1:
                BGM.clip = chapter1BGM;
                break;
            case 2:
                BGM.clip = chapter2BGM;
                break;
            case 3:
                BGM.clip = chapter3BGM;
                break;
        }
        PlayMusic();
    }

    //Pause BGM
    public void PauseBGM()
    {
        BGM.Pause();
    }

    public void UnPauseBGM()
    {
        BGM.UnPause();
        if (!BGM.isPlaying)
        {
            BGM.Play();
        }
    }

    //Fade In and Fade Out BGM
    public static IEnumerator FadeOut(AudioSource BGM, float FadeTime)
    {
        float startVolume = BGM.volume;
        while (BGM.volume > 0)
        {
            BGM.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        BGM.Stop();
    }

    public static IEnumerator FadeIn(AudioSource BGM, float FadeTime)
    {
        BGM.Play();
        BGM.volume = 0f;
        while (BGM.volume < PlayerPrefs.GetFloat("musicVolumeSlider"))
        {
            BGM.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }
}
