using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent( typeof( AudioSource ) )]
public class MusicManager : MonoBehaviour 
{
    public string MusicLibraryFileName = "Audio/Scripts/AudioMasterList";
    private string[] MusicLibraryFile;
    private AudioSource audioSource;
    static public MusicManager instance;

    private Dictionary<string, AudioClip> MusicLibrary;

    void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        MusicLibrary = new Dictionary<string, AudioClip>();
        audioSource.loop = true; 
    
        // TODO(jesse): Load this all up in a loading screen?
        char[] delimiter = { '\r', '\n' };
        MusicLibraryFile = (Resources.Load( MusicLibraryFileName ) as TextAsset).ToString().Split( delimiter, System.StringSplitOptions.RemoveEmptyEntries);

        foreach(string CurrentString in MusicLibraryFile)
        {
            string[] Boththings = CurrentString.Split(' ');
            MusicLibrary.Add( Boththings[0], Resources.Load(Boththings[1]) as AudioClip );
            Debug.Log( "[Music Manager] Added Song " + Boththings[0] + " - " + Boththings[1] );
        }
    }
    
	public void ChangeSong(string newSong)
    {
        audioSource.clip = MusicLibrary[newSong];
        audioSource.Play();
    }

    public void SetVolume( float volume )
    {
        audioSource.volume = volume;
    }

}