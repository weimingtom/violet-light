using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour
{
	static public AudioPlayer instance;
	AudioSource audioSource;
	// Use this for initialization
	void Awake () 
    {
		instance = this;
		audioSource = GetComponent<AudioSource>();
	}
	public void SetAudio(AudioClip clip)
	{
		audioSource.clip = clip;
	}
	public void Play()
	{
		audioSource.Play();
	}

}
