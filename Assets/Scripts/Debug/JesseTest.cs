using UnityEngine;
using System.Collections;

public class JesseTest : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
        SceneManager.Instance.LoadCase( 1 );
        SceneManager.Instance.ChangeScene( 2 );

    }

    void Update()
    {
        if( Input.GetKeyDown( KeyCode.F1 ) )
        {
            FXManager.Instance.Spawn("ScreenShake");
        }
        if( Input.GetKeyDown( KeyCode.F2 ) )
        {
            FXManager.Instance.Spawn( "ScreenFlash" );
        }

        if( Input.GetKeyDown( KeyCode.F3 ) )
        {
            FXManager.Instance.Spawn( "ScreenShake" ); 
            FXManager.Instance.Spawn( "ScreenFlash" );
            FXManager.Instance.Spawn( "SoundEffect" );          
        }
    }

}
