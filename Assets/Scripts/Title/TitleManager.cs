using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour 
{
    public GameObject ScrollingBackground;
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

    

    static public TitleManager instance;

    void Awake()
    {
        instance = this;
    }

	void Start () 
    {
        ScrollingBackground.SetActive( false );
        WhiteBackground.SetActive( true );
        GameCompany.SetActive( true );

        GameCompany.GetComponent<FadeSprite>().SetVisible( false );

        CompanyDelayTimer = Time.time + CompanyDelayTimerTime;
        CompanyShowTimer = CompanyDelayTimer + CompanyShowTimerTime;
        
        
    }

    private int menuStage = 0;
	
	void Update () 
    {

        switch( menuStage )
        { 
        case 0:

            if( Time.time > CompanyDelayTimer )
            {
                GameCompany.GetComponent<FadeSprite>().StartFade( 1 );
                menuStage = 1;
            }
            break;

        case 1:
          
            if( Time.time > CompanyShowTimer)
            {
                GameCompany.GetComponent<FadeSprite>().StartFade( -1 );
                WhiteBackground.GetComponent<FadeSprite>().StartFade( -1 );
                ScrollingBackground.SetActive( true );
                clickAnything.GetComponent<Blink>().Initialize();
                menuStage = 2;
            }
            break;
        
        case 2:
            clickAnything.GetComponent<Blink>().UpdateThis();

            if( Input.GetMouseButton( 0 ) && !once )
            {
                clickAnything.GetComponent<Blink>().pause();
                Vector3 newpos = new Vector3( clickAnything.transform.position.x, clickAnything.transform.position.y + 0.25f, clickAnything.transform.position.z );
                gameObject.GetComponent<LinearInterpolation>().Interpolate( clickAnything, newpos );
                
                clickAnything.GetComponent<SpriteRenderer>().color = new Color( 1.0f, 1.0f, 1.0f, 1.0f );
                once = true;
            }

            if( Input.GetMouseButtonUp( 0 ) ) 
            {
                Vector3 newClickPos = new Vector3( clickAnything.transform.position.x, clickAnything.transform.position.y - 10.0f, clickAnything.transform.position.z );
                gameObject.GetComponent<LinearInterpolation>().Interpolate( clickAnything, newClickPos, 0.25f, 1 );
                Vector3 newTitlePos = new Vector3( title.transform.position.x, Screen.height * 0.75f, title.transform.position.z );
                gameObject.GetComponent<LinearInterpolation>().Interpolate( title, newTitlePos, 1.0f, 1 );
                menuStage = 3;
                once = false;
            }
            break;

        case 3:
            //bring up the menu
            if( !once )
            {
                newpos = new Vector3( 0, Screen.height * 0.6f, 0 );
                once = !once;
            }

            if( !done )
            {
                MoveButtonsHelper( newpos, btn );
            }
            else
                menuStage = stageWhenDone;
        
            //changing state is done in the buttons. 
            break;

        case 4:
            break;
        case 5:
            break;
        case 6:
            break;
        case 7:
            Application.Quit();
            break;
        default:
            Debug.LogError( "Error: Menu Stage out of bounds. [TitleManager.cs]" );
            break;
        }

	}

    public void NewGame()
    {
        MoveButtons( new Vector3( buttons[0].transform.position.x - (Screen.width + buttons[0].GetComponent<RectTransform>().rect.width), 0, 0 ), 0, 4);
    }

    public void LoadGame()
    {
        MoveButtons( new Vector3( buttons[0].transform.position.x - (Screen.width + buttons[0].GetComponent<RectTransform>().rect.width), 0, 0 ), 1, 5 );
    }

    public void Settings()
    {
        MoveButtons( new Vector3( buttons[0].transform.position.x - (Screen.width + buttons[0].GetComponent<RectTransform>().rect.width), 0, 0 ), 2, 6 );
    }

    public void Quit()
    {
        MoveButtons( new Vector3( buttons[0].transform.position.x - (Screen.width + buttons[0].GetComponent<RectTransform>().rect.width), 0, 0 ), 3, 7 );
    }


    //this is really hackey but I can;t think of anything else. 
    private void MoveButtons( Vector3 pos, int btnChosen, int goToState = 3)
    {
        newpos = pos;
        btn = btnChosen;
        bOnce = false;
        counter = 0;
        timer = 0.0f;
        done = false;
        stageWhenDone = goToState;
    }

    private void MoveButtonsHelper(Vector3 pos, int btnChosen)
    {
        if( !bOnce )
        {
            done = false;
            if( btnChosen == 0 )
                counter++;
            gameObject.GetComponent<LinearInterpolation>().Interpolate( buttons[counter], buttons[counter].transform.position + pos, btnDelay, 1 );
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
                    gameObject.GetComponent<LinearInterpolation>().Interpolate( buttons[btnChosen], buttons[btnChosen].transform.position + pos, btnDelay, 1 );
                    done = true;
                }
                else 
                {
                    gameObject.GetComponent<LinearInterpolation>().Interpolate( buttons[counter], buttons[counter].transform.position + pos, btnDelay, 1 );
                }
                timer = 0.0f;
                counter++;
                if( counter == buttons.Length )
                {
                    if( btnChosen == -1 )
                    {
                        done = true;
                        return;
                    }
                    else
                    {
                        timer = -1.0f;
                    }
                }
            }
        }
        
    }


    public void StartGame()
    {
    }
}
