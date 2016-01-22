using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour 
{

    public int id;
    public int sign;
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
        puzScript.LastTriggered( id, other.gameObject );

        if (!other.GetComponent<ClickToMove>().GetHeld())
        {
            other.GetComponent<ClickToMove>().Snap(3);
        }
    }

    void OnTriggerExit2D( Collider2D other )
    {
        puzScript.ClearTrigger();
    }
}
