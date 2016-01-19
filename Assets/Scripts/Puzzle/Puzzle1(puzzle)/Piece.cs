using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {

    public int uniqueID;
    private Puzzle1 puzzleScript;
    public bool moveable = true;

	void Start () 
    {
        this.transform.position = new Vector2(Random.Range(-6, 6), Random.Range(-2, 2));
        this.transform.Rotate( 0, 0, Random.Range( 0, 4 ) * 90);
	
	}

    public void SetDetails( int uID, Puzzle1 newScript )
    {
        uniqueID = uID;
        puzzleScript = newScript;

    }

    public void doneMoving()
    {
        moveable = false;
        this.GetComponent<ClickToMove>().moveable = false;
    }

    void OnMouseDown()
    {
        //rotate if double clicked.
        if( moveable )
        { 
            if( DoubleClick.Instance.TestDoubleClick() )
            {
                this.transform.Rotate( 0, 0, -90.0f );
            }
        }
    }

    public void OnMouseUp()
    {
        puzzleScript.Placed( uniqueID );
        
    }
}
