using UnityEngine;
using System.Collections;

public class PieceSlot : MonoBehaviour {

    public int myID;
    private Puzzle1 puzzleScript;

	// Use this for initialization

    public void SetID( int ID, Puzzle1 newScript )
    {
        myID = ID;
        puzzleScript = newScript;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log( "angle = " + other.gameObject.transform.rotation.eulerAngles.z );
        if( other.gameObject.transform.rotation.eulerAngles.z >= -1.0f && other.gameObject.transform.rotation.eulerAngles.z <= 1.0f ) 
           puzzleScript.collided( other.gameObject, myID );
    }


}
