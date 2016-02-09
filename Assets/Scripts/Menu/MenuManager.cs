using UnityEngine;
using System.Collections;


public class MenuManager : MonoBehaviour
{

	private enum state
	{
        SaveLoad,
        Evidence,
        end
	}

    public static MenuManager instance;
	public GameObject menu;
	private bool active;
	private state myState;
    //animation purposes
    public GameObject[] States;
    public GameObject[] StatesButton;
    public Vector3[] menuUIOriginalPos;
    public Vector3[] btnUIOriginalPos;
    public float offset = 100.0f;
    private bool moveRight = true;
    public float speed = 1.0f;
    private bool animateMenu = false;
    private state animateState = state.SaveLoad;

    void Awake()
    {
        instance = this;
    }

	void Start () 
    {
        menu.SetActive(false);
        active = false;
        myState = state.Evidence;
        InitializeUI();
    }
    void InitializeUI()
    {
        if( States.Length != StatesButton.Length )
        {
            Debug.Log( "menu ui need to have btn come with it" );
            Debug.Break();
        }
        else
        {
            menuUIOriginalPos = new Vector3[States.Length];
            btnUIOriginalPos = new Vector3[StatesButton.Length];
            for( int i = 0; i < menuUIOriginalPos.Length; i++ )
            {
                menuUIOriginalPos[i] = States[i].transform.position;
                btnUIOriginalPos[i] = StatesButton[i].transform.position;
            }
        }
    }

	void Update () 
    {
	    //check if open/close menu button was pressed
        if(Input.GetKeyDown(KeyCode.Escape))
        {
			toggleMenu();
		}
        //if( animateMenu == true )
        //{
        //    AnimateMenu();
        //}
	}

	public bool GetMenuActive()
	{
		return active;
	}

	public void toggleMenu()
	{
        if( active && myState != state.SaveLoad )
        {
            ChangeState( state.SaveLoad );
        }
        else if (!active)
        {
			OpenMenu();
            active = true;
        }
		else
        {
            active = false;
			CloseMenu();
        }
	}

    //Get When a Tab button is pressed
    public void TabPressed( string btn )
    {
       
        // TODO(jesse): THis is a quick hack to ensure that you don't travel during dialogue
        if (!CommandManager.Instance.myBannerBox.activeInHierarchy)
        {
            //animateMenu = true;
            switch (btn)
            {
                case "btn_save_load":
                    ChangeState(state.SaveLoad);
                    break;
                case "btn_evidence":
                    ChangeState(state.Evidence);
                    break;
                default:
                    Debug.Log("ERROR: Button Marked as 'tab' But Was Not Found In Tab Switch List!" + btn);
                    break;
            }
    
        }
         
    }


    public void TravelButtonPressed(int btn)
    {
        SceneManager.Instance.ChangeScene(btn);
        CloseMenu();
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
        States[(int)myState].transform.SetAsLastSibling();
        //States[(int)myState].SetActive( false );
		//set our new state
        States[(int)newState].transform.SetAsFirstSibling();
		myState = newState;

	}
    private void AnimateMenu()
    {
        if( animateState == myState )
        { 
            animateState++;
        }
        else if( animateState == state.end )
        {
            animateMenu = false;
        }
        else
        {
            float step = speed * Time.deltaTime;
            Vector3[] dest = new Vector3[2];
            dest[0] = menuUIOriginalPos[(int)animateState];
            dest[1] = btnUIOriginalPos[(int)animateState];
            if(moveRight == true)
            {
                dest[0].x += offset;
                dest[1].x += offset;
            }
            States[(int)animateState].transform.position = Vector2.MoveTowards( States[(int)animateState].transform.position, dest[0], step );
            StatesButton[(int)animateState].transform.position = Vector2.MoveTowards( States[(int)animateState].transform.position, dest[1], step );
            if( States[(int)animateState].transform.position == dest[0] && StatesButton[(int)animateState].transform.position == dest[1] )
            {
                animateState++;
            }
        }
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
        ChangeState( state.SaveLoad );
        menu.SetActive(false);
		//delte input eater

        // TODO(jesse): Hacky fix to input blocker disappearing while still in dialogue
        // so we need to make a better one
        if (!CommandManager.Instance.myBannerBox.activeInHierarchy)
        {
            SceneManager.Instance.SetInputBlocker(false);
        }
    }

    public void VolumeSlider( float multiplier )
    {
        AudioPlayer.instance.SetVolume(multiplier);
        MusicManager.instance.SetVolume(multiplier);
    }

    public void BrightnessSlider( float multiplier )
    {
        Debug.Log( "Brightness Changed: " + multiplier );
    }


}


