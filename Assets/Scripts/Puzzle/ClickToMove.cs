using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {

    private Vector3 lastMousePos = new Vector3(0.0f, 0.0f, 0.0f);
    public bool moveable = true;

    private bool held = false;
    

	// Update is called once per frame
	void Update () 
    {
        if( moveable && lastMousePos != new Vector3( 0, 0, 0 ) )
        {
            this.transform.position += Camera.main.ScreenToWorldPoint( Input.mousePosition ) - lastMousePos;
            lastMousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        }
	}

    public bool GetMoving()
    {
        return !(lastMousePos == new Vector3( 0.0f, 0.0f, 0.0f ));
    }

    void OnMouseDown()
    {
        held = true;
        lastMousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
    }

    public void OnMouseUp()
    {
        held = false;
        lastMousePos = new Vector3( 0.0f, 0.0f, 0.0f );
    }

    public void SetHeld(bool newH)
    {
        held = newH;
    }

    public bool GetHeld()
    {
        return held;
    }
}
