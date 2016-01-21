using UnityEngine;
using System.Collections;

public class Puzzle03 : MonoBehaviour
{

    private bool chosen = false;
    public GameObject[] labels;
    public GameObject[] fruits;
    public GameObject[] crates;
    private Vector3[] fruitLoc;

    private int fruitShown;
    private bool correctChoice = false;

    //0 - APPLES, 1 - Oranges, 2 - App&Orange

    void Start()
    {
        //get locations
        fruitLoc = new Vector3[3];
        float[] loc = new float[3]; 
        for(int i = 0; i < 3; ++i)
        {
            fruitLoc[i] = fruits[i].transform.position;
            fruits[i].SetActive(false);
            loc[i] = labels[i].transform.position.x;
            labels[i].GetComponent<ClickToMove>().moveable = false;
        }
        int randLoc = Random.Range( 0, 2 );

        for( int i = 0; i < 3; ++i )
        {
            if(randLoc == 3)
                randLoc = 0; 
            labels[i].transform.position = new Vector3( loc[randLoc], labels[0].transform.position.y );
            crates[randLoc].GetComponent<Crate>().sign = i;
            randLoc++;
        }


    }


    void Clicked(int id)
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

}
