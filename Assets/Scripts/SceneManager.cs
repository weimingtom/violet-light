﻿using UnityEngine;
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

    public Dictionary<string, string> backgroundLookup = new Dictionary<string, string>();
    private GameObject currBackground;
    private SpriteRenderer currBackgroundRend;

    //0.0 - 1.0. How quickly the picture fades in. 
    public float deltaAlpha = 0.7f;
    //thew new background that is fading in
    private GameObject newBackground;
    private SpriteRenderer newBackgroundRend;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        backgroundLookup.Add( "test1", "Textures/Backgrounds/backstreet_test" );
        backgroundLookup.Add( "test2", "Textures/Backgrounds/backstreet_test2" );

        // Create GameObject
        currBackground = new GameObject( "Background" );
        currBackgroundRend = currBackground.AddComponent<SpriteRenderer>();

        newBackground = new GameObject( "NewBackground" );
        newBackground.transform.position = new Vector3( 0f, 0f, -0.01f );
        newBackgroundRend = newBackground.AddComponent<SpriteRenderer>();
        newBackgroundRend.sprite = null;

        LoadCase( 1 );
    }

    void Update()
    {
        if( newBackgroundRend.sprite != null )
            DoFade();
    }

    void ChangeBackground( string backgroundName, float Speed = 0.5f )
    {
        // Load from \Resources\ folder
        newBackgroundRend.sprite = Resources.Load<Sprite>( backgroundLookup[backgroundName] );
        newBackgroundRend.color = new Color( 1f, 1f, 1f, 0f );
        deltaAlpha = Speed;
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
        }

    }
	
    public void ChangeScene(int SceneID)
    {
        if( CurrentCaseFile != null )
        {
            ChangeBackground( Scenes[SceneID].Background , 0.7f);
            InteractableManager.Instance.Clear();
            InteractableManager.Instance.Spawn( Scenes[SceneID].Prefab ,Vector3.zero);
            Debug.Log( "[scene manager] Changed Scene to number "+SceneID );
        }
        else Debug.Log( "[scene manager] No case loaded!" );
    }

    void LoadCase(uint NewCase)
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

            // TODO(jesse): Load this in from a file
            Scene NewScene = new Scene();
            NewScene.Background = "test1";
            NewScene.ID = 0;
            NewScene.Name = "Alley Way";
            NewScene.Time = 1005;
            NewScene.Prefab = "TestArea1";
            Scenes.Add( NewScene );

            NewScene.Background = "test2";
            NewScene.ID = 1;
            NewScene.Name = "Bridge";
            NewScene.Time = 0605;
            NewScene.Prefab = "TestArea2";
            Scenes.Add( NewScene );

        }
        else Debug.Log( "[scene manager] Failed to load case" );
    }

    
}
