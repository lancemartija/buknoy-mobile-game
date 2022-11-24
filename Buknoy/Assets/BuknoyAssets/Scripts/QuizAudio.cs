using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizAudio : MonoBehaviour
{
    public QuizManager quizmanager;

    public AudioSource correctSound, incorrectSound, gameover, setBGM;

    public AudioClip menuBGM, gameBGM;

    private float fadeTime = 2f;

    void Start()
    {
        setBGM.clip = menuBGM;
        setBGM.Play();
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
        StartCoroutine(FadeOut(setBGM, fadeTime));
        setBGM.clip = gameBGM;
        StartCoroutine(FadeIn(setBGM, fadeTime));
    }
    public void BacktoMenuBGM()
    {
        StartCoroutine(FadeOut(setBGM, fadeTime));
        setBGM.clip = menuBGM;
        StartCoroutine(FadeIn(setBGM, fadeTime));
    }
    //Pause BGM
    public void PauseBGM()
    {
        setBGM.Pause();
    }
    public void UnPauseBGM()
    {
        setBGM.UnPause();
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
