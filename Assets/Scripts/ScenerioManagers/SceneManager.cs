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
    private string CurrentChar = "";

    private bool enteredNewScene = false;

    private int QuestStage;

    
    public void AdvQuest()
    {
        ++QuestStage;
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
        SceneStart();
        DoFade();
    }

    public void LoadGame(Game loadedGame)
    {
        currentScene = loadedGame.currentScene;
        QuestStage = loadedGame.questStage;
        foreach (string item in loadedGame.inventory)
            ItemManager.Instance.AddItem(item);
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
            currentScene = SceneID;
            ChangeBackground( Scenes[SceneID].Background , 0.7f);
            InteractableManager.Instance.Clear();
            InteractableManager.Instance.Spawn( Scenes[SceneID].Prefab ,Vector3.zero);
            MusicManager.instance.ChangeSong( Scenes[SceneID].Name );
            Debug.Log( "[scene manager] Changed Scene to number "+SceneID );
            enteredNewScene = true;
        }
        else Debug.Log( "[scene manager] No case loaded!" );
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
                    currentScene = (int)cs.ID;
                    ChangeBackground(Scenes[currentScene].Background, 0.7f);
                    InteractableManager.Instance.Clear();
                    InteractableManager.Instance.Spawn(Scenes[currentScene].Prefab, Vector3.zero);
                    MusicManager.instance.ChangeSong(Scenes[currentScene].Name);
                    Debug.Log("[scene manager] Changed Scene to number " + cs.ID);
                    enteredNewScene = true;
                    break;
                }
            }
            
        }
        else Debug.Log("[scene manager] No case loaded!");
    }

    public void SetScenePlayed(string scene, bool played = true)
    {
        ScenesPlayed.Add(scene, true);
    }

    public bool GetScenePlayed(string scene)
    {
        // NOTE(jesse): This might be an expensive look up
        if (ScenesPlayed.ContainsKey(scene))
        return ScenesPlayed[scene];
        return false;
    }

    // NOTE(jesse): Function to run the intro script after the fade into a new scene has finished.
    private void SceneStart()
    {
        if(enteredNewScene)
        {
            if (!SceneManager.Instance.GetInputBlocker())
            {
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
  //          Case = NewCase;

            Scenes.Clear();
            /********* LOADING CHARACTERS FROM FILE ***********/
            //Load in the characters specific to the case. 
            string filepath = "DialougeScripts/characters_scene_";
            filepath += NewCase.ToString();
            FileReader.Instance.ReadCharacter(filepath);

            /************** LOAD THE BACKGROUNDS FROM A FILE *******************/
            //Load in the backgrounds that are specific to the case. 
            filepath = "backgrounds_scene_";
            filepath += NewCase.ToString();
            FileReader.Instance.ReadBackgrounds(filepath);
            FileReader.Instance.ReadScenes( "scenes_scene_" + NewCase.ToString() );
           
        }
        else Debug.Log( "[scene manager] Failed to load case" );
    }

    public bool GetCanSkip()
    {
        return CanSkip;
    }
}
