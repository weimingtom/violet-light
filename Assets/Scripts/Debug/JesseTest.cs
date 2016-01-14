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

    }

}
