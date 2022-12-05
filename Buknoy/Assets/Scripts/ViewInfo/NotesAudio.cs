using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesAudio : MonoBehaviour
{
    public AudioSource setBGM;

    private float fadeTime = 2f;


    void Start()
    {
        setBGM.Play();
    }

    public void EndMusic()
    {
        StartCoroutine(FadeOut(setBGM, fadeTime));
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
