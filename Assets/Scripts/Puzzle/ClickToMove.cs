using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {

    private Vector3 lastMousePos = new Vector3(0.0f, 0.0f, 0.0f);
    public bool moveable = true;
    

	// Update is called once per frame
	void Update () 
    {
        if( moveable && lastMousePos != new Vector3( 0, 0, 0 ) )
        {
            this.transform.position += Camera.main.ScreenToWorldPoint( Input.mousePosition ) - lastMousePos;
            lastMousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        }
	}

    void OnMouseDown()
    {
        lastMousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
    }

    public void OnMouseUp()
    {
        lastMousePos = new Vector3( 0, 0, 0 );
    }
}
