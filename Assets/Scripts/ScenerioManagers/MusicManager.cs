using UnityEngine;
using UnityEngine.UI;
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
    public Slider mySlider;
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
            MusicLibrary.Add(Boththings[0].ToLower(), Resources.Load(Boththings[1]) as AudioClip);
            Debug.Log("[Music Manager] Added Song " + Boththings[0].ToLower() + " - " + Boththings[1]);
        }
    }
    
	public void ChangeSong(string newSong)
    {
        
        if (MusicLibrary.ContainsKey(newSong.ToLower()))
        {
            if (MusicLibrary[newSong.ToLower()] == audioSource.clip)
            {
                Debug.Log("[Music Manager] SONG ALREADY PLAYING");

            }
            else
            {
                Debug.Log("[Music Manager] CHANGING SONG TO:" + newSong);

                audioSource.clip = MusicLibrary[newSong.ToLower()];
                audioSource.Play();
            }

        }
        else
        {
            audioSource.clip = null;
            audioSource.Stop();

            Debug.Log("[Music Manager does  not contain song:" + newSong);
        }
    }

    public void OnReset()
    {
        float maxVolume = 1.0f;
        mySlider.value = maxVolume;
        audioSource.volume = maxVolume;
    }

    public void SetVolume( float volume )
    {
        audioSource.volume = volume;
    }

}