using UnityEngine;
using System.Collections;

public class PuzzleManager : MonoBehaviour 
{
    public static PuzzleManager Instance;

    public GameObject[] PuzzleList;

    private GameObject ParentObject;

    void Awake ()
    {
        Instance = this;
        ParentObject = new GameObject("ParentObject");
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
}
 