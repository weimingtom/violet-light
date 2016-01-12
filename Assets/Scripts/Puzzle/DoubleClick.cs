using UnityEngine;
using System.Collections;

public class DoubleClick : MonoBehaviour 
{
    static public DoubleClick Instance;
    private float clickOne;
    private float clickTwo;
    private bool flipflop;
    const float tapSpeed = 0.50f;
    const float minumum = 0.005f;
    private float lastClick; 

    void Awake()
    {
        Instance = this;
        flipflop = false;
        clickOne = 0.0f;
        clickTwo = 0.0f;
        lastClick = 0.0f;
    }

    //call this when your object is clicked to test if it was a double click.
    public bool TestDoubleClick()
    {
        //for stopping the script from being called multiple times in one click
        if( Time.time - lastClick < minumum )
        {
            return false;
        }
        lastClick = Time.time;
        
        
        flipflop = !flipflop;
        if( flipflop )
        {
            clickOne = Time.time;
            if( clickOne - clickTwo <= tapSpeed )
            {
                clickOne = 0.0f;
                return true;
            }
            else
                return false;
            
        }
        else
        { 
            clickTwo = Time.time;
            if( clickTwo - clickOne <= tapSpeed )
            {
                clickTwo = 0.0f;
                return true;
            }
            else
                return false;
        }

    }
	


}
