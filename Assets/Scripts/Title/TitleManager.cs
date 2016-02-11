using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour 
{
    public GameObject ScrollingBackground;
    public GameObject WhiteBackground;
    public GameObject GameCompany;
    //public GameObject StartButton;
    private float CompanyShowTimer = 2.0f;
    private float CompanyDelayTimer =0.0f;

    private bool DoneFirstPart = false;
    private bool DoneSecondPart = false;
    private bool DoneThirdPart = false;

    public GameObject mything;

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

        CompanyDelayTimer += Time.time;
        CompanyShowTimer += CompanyDelayTimer;

        mything.AddComponent<Blink>();
        mything.GetComponent<Blink>().Initialize(mything);
    }
	
	void Update () 
    {
        if( Time.time > CompanyDelayTimer && !DoneFirstPart)
        {
            DoneFirstPart = true;
            GameCompany.GetComponent<FadeSprite>().StartFade( 1 );
        }

        if( Time.time > CompanyShowTimer && !DoneSecondPart )
        {
            GameCompany.GetComponent<FadeSprite>().StartFade( -1 );
            WhiteBackground.GetComponent<FadeSprite>().StartFade( -1 );
            ScrollingBackground.SetActive( true );
            DoneSecondPart = true;
        }

        //if(StartButton.GetComponent<FadeFlyText>().GetDoneFade() && !DoneThirdPart)
        //{
        //    StartButton.GetComponent<StartButton>().EnableButton();
        //    DoneThirdPart = true;
        //}
	}

    public void StartGame()
    {
        GetComponent<TitleDebug>().StartGame();
    }
}
