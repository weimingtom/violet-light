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
	private Vector2 hiddenPos;
	private Vector2 ActivePos;
	private bool active;
	private state myState;

	void Start () 
    {

		//TODO Figure out How to do this without magic numbers.
        hiddenPos = new Vector2(0.0f, Screen.height * 3);
        ActivePos = new Vector2( 0.0f, Screen.height - 30.0f); 
        menu.transform.localPosition = hiddenPos;
        active = false;
		myState = state.Main;
    }
	

	void Update () 
    {
	    //check if menu button was pressed
        if(Input.GetKeyDown(KeyCode.Escape))
        {
			toggleMenu();
		}
	}

	public bool GetMenuActive()
	{
		return active;
	}

	private void toggleMenu()
	{
		active = !active;
		if(active)
			OpenMenu();
		else
			CloseMenu();
	}

	//Get When a button is pressed
	public void ButtonPressed(string btn)
	{
		if(btn == "btnMenu")
		{
			toggleMenu();
			return;
		}

		if(active)
		{
			//Not yet implemented into menu UI
			//Check Tabs
			if(btn[0] == 't' && btn[1] == 'a' && btn[2] == 'b')
			switch(btn)
			{
			case "tabEvidence":
				ChangeState(state.Evience);
				break;
			case "tabNotes":
				ChangeState(state.Notes);
				break;
			case "tabOptions":
				ChangeState(state.Options);
				break;
			case "tabPuzzles":
				ChangeState(state.Puzzles);
				break;
			case "tabSuspects":
				ChangeState(state.Suspects);
				break;
			case "tabTravel":
				ChangeState(state.Travel);
				break;
			default:
				Debug.Log("ERROR: Button Marked as 'tab' But Was Not Found In Tab Switch List!" + btn);
				break;
			}

			//btn wasn't a Tab button so 
			switch(myState)
			{
			case state.Main:
				MainButton(btn);
				break;
			case state.Evience:

				break;
			case state.Notes:

				break;
			case state.Options:

				break;
			case state.Puzzles:

				break;
			case state.Suspects:

				break;
			case state.Travel:

				break;
			default:
				Debug.Log("ERROR: myState Set To Unlisted State... How the F did you do that??? " + btn);
				break;
			}




		}

	}

	private void MainButton(string btn)
	{
		switch(btn)
		{
		case "btnContinue":
			toggleMenu();
			break;
		

		default:
			Debug.Log("ERROR: Button Not Found In List, " + btn);
			break;
		}

	}

	private void ChangeState(state newState)
	{
		//do fancy transistion animation between states

		//set our new state
		myState = newState;

	}

    private void OpenMenu()
    {
		//Do fancy transition animation
        menu.transform.localPosition = ActivePos;
		//spawn input eater
		SceneManager.Instance.SetInputBlocker(true);
    }

    private void CloseMenu()
    {
		//do fancy transition in reverse
        menu.transform.localPosition = hiddenPos;
		//delte input eater
		SceneManager.Instance.SetInputBlocker(false);
		//set menu back to default state
		myState = state.Main;
    }
}

