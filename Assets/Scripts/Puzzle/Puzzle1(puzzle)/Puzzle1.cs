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
            puzzlePiece.AddComponent<PolygonCollider2D>();
            pieceList.Add( puzzlePiece );
            pieceList[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>( "Textures/Puzzle/01/TestPuzzlePiece" );
            pieceList[i].transform.parent = this.transform;

            //set who they connect to.

        }
        


	
	}
	
    //TODO: Bring Peice to front when clicked and always take the peice on top.
	void Update () 
    {
        //check if any of the peices were clicked. 
        //if( Input.GetMouseButtonDown( 0 ) )
        //{
            //RaycastHit2D hit = Physics2D.Raycast( Camera.main.ScreenToWorldPoint( Input.mousePosition ), Vector2.zero );
            //Debug.Log( hit.collider.transform );

            //Vector2 mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
            //Collider2D hitCollider = Physics2D.OverlapPoint( mousePos );
            //Debug.Log( hitCollider );
        //}
        
	
	}

   
}
