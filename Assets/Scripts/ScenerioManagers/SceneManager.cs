using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour 
{

    public struct Scene
    {
        public uint     ID;
        public string   Name;
        public string   Background;
        public uint     Time;
        // TODO(jesse): Load everything in the scene from a 
        // file dynamically
        public string Prefab;
    }
    static public SceneManager Instance;

    private string CurrentCaseFile = null;
//    private uint Case = 0;
    List<Scene> Scenes = new List<Scene>();

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

    private bool CanSkip;
    private string CurrentChar = "";

    private int QuestStage = 0;

    
    public void AdvQuest()
    {
        ++QuestStage;
    }

    public int GetQuestStage()
    {
        return QuestStage;
    }

    public string GetChar()
    {
        return CurrentChar;
    }

    public void SetChar(string New)
    {
        CurrentChar = New;
    }

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
        
        CanSkip = true;
    }

    void Update()
    {
        if( newBackgroundRend.sprite != null )
            DoFade();
    }

    public void SetInputBlocker(bool Enabled)
    {
        if(!Enabled)
        {
            if(CommandManager.Instance.myBannerBox.activeInHierarchy)
            {
                Enabled = true;
            }
        }
        ScreenBlocker.SetActive( Enabled );
    }

    private bool checkExists( string bgName )
    {
        return !backgroundLookup.ContainsKey( bgName );
    }

    void ChangeBackground( string backgroundName, float Speed = 0.5f )
    {
        //Check if the requested background exists in the dictionary - debug mode only.
        if( Debug.isDebugBuild )
        {
            if( checkExists( backgroundName ) )
            {
                Debug.Log( "[Scene Manager] ERROR: Requested background does not exist - " + backgroundName );
                Debug.Break();
            }
        }
        // Load from \Resources\ folder
        newBackgroundRend.sprite = Resources.Load<Sprite>( backgroundLookup[backgroundName] );
        newBackgroundRend.color = new Color( 1f, 1f, 1f, 0f );
        deltaAlpha = Speed;
        SceneManager.Instance.SetInputBlocker( true );
    }

    void DoFade()
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
            SceneManager.Instance.SetInputBlocker( false );
        }

    }
	
    public void ChangeScene(int SceneID)
    {
        if( CurrentCaseFile != null )
        {
            currentScene = SceneID;
            ChangeBackground( Scenes[SceneID].Background , 0.7f);
            InteractableManager.Instance.Clear();
            InteractableManager.Instance.Spawn( Scenes[SceneID].Prefab ,Vector3.zero);
            Debug.Log( "[scene manager] Changed Scene to number "+SceneID );
        }
        else Debug.Log( "[scene manager] No case loaded!" );
    }

    public int GetScene()
    {
        return currentScene;
    }

    public void LoadCase(uint NewCase)
    {
        Debug.Log( "[scene manager] loading case..." );
        // TODO(jesse): StringParser read file into memory
        //CurrentCaseFile = FileReader.Instance.LoadFile()
        CurrentCaseFile = "0";
        if( CurrentCaseFile != null ) 
        {
            Debug.Log( "[scene manager] Case loaded!" ); 
  //          Case = NewCase;

            Scenes.Clear();
            /********* LOADING CHARACTERS FROM FILE ***********/
            //Load in the characters specific to the case. 
            string filepath = "DialougeScripts/Characters_Scene";
            filepath += NewCase.ToString();
            FileReader.Instance.ReadCharacter(filepath);

            /************** LOAD THE BACKGROUNDS FROM A FILE *******************/
            //Load in the backgrounds that are specific to the case. 
            filepath = "Backgrounds_Scene";
            filepath += NewCase.ToString();
            FileReader.Instance.ReadBackgrounds(filepath);

            // TODO(jesse): Load this in from a file
            Scene NewScene = new Scene();
            NewScene.Background = "alleyway";
            NewScene.ID = 0;
            NewScene.Name = "Alley Way";
            NewScene.Time = 1005;
            NewScene.Prefab = "TestArea1";
            Scenes.Add( NewScene );

            NewScene.Background = "bridge";
            NewScene.ID = 1;
            NewScene.Name = "Bridge";
            NewScene.Time = 0605;
            NewScene.Prefab = "TestArea2";
            Scenes.Add( NewScene );

            NewScene.Background = "market";
            NewScene.ID = 2;
            NewScene.Name = "Market";
            NewScene.Time = 0605;
            NewScene.Prefab = "TestArea3";
            Scenes.Add(NewScene);

            NewScene.Background = "crimescene";
            NewScene.ID = 3;
            NewScene.Name = "Crime Scene";
            NewScene.Time = 0605;
            NewScene.Prefab = "demo_crimescene";
            Scenes.Add(NewScene);

            NewScene.Background = "police_station";
            NewScene.ID = 4;
            NewScene.Name = "Police Station";
            NewScene.Time = 0605;
            NewScene.Prefab = "demo_policestation";
            Scenes.Add(NewScene);

            NewScene.Background = "music_shop";
            NewScene.ID = 5;
            NewScene.Name = "Rush's Music Shop";
            NewScene.Time = 0605;
            NewScene.Prefab = "demo_music";
            Scenes.Add(NewScene);

            NewScene.Background = "docks";
            NewScene.ID = 6;
            NewScene.Name = "Fisherman's Wharf";
            NewScene.Time = 0605;
            NewScene.Prefab = "demo_warf";
            Scenes.Add(NewScene);

           
        }
        else Debug.Log( "[scene manager] Failed to load case" );
    }

    public bool GetCanSkip()
    {
        return CanSkip;
    }
}
