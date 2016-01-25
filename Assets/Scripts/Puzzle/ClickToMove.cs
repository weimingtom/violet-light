using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour 
{

    private Vector3 lastMousePos = new Vector3(0.0f, 0.0f, 0.0f);
    public bool moveable = true;
    private bool held = false;
    public bool touched = false;
    

	// Update is called once per frame
	void Update () 
    {
        if(held)
        {
            this.transform.position += Camera.main.ScreenToWorldPoint( Input.mousePosition ) - lastMousePos;
            lastMousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        }
	}

    public bool GetHeld()
    {
        return held;
    }

    void OnMouseDown()
    {
        if( moveable )
        { 
            held = true;
            touched = true;
            lastMousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        }
    }

    public void OnMouseUp()
    {
        held = false;
    }

    public void Snap(int location)
    {
        Debug.Log( "Snapped to "+ location + "!" );
    }
}
