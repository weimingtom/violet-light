using UnityEngine;
using System.Collections;

public class AdamTest : MonoBehaviour {

	
	// Update is called once per frame
	void Update () 
    {
        if( Input.GetKeyDown( KeyCode.F1 ) )
        {
            PuzzleManager.Instance.StartPuzzle( 2 );
        }

	}
}
