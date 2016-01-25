using UnityEngine;
using System.Collections;

public class Puzzle03 : MonoBehaviour
{

    private bool chosen = false;
    public GameObject[] labels;
    public GameObject[] fruits;
    public GameObject[] crates;
    public GameObject[] ghostLabels;
    private Vector3[] fruitLoc;
    private Vector3[] labelLoc;
    private Vector3 labelOffset;

    private int fruitShown;
    private bool correctChoice = false;
    private bool won = true;

    private PuzzleStatus status = PuzzleStatus.NotRunning;


    //0 - APPLES, 1 - Oranges, 2 - App&Orange

    //public override void Initialize()
    void Start()
    {
        status = PuzzleStatus.Running;
        labelOffset = new Vector3( -0.5f, 1.29f, 0.0f );
        //get locations
        fruitLoc = new Vector3[3];
        labelLoc = new Vector3[3];
        for( int i = 0; i < 3; ++i )
        {
            fruitLoc[i] = fruits[i].transform.position;
            fruits[i].SetActive( false );
            labelLoc[i] = labels[i].transform.position;
            crates[i].GetComponent<Crate>().SetScript( this );
        }

        //set which label goes to which box.
        int randLoc = Random.Range( 0, 3 );

        for( int i = 0; i < 3; ++i )
        {
            if( randLoc == 3 )
                randLoc = 0;
            labels[i].transform.position = labelLoc[randLoc] - labelOffset;
            ghostLabels[i].transform.position = labelLoc[randLoc] - labelOffset;
            labels[i].GetComponent<ClickToMove>().moveable = false;
            crates[randLoc].GetComponent<Crate>().initLabel = i;
            randLoc++;
        }
    }

    //public override void Reset()
    public void Reset()
    {
        for( int i = 0; i < 3; ++i )
        {
            fruits[i].transform.position = fruitLoc[i];
            labels[i].transform.position = labelLoc[i];
            labels[i].GetComponent<ClickToMove>().touched = false;
        }
        chosen = false;
        correctChoice = false;

        Start();
        //Initialize();
   
    }

    //public override void Submit()
    public void Submit()
    {
        if( correctChoice )
        {
            for( int i = 0; i < 3; ++i )
            {
                switch( crates[i].GetComponent<Crate>().initLabel )
                { 
                case 2:
                    if( fruitShown != crates[i].GetComponent<Crate>().endLabel )
                        won = false;
                    break;
                case 1:
                    if( crates[i].GetComponent<Crate>().endLabel != 0 )
                        won = false;
                    break;
                case 0:
                if( crates[i].GetComponent<Crate>().endLabel != 1 )
                        won = false;
                    break;
                    
                }
            }
        }
    
    }

    void Update()
    { 
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
        if( Input.GetKeyDown( KeyCode.S ) )
        {
            Submit();
        }
    }

    void Clicked( int id )
    {
        if( !chosen )
        {
            chosen = true;
            Debug.Log( "Box " + id + "Clicked, Sign " + crates[id].GetComponent<Crate>().initLabel );

            //decide which fruit to show.
            int sign = crates[id].GetComponent<Crate>().initLabel;
            switch( sign )
            {
            case 2:
            fruitShown = Random.Range( 0, 2 );
            correctChoice = true;
            break;
            case 1:
            fruitShown = 0;
            break;
            case 0:
            fruitShown = 1;
            break;
            default:
            Debug.Log( "ERROR: Sign incorrect, Puzzle03" );
            break;
            }

            //show the fruit
            fruits[fruitShown].transform.position = crates[id].transform.position;
            fruits[fruitShown].SetActive( true );
            //TODO: interpolate upwards, fade and place in fruitloc
            fruits[fruitShown].transform.position = fruitLoc[id];

            //make labels moveable
            for( int i = 0; i < 3; ++i )
                labels[i].GetComponent<ClickToMove>().moveable = true;

        }
    }

    public void Snap( int crateID, GameObject label )
    {
        for( int i = 0; i < 3; ++i )
        {
            if( label == labels[i] )
            {
                labels[i].transform.position = labelLoc[crateID];
                crates[crateID].GetComponent<Crate>().endLabel = i;
                break;
            }
        
        }
    }
}
