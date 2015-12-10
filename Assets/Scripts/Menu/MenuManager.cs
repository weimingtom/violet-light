using UnityEngine;
using System.Collections;


public class MenuManager : MonoBehaviour {

   

	private enum state
	{
		Main,
		Evience,
		Notes,
		Travel,
		Suspects,
		Puzzles,
		Options
	}

	public GameObject menu;
	private bool active;
	private state myState;
    public GameObject[] States;

	void Start () 
    {
        menu.SetActive(false);
        active = false;
		myState = state.Main;
    }
	

	void Update () 
    {
	    //check if open/close menu button was pressed
        if(Input.GetKeyDown(KeyCode.Escape))
        {
			toggleMenu();
		}
	}

	public bool GetMenuActive()
	{
		return active;
	}

	public void toggleMenu()
	{
		active = !active;
		if(active)
			OpenMenu();
		else
			CloseMenu();
	}

    //Get When a Tab button is pressed
    public void TabPressed( string btn )
    { 
         switch(btn)
	    {
	    case "btnEvidence":
		    ChangeState(state.Evience);
		    break;
	    case "btnNotes":
		    ChangeState(state.Notes);
		    break;
	    case "btnOptions":
		    ChangeState(state.Options);
		    break;
	    case "btnPuzzles":
		    ChangeState(state.Puzzles);
		    break;
	    case "btnSuspects":
		    ChangeState(state.Suspects);
		    break;
	    case "btnTravel":
		    ChangeState(state.Travel);
		    break;
	    default:
		    Debug.Log("ERROR: Button Marked as 'tab' But Was Not Found In Tab Switch List!" + btn);
		    break;
	    }
    
    }

	public void MainButtonPressed(string btn)
	{
		switch(btn)
		{
		case "btnContinue":
			toggleMenu();
			break;
        case "btnQuit":
            Debug.Log( "QUIT" );
            Application.Quit();
            break;
        case "btnSave":
            break;
		default:
			Debug.Log("ERROR: Button Not Found In List, " + btn);
			break;
		}

	}

	private void ChangeState(state newState)
	{
		//do fancy transistion animation between states
        States[(int)myState].SetActive( false );
		//set our new state
        States[(int)newState].SetActive( true );
		myState = newState;

	}

    private void OpenMenu()
    {
		//Do fancy transition animation
        menu.SetActive(true);
		//spawn input eater
		SceneManager.Instance.SetInputBlocker(true);
    }

    private void CloseMenu()
    {
		//do fancy transition in reverse

        //set menu back to default state
        ChangeState(state.Main);
        menu.SetActive(false);
		//delte input eater
		SceneManager.Instance.SetInputBlocker(false);
		
		
    }

    public void VolumeSlider( float multiplier )
    {
        Debug.Log( "Volume Changed: " + multiplier );
    }

    public void BrightnessSlider( float multiplier )
    {
        Debug.Log( "Brightness Changed: " + multiplier );
    }


}


