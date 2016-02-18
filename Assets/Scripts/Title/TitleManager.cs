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


    private bool DoneFirstPart = false;
    private bool DoneSecondPart = false;
    private bool DoneThirdPart = false;


    public GameObject clickAnything;
    public GameObject title;
    private bool once = false;

    
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

            if( Input.GetMouseButtonUp( 0 ) ) //TODO: Take out the magic numbers
            {
                Vector3 newClickPos = new Vector3( clickAnything.transform.position.x, clickAnything.transform.position.y - 10.0f, clickAnything.transform.position.z );
                gameObject.GetComponent<LinearInterpolation>().Interpolate( clickAnything, newClickPos, 0.25f, 1 );
                Vector3 newTitlePos = new Vector3( title.transform.position.x, title.transform.position.y + 100.0f, title.transform.position.z );
                gameObject.GetComponent<LinearInterpolation>().Interpolate( title, newTitlePos, 1.0f, 1 );
                menuStage = 3;
            }
            break;

        case 3:
            
            break;
        
        default:
            Debug.LogError( "Error: Menu Stage out of bounds. [TitleManager.cs]" );
            break;
        }

	}

    public void StartGame()
    {
        GetComponent<TitleDebug>().StartGame();
    }
}
