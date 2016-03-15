using UnityEngine;
using System.Collections;


public class MenuManager : MonoBehaviour
{

	public enum state
	{
        SaveLoad,
        Evidence,
        Note,
        Location,
        Suspect,
        Puzzle,
        Setting,
        end
	}

    public bool MouseIsAboveInv { protected get; set; }
    private bool animateMenu;
    private bool calledByOpenMenuCommand;
    private state openMenuState;

    public static MenuManager instance;
	public GameObject menu;
	private bool active;

    public GameObject menuButton;
    public GameObject menuButtonBlocker;
    bool forceCloseMenu = false;

	private state myState;
    //animation purposes
    //public GameObject[] States;
    public float speed = 1.0f;

    void Awake()
    {
        instance = this;
    }

	void Start () 
    {
        calledByOpenMenuCommand = false;
        menu.SetActive(false);
        active = false;
        animateMenu = false;
        // NOTE(jesse): Calling this while inactive will probs throw error
        // but Sometimes the menu won't open on first hit without it
        ChangeState(state.SaveLoad);
        myState = state.SaveLoad;
    }

	void Update () 
    {
	    //check if open/close menu button was pressed
        if( Input.GetKeyDown( KeyCode.Escape ) && menuButton.activeInHierarchy && !CommandManager.Instance.myBannerBox.activeInHierarchy )
        {
			ToggleMenu();
		}
	}

	public bool GetMenuActive()
	{
		return active;
	}

    public bool IsDoneAnimating()
    {
        return !animateMenu;
    }

    // NOTE(jesse): Enable and disables the menu buttons and the ability to open the menu
    public void ToggleMenuAccess(bool enable = false)
    {
        menuButton.SetActive(enable);
        menuButtonBlocker.SetActive(enable);
    }

	public void ToggleMenu()
	{
        SFXManager.instance.PlayPage();
        SceneManager.Instance.ResetCursor();

        if( !animateMenu && !UIAnimation.Instance.animateBackward && !UIAnimation.Instance.animateForward )
        {
            if (!active)
            {
			    OpenMenu();
                ItemManager.Instance.SetLoadInventory( true );

            }
		    else
            {
                //active = false;
			    CloseMenu();
            }
        }
	}

    public void OpenEvidenceTab()
    {
        ToggleMenu();
        calledByOpenMenuCommand = true;
        openMenuState = state.Evidence;
    }

    public void OpenTravelTab()
    {
        ToggleMenu();
        calledByOpenMenuCommand = true;
        openMenuState = state.Location;
    }

    public void ForceCloseMenu()
    {
        ToggleMenu();
        forceCloseMenu = true;
    }

    //Get When a Tab button is pressed
    public void TabPressed( string btn )
    {
        // TODO(jesse): THis is a quick hack to ensure that you don't travel during dialogue
        if( !CommandManager.Instance.myBannerBox.activeInHierarchy )
        {
            SFXManager.instance.PlayPage();
            switch( btn.ToLower() )
            {
            case "btn_save_load":
            ChangeState( state.SaveLoad );
            break;
            case "btn_evidence":
            ChangeState( state.Evidence );
            break;
            case "btn_note":
            ChangeState( state.Note );
            break;
            case "btn_location":
            ChangeState( state.Location );
            locationManager.Instance.UpdateButton();
            break;
            case "btn_suspect":
            ChangeState( state.Suspect );
            break;
            case "btn_puzzle":
            ChangeState( state.Puzzle );
            break;
            case "btn_setting":
            ChangeState( state.Setting );
            break;
            default:
            Debug.Log( "ERROR: Button Marked as 'tab' But Was Not Found In Tab Switch List!" + btn );
            break;
            }
        }
        else
        {
            if( btn == "btn_evidence" )
            {
                ChangeState(state.Evidence);
            }
            else if( btn == "btn_save_load" )
            {
                ChangeState( state.SaveLoad );
            }
        }
    }


    public bool CheckMouseAbove()
    {

        return MouseIsAboveInv;
    }

    public void TravelButtonPressed(int btn)
    {
        SceneManager.Instance.ChangeScene(btn);
        ForceCloseMenu();
    }

	private void ChangeState(state newState)
	{
        if( myState != newState )
        {
            //Refactor!
            UIAnimation.Instance.StartAnimate(newState);
		    myState = newState;
        }
	}

    private void OpenMenu()
    {
		//Do fancy transition animation
        menu.SetActive(true);
        animateMenu = true;

		//spawn input eater
		SceneManager.Instance.SetInputBlocker(true);
    }

    public void CloseMenu()
    {
        //set menu back to default state
        ChangeState( state.SaveLoad );
        animateMenu = true;
    }

    void DisableMenu()
    {
        //reset menu hierarchy
        menu.SetActive( false );
        //delete input eater
        // TODO(jesse): Hacky fix to input blocker disappearing while still in dialogue
        // so we need to make a better one
        if( !CommandManager.Instance.myBannerBox.activeInHierarchy && SceneMenuManager.instance.GetExamining() )
        {
            SceneManager.Instance.SetInputBlocker( false );
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

    public void SaveGame()
    {
        SFXManager.instance.PlaySave();
        SaveLoad.Load();
        SaveLoad.Save();
    }

    public void ExitGame()
    {
        Destroy(this.gameObject);
        Application.LoadLevel(0);
    }
    void AnimateMenu()
    {

        if( !active )
        {
            active = UIAnimation.Instance.OpeningMenuAnimation();
            if( active == true )
            {
                animateMenu = false;
            }
        }
        else if( active )
        {
            active = UIAnimation.Instance.ClosingMenuAnimation();
            if( active == false )
            {
                animateMenu = false;
                active = false;
                DisableMenu();
            }
        }
    }

    void FixedUpdate()
    {
        if( forceCloseMenu && !UIAnimation.Instance.animateBackward && !UIAnimation.Instance.animateForward )
        {
            forceCloseMenu = false;
            ToggleMenu();
        }
        if( calledByOpenMenuCommand && !animateMenu )
        {
            ChangeState( openMenuState );
            openMenuState = state.end;
            calledByOpenMenuCommand = false;
        }
        if( animateMenu )
        {
            AnimateMenu();
        }
    }
}


