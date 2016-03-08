using UnityEngine;
using System.Collections;

[RequireComponent( typeof( AudioSource ) )]
public class SoundEffect : MonoBehaviour {

    private AudioSource audioSource;
	void OnEnable() 
    {
        audioSource = GetComponent<AudioSource>();
        string clipName = gameObject.name.ToString();
        string isClone = clipName.Substring( clipName.Length - 7 );
        if(isClone == "(Clone)")
            audioSource.clip = Resources.Load( "Audio/SFX/" + gameObject.name.ToString().Substring( 0, gameObject.name.ToString().Length - 7 ) ) as AudioClip;
        else
            audioSource.clip = Resources.Load( "Audio/SFX/" + clipName ) as AudioClip;
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
