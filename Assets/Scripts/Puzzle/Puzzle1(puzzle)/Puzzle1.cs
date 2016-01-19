using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Puzzle1 : Puzzle {

    public int height = 4;
    public int width = 3;
    private int numpieces;
    private float size;
    private const float reduceSize = 0.4f;
    public string filepath = "Textures/puzzle/01/Swan_0";

    private PuzzleStatus status = PuzzleStatus.NotRunning;

    private List<GameObject> pieceList = new List<GameObject>();
    public GameObject[] slotList;


    public override void Initalize()
    {
        numpieces = height * width;
        status = PuzzleStatus.Running;

        for( int i = 0; i < numpieces; ++i )
        {
            //create the puzzle pieces from scratch.
            GameObject puzzlePiece = new GameObject( "PuzzlePiece" + i );
            puzzlePiece.AddComponent<SpriteRenderer>();
            puzzlePiece.AddComponent<Piece>();
            puzzlePiece.AddComponent<ClickToMove>();
            pieceList.Add( puzzlePiece );
            //construct string
            string eFilepath = filepath + (i + 1);
            pieceList[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>( eFilepath );
            puzzlePiece.AddComponent<PolygonCollider2D>();
            pieceList[i].transform.parent = this.transform;
            pieceList[i].gameObject.GetComponent<Piece>().SetDetails( i, this );
            pieceList[i].gameObject.transform.localScale = new Vector3( reduceSize, reduceSize, 1.0f );


            //set who they connect to.
            slotList[i].gameObject.GetComponent<PieceSlot>().SetID( i, this );

        }
        size = pieceList[0].gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        size *= reduceSize;

    }


    public override void Submit()
    {
        for( int i = 0; i < numpieces; ++i )
        {
            if( pieceList[i].gameObject.GetComponent<Piece>().moveable )
            {
                break;
            }
            else if( i == numpieces - 1 )
            {
                //PUZZLE COMPLETE
                status = PuzzleStatus.Win;
            }
        }
    }


    public override void Reset()
    {
        // Rest it

        for( int i = 0; i < numpieces; ++i )
        {
            Destroy( pieceList[i] );
        }
        Initalize();
    }


    public override PuzzleStatus GetStatus()
    {
        return status;
    }
	
    //TODO: Bring Peice to front when clicked and always take the piece on top.
    public void Placed( int uID )
    {
        //get sides
        int left = uID - 1;
        int right = uID + 1;
        int up = uID - width;
        int down = uID + width;

        if(uID % width == 0)
            left = -1;
        if(uID % width == width - 1)
            right = -1;
        if(uID < width)
            up = -1;
        if(uID >= (height - 1) * width)
            down = -1;

        Debug.Log( "UID: " + uID + " PositionX: " + pieceList[uID].gameObject.transform.position.x + " Left: " + left + " Right: " + right + " Up: " + up + " Down: " + down);



        /*

        //check if any others collide
        int hxW = height * width;
        Vector3 currPiece = pieceList[uID].transform.position;
        if(left != -1)
        {
            if( pieceList[left].transform.position.x > currPiece.x - (size + size * 0.6) && pieceList[left].transform.position.x < currPiece.x - (size + size * 0.2) )
            {
                if( pieceList[left].transform.position.y > currPiece.y - size * 0.3 && pieceList[left].transform.position.y < currPiece.y + size * 0.3 )
                     snap( up, uID, -1, 0 );
            }
        
        }
        if( right != -1 )
        {
            if( pieceList[right].transform.position.x > currPiece.x + (size + size * 0.6) && pieceList[right].transform.position.x < currPiece.x + (size + size * 0.2))
            {
                if( pieceList[right].transform.position.y > currPiece.y - size * 0.3 && pieceList[right].transform.position.y < currPiece.y + size * 0.3 )
                    snap( up, uID, 1, 0 );
            }

        }
        if( down != -1 )
        {
            if( pieceList[down].transform.position.y > currPiece.y - (size + size * 0.6) && pieceList[down].transform.position.y < currPiece.y - (size + size * 0.2))
            {
                if( pieceList[down].transform.position.x > currPiece.x - size * 0.3 && pieceList[down].transform.position.x < currPiece.x + size * 0.3 )
                    snap( up, uID, 0, -1 );
            }

        }
        if( up != -1 )
        {
            if( pieceList[up].transform.position.y > currPiece.y + (size + size * 0.6) && pieceList[up].transform.position.y < currPiece.y + (size + size * 0.2))
            {
                if( pieceList[up].transform.position.x > currPiece.x - size * 0.3 && pieceList[up].transform.position.x < currPiece.x + size * 0.3 )
                    snap( up, uID, 0, 1);
            }

        }
          
          
         */
    }

    
    private void snap( int piece1, int piece2, int x, int y )
    {
        float positionX = pieceList[piece1].transform.position.x + (size * x);
        float positionY = pieceList[piece1].transform.position.y + (size * y);
        pieceList[piece2].transform.position = new Vector3( positionX, positionY );
        Debug.Log( "Piece " + piece2 + " Connected to piece " + piece1 + "!" );
    }

    public void collided( GameObject piece, int slot )
    {
        if( pieceList[slot].gameObject == piece)
        {
            piece.transform.position = slotList[slot].transform.position;
            pieceList[slot].gameObject.GetComponent<Piece>().doneMoving();
        }
    }
   
}
