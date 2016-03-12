using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioPlayer : MonoBehaviour
{
	static public AudioPlayer instance;
    public AudioClip maleBlip;
    private AudioSource audioSource;
    public AudioClip femaleBlip;
    public Slider mySlider;
	// Use this for initialization
	void Awake () 
    {
		instance = this;
		audioSource = GetComponent<AudioSource>();
	}

    public void PlayBlip(bool female)
    {
        audioSource.clip = maleBlip;
        if(female)
        {
            audioSource.clip = femaleBlip;
        }
        audioSource.Play();
    }

    public void OnReset()
    {
        float maxVolume = 1.0f;
        mySlider.value = maxVolume;
        audioSource.volume = maxVolume;
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        
    }

}
