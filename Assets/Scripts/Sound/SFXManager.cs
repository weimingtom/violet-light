using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour 
{
    static public SFXManager instance;
    private AudioSource audioSource;
    public AudioClip acceptClip;
    public AudioClip cancelClip;
    public AudioClip pageClip;
    public AudioClip saveClip;
    public AudioClip sceneChangeClip;

	// Use this for initialization
    void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayAccept()
    {
        audioSource.clip = acceptClip;
        audioSource.Play();
    }
    public void PlayCancel()
    {
        audioSource.clip = cancelClip;
        audioSource.Play();
    }
    public void PlayPage()
    {
        audioSource.clip = pageClip;
        audioSource.Play();
    }
    public void PlaySave()
    {
        audioSource.clip = saveClip;
        audioSource.Play();
    }
    public void PlaySceneChange()
    {
        audioSource.clip = sceneChangeClip;
        audioSource.Play();
    }
}
