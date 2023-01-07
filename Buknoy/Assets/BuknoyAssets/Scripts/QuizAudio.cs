using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizAudio : MonoBehaviour
{

    [SerializeField] private Text music2Text, sound2Text;

    [SerializeField] private float loadingtime = 0f;


    public QuizManager quizmanager;

    public AudioSource correctSound, incorrectSound, buttonSound, gameover;


    public int musicVolumeToggle, soundVolumeToggle;

    public bool musicEnabled = true, soundEnabled = true;



    public Text Music2Text {get {return music2Text;}}
    public Text Sound2Text {get {return sound2Text;}}

    void Start()
    {
        StartCoroutine(LoadSFXSettings());
    }

    //Play Sounds
    public void GameOverSound()
    {
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().StopMusicNow();
        gameover.Play();
    }

    public void CorrectSound()
    {
        correctSound.Play();
    }

    public void IncorrectSound()
    {
        incorrectSound.Play();
    }

    //Toggle Music and Sound Effects

    public void ToggleMusic()
    {
        if (musicEnabled == true)
        {
            musicEnabled = false;
            Music2Text.text = "Music: OFF";
             GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().BGM.volume = 0;
            musicVolumeToggle = 0;
        }
        else
        {
            musicEnabled = true;
            Music2Text.text = "Music: ON";
             GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().BGM.volume = 1;
            musicVolumeToggle = 1;
        }

    }

    public void ToggleSound()
    {
        if (soundEnabled == true)
        {
            soundEnabled = false;
            Sound2Text.text = "Sound: OFF";
            correctSound.volume = 0;
            incorrectSound.volume = 0;
            buttonSound.volume = 0;
            gameover.volume = 0;
            soundVolumeToggle = 0;
        }
        else
        {
            soundEnabled = true;
            Sound2Text.text = "Sound: ON";
            correctSound.volume = 1;
            incorrectSound.volume = 1;
            buttonSound.volume = 1;
            gameover.volume = 1;
            soundVolumeToggle = 1;
        }

    }

    IEnumerator  LoadSFXSettings()
    {
        yield return new WaitForSeconds(loadingtime);
        float sfxVolume = PlayerPrefs.GetFloat("soundVolumeSlider");
        musicVolumeToggle = PlayerPrefs.GetInt("musicVolumeToggle");
        soundVolumeToggle = PlayerPrefs.GetInt("soundVolumeToggle");
        correctSound.volume = sfxVolume;
        incorrectSound.volume = sfxVolume;
        buttonSound.volume = sfxVolume;
        gameover.volume = sfxVolume;
    }

}
