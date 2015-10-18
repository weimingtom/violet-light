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
        public GameObject Prefab;
    }

    private string CurrentCaseFile = null;
    private uint Case = 0;
    List<Scene> Scenes = new List<Scene>();

	void Start () 
    {
        LoadCase( 1 );
	}
	
    void LoadScene(uint SceneID)
    {
        if( CurrentCaseFile != null )
        {
            //BackgroundManager
        }
        else Debug.Log( "[scene manager] No case loaded!" );
    }

    void LoadCase(uint NewCase)
    {
        Debug.Log( "[scene manager] loading case..." );
        // TODO(jesse): StringParser read file into memory
        //CurrentCaseFile = FileReader.Instance.LoadFile()
        if( CurrentCaseFile != null ) 
        {
            Debug.Log( "[scene manager] Case loaded!" ); 
            Case = NewCase;

            Scenes.Clear();

            // TODO(jesse): Load this in from a file
            Scene NewScene = new Scene();
            NewScene.Background = "test";
            NewScene.ID = 1;
            NewScene.Name = "Alley Way";
            NewScene.Time = 1005;
            NewScene.Prefab = null;
            Scenes.Add( NewScene );

            NewScene.Background = "test2";
            NewScene.ID = 2;
            NewScene.Name = "Bridge";
            NewScene.Time = 0605;
            NewScene.Prefab = null;
            Scenes.Add( NewScene );

        }
        else Debug.Log( "[scene manager] Failed to load case" );
    }

    
}
