using UnityEngine;
using System.Collections;

public class Puzzle03 : MonoBehaviour
{

    private bool chosen = false;
    public GameObject[] labels;
    public GameObject[] fruits;
    public GameObject[] crates;
    private Vector3[] fruitLoc;
    private Vector3[] labelLoc;

    private int fruitShown;
    private bool correctChoice = false;
    private int holding;

    private int lastTriggered;


    //0 - APPLES, 1 - Oranges, 2 - App&Orange

    void Start()
    {
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
        int randLoc = Random.Range( 0, 2 );

        for( int i = 0; i < 3; ++i )
        {
            if( randLoc == 3 )
                randLoc = 0;
            labels[i].transform.position = new Vector3( labels[i].transform.position.x + 0.5f, labels[i].transform.position.y - 1.3f, -1.2f );
            labels[i].GetComponent<ClickToMove>().moveable = false;
            crates[randLoc].GetComponent<Crate>().sign = i;
            randLoc++;
        }


    }

    void Clicked( int id )
    {
        if( !chosen )
        {
            chosen = true;
            Debug.Log( "Box " + id + "Clicked, Sign " + crates[id].GetComponent<Crate>().sign );

            //decide which fruit to show.
            int sign = crates[id].GetComponent<Crate>().sign;
            switch( sign )
            {
            case 2:
            fruitShown = Random.Range( 0, 1 );
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

    public void LastTriggered( int id, GameObject held )
    {
        lastTriggered = id;
    }
    public void ClearTrigger()
    {
        lastTriggered = -1;
    }

    void Update()
    {
        if( Input.GetMouseButtonUp( 0 ) )
        {
            for( int i = 0; i < 3; ++i )
            {
                if( labels[i].GetComponent<ClickToMove>().GetHeld() )
                {
                    holding = i;
                }
            }
        }
    }
}
