using UnityEngine;
using System.Collections;

public class LocationCommand : Commands {

    string location = "";
    public override bool ExecuteCommand()
    {
        return true;
    }
    public override void PrintData()
    {
        Debug.Log( "LocationCommand\nlocation : " + location );
    }
    public void SetLocation( string _location )
    {
        location = _location;
    }
    public string GetLocation()
    {
        return location;
    }
}
