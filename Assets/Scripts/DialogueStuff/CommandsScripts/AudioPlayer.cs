using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour
{
	static public AudioPlayer instance;
    public AudioClip maleBlip;
    public AudioClip femaleBlip;
    private AudioSource audioSource;

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

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

}
