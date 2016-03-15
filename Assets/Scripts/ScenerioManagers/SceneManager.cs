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

    private Dictionary<string, bool> ScenesPlayed = new Dictionary<string, bool>();

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
    private bool CanControl = true;
    private string CurrentChar = "";

    private bool enteredNewScene = false;

    private int QuestStage;
    public Texture2D cursor;
    public Texture2D cursorSparkle;
    public Texture2D cursorNorm;
    public Vector2 cursorHotspot = new Vector2( 16, 16 );

    private Vector3 defaultCameraPos;
    public GameObject cutscene;
    
    public void AdvQuest()
    {
        ++QuestStage;
        Debug.Log( "[Scene Manager] Current Quest Stage Advanced to " + QuestStage );
    }
    public void SetQuestStage(int stage)
    {
        QuestStage = stage;
        Debug.Log( "[Scene Manager] Current Quest Stage Advanced to " + QuestStage );
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
        Debug.Log( "[Scene Manager] Awakened from my eternal slumber!" );

        // Create GameObject
        ScreenBlocker = Instantiate( ScreenBlocker, new Vector3(0,0,-8), Quaternion.identity ) as GameObject;
        currBackground = new GameObject( "Background" );
        currBackgroundRend = currBackground.AddComponent<SpriteRenderer>();

        newBackground = new GameObject( "NewBackground" );
        newBackground.transform.position = new Vector3( 0f, 0f, -0.01f );
        newBackgroundRend = newBackground.AddComponent<SpriteRenderer>();
        newBackgroundRend.sprite = null;
        
        CanSkip = true;

        Cursor.SetCursor( cursor, cursorHotspot, CursorMode.Auto );
        defaultCameraPos = Camera.main.transform.position;
    }

    void Start()
    {
        SceneManager.Instance.LoadCase( 1 );

        if( GameManager.instance.newGame )
        {
            Instantiate( cutscene );
        }
        else
        {
            SceneManager.Instance.LoadGame( SaveLoad.savedGames[GameManager.instance.gameToLoad] );
        }

        // TODO: FADEOUTSCREEN CANNOT BE FOUND AT THIS POINT SO GAME BREAKS
        //FadeOutScreen.instance.BeginFade( -1 );
    }

    void Update()
    {
        SceneStart();
        DoFade();
    }

    public void LoadGame(Game loadedGame)
    {
        ItemManager.Instance.ResetInventory();
        ScenesPlayed.Clear();

        QuestStage = loadedGame.questStage;
        foreach( string item in loadedGame.inventory )
        { 
            ItemManager.Instance.AddItem( item.ToLower() );
        }
        foreach( string strScene in loadedGame.playedScenes )
            ScenesPlayed.Add( strScene, true ); 
        
        ChangeScene( loadedGame.currentScene );
    }

    public void SetCursor(bool sparkle)
    {
        if (sparkle)
        {
            Cursor.SetCursor( cursorSparkle, cursorHotspot, CursorMode.Auto );
            return;
        }
        Cursor.SetCursor( cursor, cursorHotspot, CursorMode.Auto );
    }   

    public void ResetCursor()
    {
         Cursor.SetCursor( cursorNorm, cursorHotspot, CursorMode.Auto );
    }

    public void SetCursor(string itemName)
    {
        string sceneToCheck =  QuestStage.ToString() + "_" + Scenes[currentScene].Prefab + "_" + itemName;
        bool sparkle;
        if (FileReader.Instance.IsScene(sceneToCheck))
        {
            sparkle = !ScenesPlayed.ContainsKey(sceneToCheck);
        }
        else
        {
            sparkle = false;
        }
        if (sparkle)
        {
            Cursor.SetCursor( cursorSparkle, cursorHotspot, CursorMode.Auto );
            return;
        }
        Cursor.SetCursor( cursor, cursorHotspot, CursorMode.Auto );
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

    public bool GetInputBlocker()
    {
        return ScreenBlocker.activeInHierarchy;
    }

    private bool checkExists( string bgName )
    {
        return !backgroundLookup.ContainsKey( bgName );
    }

    private void ChangeBackground( string backgroundName, float Speed = 0.5f )
    {
        //Check if the requested background exists in the dictionary - debug mode only.
        if( Debug.isDebugBuild )
        {
            if( checkExists( backgroundName ) )
            {
                Debug.Log( "[Scene Manager] ERROR: Requested background does not exist - " + backgroundName );
                //Debug.Break();
            }
        }
        // Load from \Resources\ folder
        newBackgroundRend.sprite = Resources.Load<Sprite>( backgroundLookup[backgroundName] );
        newBackgroundRend.color = new Color( 1f, 1f, 1f, 0f );
        deltaAlpha = Speed;
        SceneManager.Instance.SetInputBlocker( true );
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
                SetInputBlocker( false );
            }
        }
    }
	
    public void ChangeScene(int SceneID)
    {
        if( CurrentCaseFile != null )
        {
            SceneMenuManager.instance.RemoveCharacter();
            currentScene = SceneID;
            InteractableManager.Instance.Clear(); 
            ChangeBackground( Scenes[SceneID].Background, 0.7f );
            Camera.main.transform.position = defaultCameraPos;
            
            MusicManager.instance.ChangeSong( Scenes[SceneID].Name );
            Debug.Log( "[scene manager] Changed Scene to number " + SceneID );
            enteredNewScene = true;
            //ItemInventory.Instance.TogglePresentButton(false);

            SceneMenuManager.instance.EnteredNewScene();
            InteractableManager.Instance.Spawn( Scenes[SceneID].Prefab, Vector3.zero );
            FadeOutScreen.instance.BeginFade( -1, 0.5f );
        }
        else
        {
            Debug.Log( "[scene manager] No case loaded!" );
        }
    }


    public void OpenSecondaryScene( int SceneID )
    {
            currentScene = SceneID;
            InteractableManager.Instance.Clear();
            ChangeBackground( Scenes[SceneID].Background, 0.7f );
            Camera.main.transform.position = defaultCameraPos;
            InteractableManager.Instance.Spawn( Scenes[SceneID].Prefab, Vector3.zero );
            FadeOutScreen.instance.BeginFade( -1 );
    }

    public void ChangeScene(string SceneID)
    {
        if (CurrentCaseFile != null)
        {
            Debug.Log("[scene manager] Attempting to change scene to " + SceneID);
            foreach(Scene cs in Scenes)
            {
                if (cs.Name == SceneID || cs.Prefab == SceneID)
                {
                    ChangeScene((int)cs.ID);
                    break;
                }
            }
            
        }
        else Debug.Log("[scene manager] No case loaded!");
    }

    public void SetScenePlayed(string scene, bool played = true)
    {
        if(!ScenesPlayed.ContainsKey(scene))
        {
            ScenesPlayed.Add(scene, true);
        }
    }

    public List<string> GetAllScenesPlayed()
    {
        List<string> newScenesPlayed = new List<string>();

        foreach (string newScene in ScenesPlayed.Keys)
        {
            newScenesPlayed.Add( newScene );
        }


        return newScenesPlayed;
    }

    public bool GetScenePlayed(string scene)
    {
        // NOTE(jesse): This might be an expensive look up
        if (ScenesPlayed.ContainsKey(scene))
        return ScenesPlayed[scene];
        return false;
    }
    bool debugQuestBool = false;
    // NOTE(jesse): Function to run the intro script after the fade into a new scene has finished.
    private void SceneStart()
    {
        if(enteredNewScene)
        {
            ResetCursor();
            if (!SceneManager.Instance.GetInputBlocker())
            {
                // TODO REMOVE THIS
                if( ItemManager.Instance.CheckItem( "icy_streets" ) && ItemManager.Instance.CheckItem( "neck_wound" ) )
                {
                    if( debugQuestBool == false )
                    {
                        AdvQuest();
                        debugQuestBool = true;
                    }
                }
                string introScene = QuestStage.ToString() + "_" + Scenes[currentScene].Prefab + "_" + "intro";
                if (FileReader.Instance.IsScene(introScene) && !GetScenePlayed(introScene))
                {
                    FileReader.Instance.LoadScene(introScene);
                }
                enteredNewScene = false;
            }
        }
    }

    public int GetScene()
    {
        return currentScene;
    }

    public string GetSceneName()
    {
        return Scenes[currentScene].Prefab;
    }

    public void NewScene(string bg, uint id, string name, uint time, string prefab)
    { 
        Scene NewScene = new Scene();
        NewScene.Background = bg;
        NewScene.ID = id;
        NewScene.Name = name;
        NewScene.Time = time;
        NewScene.Prefab = prefab;
        Scenes.Add( NewScene );
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
            Scenes.Clear();
            /********* LOADING CHARACTERS FROM FILE ***********/
            //Load in the characters specific to the case. 
            FileReader.Instance.ReadCharacter("characters_scene_" + NewCase.ToString());

            /************** LOAD THE BACKGROUNDS FROM A FILE *******************/
            //Load in the backgrounds that are specific to the case. 
            FileReader.Instance.ReadBackgrounds("backgrounds_scene_" + NewCase.ToString());
            FileReader.Instance.ReadScenes( "scenes_scene_" + NewCase.ToString() );
            currentScene = 0;
        }
        else Debug.Log( "[scene manager] Failed to load case" );
    }

    public bool GetCanSkip()
    {
        return CanSkip;
    }

    public void SetCanSkip(bool newBool)
    {
        CanSkip = newBool;
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
