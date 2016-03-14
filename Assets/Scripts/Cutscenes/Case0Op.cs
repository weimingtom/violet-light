using UnityEngine;
using System.Collections;

public class Case0Op : MonoBehaviour 
{
    public float panDelay;
    public float bodyPanDelay;
    public string opScene = "0_midnight_intro";
    private bool[] doneOnce;
    private bool ending = false;

	void Start() 
    {
        MusicManager.instance.ChangeSong("alt_theme");
        doneOnce = new bool[2];
        MenuManager.instance.ToggleMenuAccess();
        FileReader.Instance.LoadScene( opScene );
        SceneManager.Instance.SetCanSkip( false );
        SceneManager.Instance.SetCanControl( false );
        FadeOutScreen.instance.BeginFade( -1, 0.1f );
        panDelay += Time.time;
    }
	
	void Update () 
    {
	    if (Time.time > panDelay && !doneOnce[0])
        {
            GetComponent<PanDown>().StartPan();
            doneOnce[0] = true;
        }
        else if( Time.time > bodyPanDelay && !doneOnce[1] )
        {
            //GetComponentInChildren<Case0Body>().StartPan();
            doneOnce[1] = true;
        }
        else if( doneOnce[1] && !CommandManager.Instance.myBannerBox.activeInHierarchy && !ending )
        {
            End();
        }
        else if(ending)
        {
            if( FadeOutScreen.instance.GetFadedIn() )
            {
                MenuManager.instance.ToggleMenuAccess( true );
                SceneManager.Instance.ChangeScene( 1 );
                SceneManager.Instance.SetCanSkip( true );
                SceneManager.Instance.SetCanControl( true );

                Destroy( this.gameObject );
            }
        }
	}

    public void End()
    {
        FadeOutScreen.instance.BeginFade( 1, 0.05f );
        ending = true;
    }

    public void Skip()
    {
        MenuManager.instance.ToggleMenuAccess( true );
        SceneManager.Instance.ChangeScene( 1 );
        SceneManager.Instance.SetCanSkip( true );
        SceneManager.Instance.SetCanControl( true );

        Destroy( this.gameObject );
    }
}
