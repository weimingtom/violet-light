using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Puzzle1 : MonoBehaviour {

    public int height = 4;
    public int width = 3;

    public List<GameObject> pieceList = new List<GameObject>();

	void Start () 
    {
        int numPieces = height * width;
        

        for(int i = 0; i < numPieces; ++i)
        {
            //create the puzzle peices from scratch.
            GameObject puzzlePiece = new GameObject("PuzzlePiece" + i);
            puzzlePiece.AddComponent<SpriteRenderer>();
            puzzlePiece.AddComponent<Piece>();
            pieceList.Add( puzzlePiece );
            pieceList[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>( "Textures/Puzzle/01/TestPuzzlePiece" );
            pieceList[i].transform.parent = this.transform;

            //set who they connect to.

        }
        


	
	}
	
	void Update () 
    {
        //check for completeion

	
	}
}
