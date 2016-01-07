using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {



	void Start () 
    {
        this.transform.position = new Vector2(Random.Range(0, 10), Random.Range(0,10));
        this.transform.Rotate( 0, 0, Random.Range( 0, 360 ) );
	
	}

	void Update () 
    {
        //test for click
        if( Input.GetMouseButton(0) )
        {
            this.transform.position = Input.mousePosition;
            
        }
	
	}
}
