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
            if(PuzzleList[CurrentPuzzle].GetComponent<Puzzle>().GetStatus() == PuzzleStatus.Win)
            {
                CompletedPuzzles[CurrentPuzzle] = true;
                EndPuzzle();
            }
			else if(PuzzleList[CurrentPuzzle].GetComponent<Puzzle>().GetStatus() == PuzzleStatus.Lose)
			{
				// TODO(jesse): Write gameover/failure screen
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
            //set which puzzle to load
            CurrentPuzzle = PuzzleNumber;
            SceneManager.Instance.SetInputBlocker( true );
            Debug.Log( "[Puzzle Manager] Spawned Puzzle " + PuzzleNumber );

            //Instantiate the puzzle
            GameObject Interactable = Instantiate( PuzzleList[PuzzleNumber], Vector3.zero,Quaternion.identity ) as GameObject;
            //parent the puzzle to the parent object "ParentObject"
            Interactable.transform.parent = ParentObject.transform;
            //Instatiate the puzzle UI
            GameObject MainCanvas = GameObject.Find( "Canvas" );
            PuzzleUI = Instantiate( PuzzleUIPrefab, Vector3.zero, Quaternion.identity ) as GameObject;
            PuzzleUI.transform.parent = MainCanvas.transform;
            PuzzleUI.transform.localScale = Vector3.one;
            PuzzleUI.transform.localPosition = Vector3.zero;
            Vector3 Pos = new Vector3( 0f, 0f, -2f );
            //set up background
            GameObject Interactable2 = Instantiate( PuzzleList[0], Pos,
                                               Quaternion.identity )
                                               as GameObject;
            Interactable2.transform.parent = ParentObject.transform;
            PuzzleLoaded = true;
            //Initialize the puzzle
            PuzzleList[CurrentPuzzle].GetComponent<Puzzle>().Initalize();
        }
    }

    public void EndPuzzle()
    {
        if( PuzzleLoaded )
        {
            SceneManager.Instance.SetInputBlocker( false);
            Debug.Log( "[Puzzle Manager] Clearing Puzzle" );
            foreach( Transform child in ParentObject.transform )
            {
                Destroy( child.gameObject );
                Debug.Log( "[Interactable Manager] Destroyed " + child.name + "!" );
            }
            Destroy( PuzzleUI );
            PuzzleLoaded = false;
        }
    }

    public void RestartPuzzle()
    {
        if( PuzzleLoaded )
        {
            PuzzleList[CurrentPuzzle].GetComponent<Puzzle>().Reset();
        }
    }



    public uint GetCurrentPuzzle()
    {
        return CurrentPuzzle;
    }
}
 