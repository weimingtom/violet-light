using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemoManager : MonoBehaviour 
{

    static public DemoManager Instance;

    private int demoStage = 0;
    public int maxDemoStage = 2;

    private int currentScene;

    public Dictionary<string, string> backgroundLookup = new Dictionary<string, string>();
    private GameObject currBackground;
    private SpriteRenderer currBackgroundRend;

    public GameObject ScreenBlocker;

    //0.0 - 1.0. How quickly the picture fades in. 
    public float deltaAlpha = 0.7f;
    //thew new background that is fading in
    private GameObject newBackground;
    private SpriteRenderer newBackgroundRend;

    private bool CanControl = false;

    private bool enteredNewScene = false;

    void Awake()
    {
        Instance = this;

        // Create GameObject
        ScreenBlocker = Instantiate( ScreenBlocker, new Vector3(0,0,-8), Quaternion.identity ) as GameObject;
        currBackground = new GameObject( "Background" );
        currBackgroundRend = currBackground.AddComponent<SpriteRenderer>();

        newBackground = new GameObject( "NewBackground" );
        newBackground.transform.position = new Vector3( 0f, 0f, -0.01f );
        newBackgroundRend = newBackground.AddComponent<SpriteRenderer>();
        newBackgroundRend.sprite = null;
        
    }

    void Start()
    {
        GameManager.instance.SetDemoMode(true);
        LoadCase(1);
        ScreenBlocker.SetActive(true);
    }

    void Update()
    {
        if (enteredNewScene)
        {
            //if (!SceneManager.Instance.GetInputBlocker())
            {

                string introScene = "demo_" + demoStage;
                FileReader.Instance.LoadScene(introScene);
                enteredNewScene = false;
            }
        }

        DoFade();

        if(!CommandManager.Instance.myBannerBox.activeInHierarchy && newBackgroundRend.sprite == null)
        {
            ChangeScene();
        }
        if(Input.GetMouseButton(0))
        {
            // TODO(jesse): wait for fade
            FadeOutScreen.instance.BeginFade(1);
            Application.LoadLevel("TitleScene");
            GameManager.instance.SetDemoMode(true);

        }
    }

    private bool checkExists( string bgName )
    {
        return !backgroundLookup.ContainsKey( bgName );
    }

    private void ChangeBackground( string backgroundName, float Speed = 0.5f )
    {
        // Load from \Resources\ folder
        newBackgroundRend.sprite = Resources.Load<Sprite>( backgroundLookup[backgroundName] );
        newBackgroundRend.color = new Color( 1f, 1f, 1f, 0f );
        deltaAlpha = Speed;
    }

    public void ChangeBg(string backgroundName, float Speed = 0.5f)
    {
        newBackgroundRend.sprite = Resources.Load<Sprite>(backgroundLookup[backgroundName]);
        newBackgroundRend.color = new Color(1f, 1f, 1f, 0f);
        deltaAlpha = Speed;
    }

    void DoFade()
    {
        if( newBackgroundRend.sprite != null )
        { 
            float alpha = newBackgroundRend.color.a;

            if( alpha < 1.0 ) //Still doing Fade
            {
                newBackgroundRend.color = new Color( 1f, 1f, 1f, alpha + (deltaAlpha * Time.deltaTime) );
            }
            else //Fade done: Set old to new and new to null.
            {
                currBackgroundRend.sprite = newBackgroundRend.sprite;
                newBackgroundRend.sprite = null;
            }
        }
    }

    public void ChangeScene()
    {
        ++demoStage;
        if (demoStage > maxDemoStage)
        {
            demoStage = 0;
        }
        enteredNewScene = true;

        FadeOutScreen.instance.BeginFade(-1, 0.5f);
    }
    public void LoadCase(uint NewCase)
    {
        Debug.Log( "[scene manager] loading case..." );
        // TODO(jesse): StringParser read file into memory
        //CurrentCaseFile = FileReader.Instance.LoadFile()
            /********* LOADING CHARACTERS FROM FILE ***********/
            //Load in the characters specific to the case. 
            FileReader.Instance.ReadCharacter("characters_scene_" + NewCase.ToString());

            /************** LOAD THE BACKGROUNDS FROM A FILE *******************/
            //Load in the backgrounds that are specific to the case. 
            FileReader.Instance.ReadBackgrounds("backgrounds_scene_" + NewCase.ToString());
            //FileReader.Instance.ReadScenes( "scenes_scene_" + NewCase.ToString() );
            currentScene = 0;
    }


    public void SetCanControl(bool newBool)
    {
        CanControl = newBool;
    }


    public bool GetCanControl()
    {
        return CanControl;
    }
}
