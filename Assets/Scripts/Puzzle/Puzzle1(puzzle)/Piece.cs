using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {

    private Vector3 lastMousePos;

	void Start () 
    {
        this.transform.position = new Vector2(Random.Range(-6, 6), Random.Range(-2, 2));
        this.transform.Rotate( 0, 0, Random.Range( 0, 4 ) * 90);
	
	}

	void Update () 
    {
        if( lastMousePos != new Vector3( 0, 0, 0 ) )
        {
            this.transform.position += Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePos;
            lastMousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        }

        
	
	}

    void OnMouseDown()
    {
        //rotate if double clicked.
        if( DoubleClick.Instance.TestDoubleClick() )
        {
            this.transform.Rotate( 0, 0, -90 );
        }

        lastMousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );

    }

    void OnMouseUp()
    {
        lastMousePos = new Vector3(0,0,0);
    }
}
