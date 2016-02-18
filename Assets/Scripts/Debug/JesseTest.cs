using UnityEngine;
using System.Collections;

public class JesseTest : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
    }

    void Update()
    {
        if( Input.GetKeyDown( KeyCode.F1 ) )
        {
            FileReader.Instance.LoadScene( "0_inn_intro" );
            FXManager.Instance.Spawn("ScreenShake");
        }
        else if( Input.GetKeyDown( KeyCode.F2 ) )
        {
            FXManager.Instance.Spawn( "ScreenFlash" );
        }
        else if( Input.GetKeyDown( KeyCode.F3 ) )
        {
            FXManager.Instance.Spawn( "ScreenShake" ); 
            FXManager.Instance.Spawn( "ScreenFlash" );
            FXManager.Instance.Spawn( "SoundEffect" );          
        }
        else if( Input.GetKeyDown( KeyCode.F4 ) )
        {
            MusicManager.instance.ChangeSong( "caught" );
        }
        if(Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.Instance.ChangeScene(3);
            MusicManager.instance.ChangeSong("theme");
        }
    }
}
