using UnityEngine;
using System.Collections;

public class Case0Op : MonoBehaviour 
{
    public float panDelay;
    public float bodyPanDelay;
    public string opScene = "0_midnight_intro";
    private bool[] doneOnce;

	// Use this for initialization
	void Awake() 
    {
        MusicManager.instance.ChangeSong("alt_theme");
        doneOnce = new bool[2];
        MenuManager.instance.ToggleMenuAccess();
        FileReader.Instance.LoadScene( opScene );
        SceneManager.Instance.SetCanSkip(false);
        FadeOutScreen.instance.BeginFade(-1);

        panDelay += Time.time;
    }
	
	// Update is called once per frame
	void Update () 
    {
	    if (Time.time > panDelay && !doneOnce[0])
        {
            GetComponent<PanDown>().StartPan();
            doneOnce[0] = true;
        }
        else if( Time.time > bodyPanDelay && !doneOnce[1] )
        {
            GetComponentInChildren<Case0Body>().StartPan();
            doneOnce[1] = true;
        }
        else if(doneOnce[1] && FadeOutScreen.instance.GetFadedIn())
        {
            End();
        }
	}

    public void End()
    {
        MenuManager.instance.ToggleMenuAccess(true);
        SceneManager.Instance.ChangeScene( 1 );
        SceneManager.Instance.SetCanSkip(true);
        Destroy( this.gameObject );


    }

}
