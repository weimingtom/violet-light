using UnityEngine;
using System.Collections;

[RequireComponent( typeof( AudioSource ) )]
public class SoundEffect : MonoBehaviour {

    private AudioSource audioSource;
	void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load( "Audio/SFX/" + gameObject.name.ToString().Substring( 0, gameObject.name.ToString().Length - 7 ) ) as AudioClip;
        audioSource.Play();
	}

    void Update()
    {
        if(audioSource.isPlaying == false)
        {
            Destroy( this.gameObject );
        }
    }
	
}
