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

        if( Input.GetKeyDown( KeyCode.O ) )
        {
            SaveLoad.Save();
        }
        if( Input.GetKeyDown( KeyCode.P ) )
        {
            SaveLoad.Load();
        }

	}
}
