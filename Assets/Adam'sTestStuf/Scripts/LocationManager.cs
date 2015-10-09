using UnityEngine;
using System.Collections;

public class LocationManager : MonoBehaviour {


    public GameObject[] locations;
    private int currentLoc = 0;
    private int fadeIn = -1; //-1 means nothing needs to fade in. 
    public const float deltaAlpha = 0.04f; //0.0 - 1.0. How quickly the picture fades in. 

    private int j = 1; //temp for changing scene

	// Use this for initialization
	void Start () 
    {
       locations[currentLoc].SetActive( true );
	}
	
	// Update is called once per frame
	void Update () 
    {
        if( fadeIn >= 0 )
            DoFade();


        if( Input.GetButtonDown( "ChangeScene" ) )
        {
            GoToLocation( j );
            j++;
            if( j > 1 )
            {
                j = 0;
            }
        }


	}

    void DoFade()
    {
        float alpha = locations[fadeIn].gameObject.GetComponent<SpriteRenderer>().color.a;
        if( alpha < 1.0f )
        {
            locations[fadeIn].gameObject.GetComponent<SpriteRenderer>().color = new Color( 1f, 1f, 1f, alpha + deltaAlpha );
        }
        else
        {
            locations[fadeIn].gameObject.GetComponent<SpriteRenderer>().color = new Color( 1f, 1f, 1f, 1f );
            locations[currentLoc].SetActive( false );
            locations[fadeIn].layer = 0;
            currentLoc = fadeIn;
            fadeIn = -1;
        }

        
    }

    void GoToLocation( int loc )
    { 
        locations[loc].gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);    //change alpha of new loc to 0
        locations[loc].SetActive( true );                                                           //make the sprite active
        locations[loc].layer = 1;                                                                   //bring the sprite to the top layer
        if(fadeIn == -1)                                                                            //set it to fade in on update
             fadeIn = loc;
        else
        {                                                                                           //In case of overflow instantly pop in the currently fading BG and set the new one to fade in.
            locations[fadeIn].gameObject.GetComponent<SpriteRenderer>().color = new Color( 1f, 1f, 1f, 1f );
            locations[currentLoc].SetActive( false );
            locations[fadeIn].layer = 0;
            currentLoc = fadeIn;
            fadeIn = loc;
            
            
        }
        
    
    }

}
