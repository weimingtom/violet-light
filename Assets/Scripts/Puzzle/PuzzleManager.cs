using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PuzzleManager : MonoBehaviour 
{
    public static PuzzleManager Instance;

    public GameObject[] PuzzleList;
    public bool[]       CompletedPuzzles;

    public  GameObject PuzzleUIPrefab;
    private GameObject ParentObject;
    private GameObject PuzzleUI;


    private bool PuzzleLoaded = false;
    private uint CurrentPuzzle = 1 ;

    void Awake ()
    {
        Instance = this;
        ParentObject = new GameObject("PuzzleParent");
    }

    void Update()
    {
        if(PuzzleLoaded)
        {
            // NOTE(jesse): Main Puzzle Loop
            if(PuzzleList[CurrentPuzzle].GetComponent<Puzzle>().IsSolved())
            {
                CompletedPuzzles[CurrentPuzzle] = true;
                EndPuzzle();
            }
        }
    }

    public void SubmitPuzzle()
    {

            PuzzleList[CurrentPuzzle].GetComponent<Puzzle>().Submit();
       
    }

    public void StartPuzzle(uint PuzzleNumber)
    {
        if( !PuzzleLoaded )
        {
            SceneManager.Instance.SetInputBlocker( true );
            Spawn( CurrentPuzzle );
            SpawnUI();
            Vector3 Pos = new Vector3( 0f, 0f, -2f );
            Spawn( "Background", Pos );
            PuzzleLoaded = true;
            PuzzleList[CurrentPuzzle].GetComponent<Puzzle>().Initalize();
            // TODO(jesse): Cue transition
        }
    }

    public void EndPuzzle()
    {
        if( PuzzleLoaded )
        {
            SceneManager.Instance.SetInputBlocker( false);
            Clear();
            DestroyUI();
            PuzzleLoaded = false;
        }
    }

    public void RestartPuzzle()
    {
        if( PuzzleLoaded )
        {
            //Clear();
            //DestroyUI();
            //Vector3 Pos = new Vector3(0f,0f,-6f);
            //Spawn( "Background", Pos );
            //Spawn( CurrentPuzzle );
            //SpawnUI();
            PuzzleList[CurrentPuzzle].GetComponent<Puzzle>().Reset();
        }
    }

    private void SpawnUI()
    {
        GameObject MainCanvas = GameObject.Find( "Canvas" );
        PuzzleUI = Instantiate( PuzzleUIPrefab, Vector3.zero, Quaternion.identity ) as GameObject;
        PuzzleUI.transform.parent = MainCanvas.transform;
        PuzzleUI.transform.localScale = Vector3.one;
        PuzzleUI.transform.localPosition = Vector3.zero;
    }

    private void DestroyUI()
    {
        // TODO(jesse): Maybe don't destroy this and just hide it?
        Destroy( PuzzleUI );
    }

    private void Spawn(uint PuzzleNumber)
    {
        foreach(GameObject Actable in PuzzleList)
        {
            if( Actable.name == "puzzle" + PuzzleNumber.ToString() )
            {
                Debug.Log( "[Puzzle Manager] Spawned Puzzle " + PuzzleNumber);

                GameObject Interactable = Instantiate( Actable, Vector3.zero, 
                                                       Quaternion.identity ) 
                                                       as GameObject;
                Interactable.transform.parent = ParentObject.transform;
                return;
            }
        }
        Debug.Log( "[Puzzle Manager] Cannot find puzzle " + PuzzleNumber );
    }

    private void Spawn( string ObjectName, Vector3 Pos )
    {
        foreach( GameObject Actable in PuzzleList )
        {
            if( Actable.name == ObjectName.ToString() )
            {
                Debug.Log( "[Puzzle Manager] Spawned " + ObjectName.ToString() );

                GameObject Interactable = Instantiate( Actable, Pos,
                                                       Quaternion.identity )
                                                       as GameObject;
                Interactable.transform.parent = ParentObject.transform;
                return;
            }
        }
        Debug.Log( "[Puzzle Manager] Cannot find " + ObjectName.ToString() );
    }

    private void Clear()
    {
        Debug.Log( "[Puzzle Manager] Clearing Puzzle");
        foreach( Transform child in ParentObject.transform )
        {
            Destroy( child.gameObject );
            Debug.Log( "[Interactable Manager] Destroyed " + child.name + "!");
        }
    }

    // NOTE(jesse): GETTERS AND SETTERS

    public uint GetCurrentPuzzle()
    {
        return CurrentPuzzle;
    }
}
 