using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour 
{
    public GameObject WhiteBackground;
    public GameObject GameCompany;
    //public GameObject StartButton;
    public float CompanyShowTimerTime = 2.0f;
    public float CompanyDelayTimerTime  = 1.0f;

    private float CompanyShowTimer;
    private float CompanyDelayTimer;

    public GameObject clickAnything;
    public GameObject title;
    private bool once = false;

    public GameObject[] buttons;
    private float timer = 0.0f;
    private int counter = 0;
    private bool bOnce = false;
    private const float btnDelay = 1.6f;
    private bool done = false;
    private int btn = -1;
    private int stageWhenDone = 3;
    Vector3 newpos;

    private List<GameObject> loadButtons = new List<GameObject>();
    public GameObject backButton;

    public float speed = 5.0f;
   
    

    static public TitleManager instance;

    void Awake()
    {
        instance = this;
    }

	void Start () 
    {
        WhiteBackground.SetActive( true );
        GameCompany.SetActive( true );

        GameCompany.GetComponent<FadeSprite>().SetVisible( false );

        CompanyDelayTimer = Time.time + CompanyDelayTimerTime;
        CompanyShowTimer = CompanyDelayTimer + CompanyShowTimerTime;
        this.gameObject.GetComponent<FadeOutScreen>().BeginFade( -1 );
        
        
    }

    private int menuStage = 0;
	
	void Update () 
    {

        switch( menuStage )
        { 
        case 0:
            DoStageZero();
            break;

        case 1:
            DoStageOne();
            break;
        
        case 2:
            DoStageTwo();
            break;

        case 3:
            //bring up the menu
            DoStageThree();
            //changing state is done in the buttons. 
            break;

        case 4:
            //NEW GAME
            DoNewGame();
            break;

        case 5:
            //LOAD GAME
            DoLoadGame();
            break;

        case 6:
            //SETTINGS
            DoSettings();
            break;

        case 7:
            //QUIT
            Application.Quit();
            break;

        default:
            Debug.LogError( "Error: Menu Stage out of bounds. [TitleManager.cs]" );
            break;
        }

	}

    //Switch Menu Stages Split up into functions for readability.
    private void DoStageZero()
    {
        if( Time.time > CompanyDelayTimer )
        {
            GameCompany.GetComponent<FadeSprite>().StartFade( 1 );
            menuStage = 1;
        }
    }
    private void DoStageOne()
    {
        if( Time.time > CompanyShowTimer )
        {
            GameCompany.GetComponent<FadeSprite>().StartFade( -1 );
            WhiteBackground.GetComponent<FadeSprite>().StartFade( -1 );
            clickAnything.GetComponent<Blink>().Initialize();
            menuStage = 2;
        }
    }
    private void DoStageTwo()
    {
        clickAnything.GetComponent<Blink>().UpdateThis();

        if( Input.GetMouseButton( 0 ) && !once )
        {
            clickAnything.GetComponent<Blink>().pause();
            Vector3 newpos = new Vector3( clickAnything.transform.position.x, clickAnything.transform.position.y + 0.25f, clickAnything.transform.position.z );
            gameObject.GetComponent<LinearInterpolation>().Interpolate( clickAnything, newpos, 3.0f );

            clickAnything.GetComponent<SpriteRenderer>().color = new Color( 1.0f, 1.0f, 1.0f, 1.0f );
            once = true;
        }

        if( Input.GetMouseButtonUp( 0 ) )
        {
            Vector3 newClickPos = new Vector3( clickAnything.transform.position.x, clickAnything.transform.position.y - 10.0f, clickAnything.transform.position.z );
            gameObject.GetComponent<LinearInterpolation>().Interpolate( clickAnything, newClickPos, 0.15f * speed, 1 );
            Vector3 newTitlePos = new Vector3( title.transform.position.x, Screen.height * 0.75f, title.transform.position.z );
            gameObject.GetComponent<LinearInterpolation>().Interpolate( title, newTitlePos, 1.0f * speed, 1 );
            menuStage = 3;
            once = false;
        }
    }
    private void DoStageThree()
    {
        if( !once )
        {
            newpos = new Vector3( 0, Screen.height * 0.6f, 0 );
            MoveButtons( newpos, -1 );
            once = true;
            stageWhenDone = 3;
        }

        if( !done )
        {
            MoveButtonsHelper( newpos, btn );
        }
        else
        {
            for( int i = 0; i < buttons.Length; ++i )
                buttons[i].GetComponent<Button>().interactable = true;

            if( stageWhenDone != menuStage )
            {
                once = false;
                menuStage = stageWhenDone;
            }


        }
    }

    private void DoNewGame()
    {
        timer += Time.deltaTime;
        if( timer >= 2.0f && !once )
        {
            once = true;
        }
        else if( timer >= 3.0f )
        {
            GameManager.instance.StartGame( true );
        }
    }
    private void DoLoadGame()
    {
        if( !once )
        {
            if( loadButtons.Count == 0 )
            {
                SaveLoad.Load();
                //show most recent save first. 
                for( int i = SaveLoad.savedGames.Count - 1; i >= 0; --i )
                {
                    //create the object
                    GameObject button = new GameObject( "button" + i, typeof( RectTransform ) );
                    button.AddComponent<Button>();
                    button.AddComponent<CanvasRenderer>();
                    button.AddComponent<Image>();
                    button.transform.SetParent( buttons[0].gameObject.GetComponentInParent<Canvas>().transform, false );
                    button.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
                    int id = i;
                    button.GetComponent<Button>().onClick.AddListener( () => LoadButtonClicked(id) );
                    GameObject text = new GameObject( "Text", typeof( RectTransform ) );
                    text.transform.SetParent( button.transform, false );
                    text.AddComponent<Text>();


                    //do the visual stuff
                    button.GetComponent<RectTransform>().SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, 400 );
                    text.GetComponent<RectTransform>().SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, 370 );
                    button.GetComponent<Image>().color = new Color( 0.1f, 0.0f, 0.4f, 0.1f );
                    text.GetComponent<Text>().text = "Saved Game: \n" + SaveLoad.savedGames[i].date + " " + SaveLoad.savedGames[i].time;
                    text.GetComponent<Text>().font = Resources.Load( "Fonts/type", typeof( Font ) ) as Font;
                    text.GetComponent<Text>().fontSize = 35;

                    button.transform.position = new Vector3( Screen.width * 0.5f, (Screen.height + button.GetComponent<RectTransform>().rect.height) * -1.0f, 0.0f );

                    //add the button
                    loadButtons.Add( button );
                }
            }

            //interpolate the buttons onto the screen

            Vector3 basePos = new Vector3( Screen.width * 0.5f, Screen.height * 0.5f, 0.0f );
            Vector3 adjusPos = new Vector3( 0.0f, Screen.height * 0.1f, 0.0f );
            bringUpBackButton();
            for( int i = 0; i < loadButtons.Count; ++i )
            {
                gameObject.GetComponent<LinearInterpolation>().Interpolate( loadButtons[i], basePos, 0.5f, 1 );
                basePos = basePos - adjusPos;
            }

            once = true;
        }
    }
    private void DoSettings()
    {
        if( !once )
        {
            bringUpBackButton();
            once = true;
        }
    }

    //Buttons
    public void NewGame()
    {
        Vector3 OffscreenLeft = new Vector3( ((Screen.width * 0.5f) + buttons[0].GetComponent<RectTransform>().rect.width) * -1.0f, 0.0f, 0.0f );
        MoveButtons( OffscreenLeft, 0, 4);
        this.gameObject.GetComponent<FadeOutScreen>().BeginFade( 1 );        
    }
    public void LoadGame()
    {
        Vector3 OffscreenLeft = new Vector3( ((Screen.width * 0.5f) + buttons[0].GetComponent<RectTransform>().rect.width) * -1.0f, 0.0f, 0.0f );
        MoveButtons( OffscreenLeft, 1, 5 );
    }
    public void Settings()
    {
        Vector3 OffscreenLeft = new Vector3( ((Screen.width * 0.5f) + buttons[0].GetComponent<RectTransform>().rect.width) * -1.0f, 0.0f, 0.0f );
        MoveButtons( OffscreenLeft, 2, 6 );
    }
    public void Quit()
    {
        Vector3 OffscreenLeft = new Vector3( ((Screen.width * 0.5f) + buttons[0].GetComponent<RectTransform>().rect.width) * -1.0f, 0.0f, 0.0f );
        MoveButtons( OffscreenLeft, 3, 7 );
    }
    public void Back()
    {
        
        Vector3 OffscreenLeft = new Vector3( ((Screen.width * 0.5f) + buttons[0].GetComponent<RectTransform>().rect.width)* -1.0f, 0.0f, 0.0f );
        MoveButtons( OffscreenLeft * -1.0f, -1, 3 );
        gameObject.GetComponent<LinearInterpolation>().Interpolate( backButton, new Vector3( Screen.width * 0.25f, Screen.height * -0.5f, 0.0f ), 0.5f, 1 );

        //interpolate whatever is on screen to off screen
        if( menuStage == 5 )
        { 
            Vector3 basePos = new Vector3( Screen.width * 0.5f, Screen.height * -0.5f, 0.0f );
            for( int i = 0; i < loadButtons.Count; ++i )
            {
                gameObject.GetComponent<LinearInterpolation>().Interpolate( loadButtons[i], basePos, 0.5f, 1 );
            }
        }
        menuStage = 3;
    }

    private void LoadButtonClicked(int id)
    {
        Debug.Log( "Loading Game " + id );

        GameManager.instance.StartGame( false, id);
    }

    //button Movement
    private void bringUpBackButton()
    {
        gameObject.GetComponent<LinearInterpolation>().Interpolate( backButton, new Vector3( Screen.width * 0.25f, Screen.height * 0.5f, 0 ), 0.5f, 1 );
    }

    //this is really spaghetti but I can't think of anything else without moving it to it's own script.
    //TODO: change the script so it changes the destination to newpos and doesn't add it to the object's current location. 
    //this will fix my menu inconsitency bug.
    private void MoveButtons( Vector3 pos, int btnChosen, int goToState = 3)
    {
        newpos = pos;
        if( btnChosen >= buttons.Length )
            btnChosen = -1;
        btn = btnChosen;
        bOnce = false;
        counter = 0;
        timer = 0.0f;
        done = false;
        stageWhenDone = goToState;
        for( int i = 0; i < buttons.Length; ++i )
            buttons[i].GetComponent<Button>().interactable = false;
    }
    private void MoveButtonsHelper(Vector3 pos, int btnChosen)
    {
        if( !bOnce )
        {
            done = false;
            if( btnChosen == 0 )
                counter++;
            gameObject.GetComponent<LinearInterpolation>().Interpolate( buttons[counter], buttons[counter].transform.position + pos, btnDelay * speed, 1 );
            bOnce = !bOnce;
            counter++;
        }

        if( counter <= buttons.Length )
        {
            timer += Time.deltaTime;
            if( counter == btnChosen )
            {
                counter++;
            }
            if( timer >= 0.2f )
            {
                if( counter == buttons.Length)
                {
                    gameObject.GetComponent<LinearInterpolation>().Interpolate( buttons[btnChosen], buttons[btnChosen].transform.position + pos, btnDelay * speed, 1 );
                    done = true;
                }
                else 
                {
                    gameObject.GetComponent<LinearInterpolation>().Interpolate( buttons[counter], buttons[counter].transform.position + pos, btnDelay * speed, 1 );
                }
                timer = 0.0f;
                counter++;
                if( counter >= buttons.Length )
                {
                    if( btnChosen == -1 )
                    {
                        done = true;
                        return;
                    }
                    else
                    {
                        timer = -0.6f;
                    }
                }
            }
        }
    }

}
