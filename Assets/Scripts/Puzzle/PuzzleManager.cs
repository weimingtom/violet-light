using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PuzzleManager : MonoBehaviour 
{
    public static PuzzleManager Instance;

    public GameObject[] PuzzleList;

    public GameObject PuzzleUIPrefab;
    private GameObject ParentObject;
    private GameObject PuzzleUI;

    private bool PuzzleActive = false;
    private uint CurrentPuzzle = 0;

    void Awake ()
    {
        Instance = this;
        ParentObject = new GameObject("PuzzleParent");
        GameObject MainCanvas = GameObject.Find("Canvas");
        PuzzleUI = Instantiate(PuzzleUIPrefab,Vector3.zero,Quaternion.identity) as GameObject;
        PuzzleUI.transform.parent = MainCanvas.transform;
        PuzzleUI.transform.localScale = Vector3.one;
        PuzzleUI.transform.localPosition = Vector3.zero;
    }

    public void Spawn(uint PuzzleNumber)
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

    public void Clear()
    {
        Debug.Log( "[Puzzle Manager] Clearing Puzzle");
        foreach( Transform child in ParentObject.transform )
        {
            Destroy( child.gameObject );
            Debug.Log( "[Interactable Manager] Destroyed " + child.name + "!");
        }
    }

    // NOTE(jesse): GETTERS AND SETTERS

    public bool GetPuzzleActive()
    {
        return PuzzleActive;
    }

    public void SetPuzzleActive(bool IsActive)
    {
        PuzzleActive = IsActive;
    }

    public uint GetCurrentPuzzle()
    {
        return CurrentPuzzle;
    }

    public void SetCurrentPuzzle(uint New)
    {
        CurrentPuzzle = New;
    }
}
 