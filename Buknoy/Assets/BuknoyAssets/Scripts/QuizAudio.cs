using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizAudio : MonoBehaviour
{

    [SerializeField] private Text musicText, soundText, music2Text, sound2Text, sliderText1, sliderText2;


    public QuizManager quizmanager;

    public AudioSource correctSound, incorrectSound, buttonSound, gameover, setBGM;

    public AudioClip menuBGM, gameBGM;

    public Slider slider1, slider2;

    public bool musicEnabled = true, soundEnabled = true;

    public int musicVolumeToggle, soundVolumeToggle;

    private float fadeTime = 2f;

    public Text MusicText {get {return musicText;}}
    public Text Music2Text {get {return music2Text;}}
    public Text SoundText {get {return soundText;}}
    public Text Sound2Text {get {return sound2Text;}}
    public Text SliderText {get {return sliderText1;}}
    public Text Slider2Text {get {return sliderText2;}}

    void Start()
    {
        setBGM.clip = menuBGM;
        setBGM.Play();

        slider1.value = PlayerPrefs.GetFloat("musicVolumeSlider", 0.75f);
        slider2.value = PlayerPrefs.GetFloat("soundVolumeSlider", 0.75f);

        if (PlayerPrefs.HasKey("musicVolumeToggle"))
        {
            musicVolumeToggle = PlayerPrefs.GetInt("musicVolumeToggle");
        }

        if (PlayerPrefs.HasKey("soundVolumeToggle"))
        {
            soundVolumeToggle = PlayerPrefs.GetInt("soundVolumeToggle");
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
        if (musicVolumeToggle == 1)
        {
            StartCoroutine(FadeIn(setBGM, fadeTime));
        }
    }
    public void BacktoMenuBGM()
    {
        setBGM.clip = menuBGM;
        if (musicVolumeToggle == 1)
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
    //Fade Out when switching back to Menu Hub
    public void EndMusic()
    {
        StartCoroutine(FadeOut(setBGM, fadeTime));
    }


    //Toggle Music and Sound Effects

    public void ToggleMusic()
    {
        if (musicEnabled == true)
        {
            musicEnabled = false;
            MusicText.text = "Music: OFF";
            Music2Text.text = "Music: OFF";
            setBGM.volume = 0;
            musicVolumeToggle = 0;
        }
        else
        {
            musicEnabled = true;
            MusicText.text = "Music: ON";
            Music2Text.text = "Music: ON";
            setBGM.volume = 1;
            musicVolumeToggle = 1;
        }

    }

    public void ToggleSound()
    {
        if (soundEnabled == true)
        {
            soundEnabled = false;
            SoundText.text = "Sound: OFF";
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
            SoundText.text = "Sound: ON";
            Sound2Text.text = "Soundc: ON";
            correctSound.volume = 1;
            incorrectSound.volume = 1;
            buttonSound.volume = 1;
            gameover.volume = 1;
            soundVolumeToggle = 1;
        }

    }

    //Slider Settings

    public void SliderMusicVolume()
    {
        float slider1Value = slider1.value;
        setBGM.volume = slider1Value;
        sliderText1.text = Math.Round(slider1Value, 2) * 100 + " %";
        PlayerPrefs.SetFloat("musicVolumeSlider", slider1Value);
    }

    public void SliderSoundVolume()
    {
        float slider2Value = slider2.value;
        correctSound.volume = slider2Value;
        incorrectSound.volume = slider2Value;
        buttonSound.volume = slider2Value;
        gameover.volume = slider2Value;
        sliderText2.text = Math.Round(slider2Value, 2) * 100 + " %";
        PlayerPrefs.SetFloat("soundVolumeSlider", slider2Value);
    }

    //Save and Load Settings
    public void SaveAudioSettings()
    {
        PlayerPrefs.SetInt("musicVolumeToggle", musicVolumeToggle);
        PlayerPrefs.SetInt("soundVolumeToggle", soundVolumeToggle);
        PlayerPrefs.Save();
    }

    public void LoadAudioSettings()
    {
        
        musicVolumeToggle = PlayerPrefs.GetInt("musicVolumeToggle", 1);
        soundVolumeToggle = PlayerPrefs.GetInt("soundVolumeToggle", 1);


        if (musicVolumeToggle == 1)
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

        if (soundVolumeToggle == 1)
        {
            soundEnabled = true;
            SoundText.text = "Sound: ON";
            correctSound.volume = 1;
            incorrectSound.volume = 1;
            gameover.volume = 1;
            buttonSound.volume = 1;
        }
        else
        {
            soundEnabled = false;
            SoundText.text = "Sound: OFF";
            correctSound.volume = 0;
            incorrectSound.volume = 0;
            gameover.volume = 0;
            buttonSound.volume = 0;
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
