using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Text musicText, soundText, sliderText1, sliderText2;

    public bool musicEnabled = true, soundEnabled = true;

    public AudioSource musiccheck, soundcheck;

    public int musicVolumeToggle, soundVolumeToggle;

    public Slider slider1, slider2;

    public float slider1Value, slider2Value, musicVolume, soundVolume;

    public Text MusicText {get {return musicText;}}
    public Text SoundText {get {return soundText;}}
    public Text SliderText {get {return sliderText1;}}
    public Text Slider2Text {get {return sliderText2;}}

    // Start is called before the first frame update
    void Start()
    {
      slider1.value = PlayerPrefs.GetFloat("musicVolumeSlider");
      slider2.value = PlayerPrefs.GetFloat("soundVolumeSlider");

      if (PlayerPrefs.HasKey("musicVolumeToggle"))
      {
        musicVolumeToggle = PlayerPrefs.GetInt("musicVolumeToggle");
      }

      if (PlayerPrefs.HasKey("soundVolumeToggle"))
      {
        soundVolumeToggle = PlayerPrefs.GetInt("soundVolumeToggle");
      }
    }

    //Toggle Music and Sound Effects

    public void ToggleMusic()
    {
        if (musicEnabled == true)
        {
            slider1.value = 0;
            slider1Value = slider1.value;
            musicEnabled = false;
            MusicText.text = "Music: OFF";    
            GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().BGM.volume = slider1Value;
            musiccheck.volume = slider1Value;
            musicVolumeToggle = 0;
            sliderText1.text = Math.Round(slider1Value, 2) * 100 + " %";
            PlayerPrefs.SetFloat("musicVolumeSlider", slider1Value);
        }
        else
        {
            slider1.value = 1;
            slider1Value = slider1.value;
            musicEnabled = true;
            MusicText.text = "Music: ON";
            GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().BGM.volume = slider1Value;
            musiccheck.volume = slider1Value;
            musicVolumeToggle = 1;
            sliderText1.text = Math.Round(slider1Value, 2) * 100 + " %";
            PlayerPrefs.SetFloat("musicVolumeSlider", slider1Value);
        }

    }

    public void ToggleSound()
    {
        if (soundEnabled == true)
        {
            slider2.value = 0;
            slider2Value = slider2.value;
            soundEnabled = false;
            SoundText.text = "Sound: OFF";
            soundcheck.volume = slider2Value;
            soundVolumeToggle = 0;
            sliderText2.text = Math.Round(slider2Value, 2) * 100 + " %";
            PlayerPrefs.SetFloat("soundVolumeSlider", slider2Value);
        }
        else
        {
            slider2.value = 1;
            slider2Value = slider2.value;
            soundEnabled = true;
            SoundText.text = "Sound: ON";
            soundcheck.volume = slider2Value;
            soundVolumeToggle = 1;
            sliderText2.text = Math.Round(slider2Value, 2) * 100 + " %";
            PlayerPrefs.SetFloat("soundVolumeSlider", slider2Value);
        }

    }

    //Slider Settings

    public void SliderMusicVolume()
    {
        slider1Value = slider1.value;
        GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().BGM.volume = slider1Value;
        musiccheck.volume = slider1Value;
        if (slider1Value == 0)
        {
            MusicText.text = "Music: OFF";    
        }
        else
        {
             MusicText.text = "Music: ON"; 
        }
        sliderText1.text = Math.Round(slider1Value, 2) * 100 + " %";
        PlayerPrefs.SetFloat("musicVolumeSlider", slider1Value);
    }

    public void SliderSoundVolume()
    {
        slider2Value = slider2.value;
        soundcheck.volume = slider2Value;
        if (slider2Value == 0)
        {
            SoundText.text = "Sound: OFF";    
        }
        else
        {
            SoundText.text = "Sound: ON"; 
        }
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
        
        musicVolume = PlayerPrefs.GetFloat("musicVolumeSlider", 1);


        if (musicVolumeToggle == 1)
        {
            musicEnabled = true;
            GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().BGM.volume = musicVolume;
        }
        else
        {
            musicEnabled = false;
            GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().BGM.volume = musicVolume;
        }

    }

    public void GoToMenuHub()
    {
        SaveAudioSettings();
        Invoke("LoadMenuHub", 2f);
    }
    //Load other Scenes after 2 seconds
    void LoadMenuHub()
    {
        SceneManager.LoadScene("MenuHub");
        LoadAudioSettings();
    }
}
