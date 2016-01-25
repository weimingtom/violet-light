using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour 
{

    public int id;
    public int initLabel;
    public int endLabel;
    private Puzzle03 puzScript;

    public void SetScript( Puzzle03 newScript )
    {
        puzScript = newScript;
    }

    void OnMouseDown()
    {
        SendMessageUpwards( "Clicked", id );
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if( other.GetComponent<ClickToMove>().touched )
        { 
            if (!other.GetComponent<ClickToMove>().GetHeld())
            {
                puzScript.Snap(id, other.gameObject);
            }
        }
    }

}
