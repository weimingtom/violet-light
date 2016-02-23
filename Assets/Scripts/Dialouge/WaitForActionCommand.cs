using UnityEngine;
using System.Collections;

public class WaitForActionCommand : Commands {

    string actionTag = "";
    public override void PrintData()
    {
        Debug.Log( "actionTag\nActionTag : " + actionTag );
    }
    public void SetAction( string _actionTag )
    {
        actionTag = _actionTag;
    }
    public string GetAction()
    {
        return actionTag;
    }
    public override bool ExecuteCommand()
    {
        if( actionTag == "LClick" && Input.GetMouseButtonUp( 0 ) )
        {
            return true;
        }
        else if( actionTag == "RClick" && Input.GetMouseButtonUp( 1 ) )
        {
            return true;
        }
        return false;
    }
    public override void Reset()
    {
        
    }
	public override bool Destroy()
	{
        return true;
	}
}
