using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizAudio : MonoBehaviour
{

    [SerializeField] private Text musicText, soundText;


    public QuizManager quizmanager;

    public AudioSource correctSound, incorrectSound, gameover, setBGM;

    public AudioClip menuBGM, gameBGM;

    public bool musicEnabled = true, soundEnabled = true;

    public int musicVolume, soundVolume;

    private float fadeTime = 2f;

    public Text MusicText {get {return musicText;}}
    public Text SoundText {get {return soundText;}}

    void Start()
    {
        setBGM.clip = menuBGM;
        setBGM.Play();

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolume = PlayerPrefs.GetInt("musicVolume");
        }

        if (PlayerPrefs.HasKey("soundVolume"))
        {
            soundVolume = PlayerPrefs.GetInt("soundVolume");
        }

    }
    //Play Sounds
    public void GameOverSound()
    {
        setBGM.Stop();
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

    public void MenutoGameBGM()
    {
        setBGM.clip = gameBGM;
        if (musicVolume == 1)
        {
            StartCoroutine(FadeIn(setBGM, fadeTime));
        }
    }
    public void BacktoMenuBGM()
    {
        setBGM.clip = menuBGM;
        if (musicVolume == 1)
        {
            StartCoroutine(FadeIn(setBGM, fadeTime));
        }
    }
    //Pause BGM
    public void PauseBGM()
    {
        setBGM.Pause();
    }
    public void UnPauseBGM()
    {
        setBGM.UnPause();
        if (!setBGM.isPlaying)
        {
            setBGM.Play();
        }
    }

    //Enable/Disable Music and Sound Effects

    public void ToggleMusic()
    {
        if (musicEnabled == true)
        {
            musicEnabled = false;
            MusicText.text = "Music: OFF";
            setBGM.volume = 0;
            musicVolume = 0;
        }
        else
        {
            musicEnabled = true;
            MusicText.text = "Music: ON";
            setBGM.volume = 1;
            musicVolume = 1;
        }

    }

    public void ToggleSound()
    {
        if (soundEnabled == true)
        {
            soundEnabled = false;
            SoundText.text = "Sound: OFF";
            correctSound.volume = 0;
            incorrectSound.volume = 0;
            gameover.volume = 0;
            soundVolume = 0;
        }
        else
        {
            soundEnabled = true;
            SoundText.text = "Sound: ON";
            correctSound.volume = 1;
            incorrectSound.volume = 1;
            gameover.volume = 1;
            soundVolume = 1;
        }

    }


    //Save and Load Settings
    public void SaveAudioSettings()
    {
        PlayerPrefs.SetInt("musicVolume", musicVolume);
        PlayerPrefs.SetInt("soundVolume", soundVolume);
        PlayerPrefs.Save();
    }

    public void LoadAudioSettings()
    {
        
        musicVolume = PlayerPrefs.GetInt("musicVolume", 1);
        soundVolume = PlayerPrefs.GetInt("soundVolume", 1);


        if (musicVolume == 1)
        {
            musicEnabled = true;
            MusicText.text = "Music: ON";
            setBGM.volume = 1;
        }
        else
        {
            musicEnabled = false;
            MusicText.text = "Music: OFF";
            setBGM.volume = 0;
        }

        if (soundVolume == 1)
        {
            soundEnabled = true;
            SoundText.text = "Sound: ON";
            correctSound.volume = 1;
            incorrectSound.volume = 1;
            gameover.volume = 1;
        }
        else
        {
            soundEnabled = false;
            SoundText.text = "Sound: OFF";
            correctSound.volume = 0;
            incorrectSound.volume = 0;
            gameover.volume = 0;
        }

    }


    //Fade In and Fade Out BGM
    public static IEnumerator FadeOut(AudioSource setBGM, float FadeTime) 
    {
		float startVolume = setBGM.volume;
		while (setBGM.volume > 0) 
        {
			setBGM.volume -= startVolume * Time.deltaTime / FadeTime;
			yield return null;
		}
		setBGM.Stop();
	}

	public static IEnumerator FadeIn(AudioSource setBGM, float FadeTime) 
    {
		setBGM.Play();
		setBGM.volume = 0f;
		while (setBGM.volume < 1) 
        {
			setBGM.volume += Time.deltaTime / FadeTime;
			yield return null;
		}
	}

}
