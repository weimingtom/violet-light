using UnityEngine;
using System.Collections;

public class SetDisplayNameCommand : Commands
{
	//bool InitialSetup = true;
    string name;
    public override bool ExecuteCommand()
    {
        CommandManager.Instance.SetNameIntoNameBox( name );
        return true;
    }
    public void SetName(string newName)
    {
        name = newName;
    }
    public override void PrintData()
    {

    }
	public override bool Destroy()
	{
        return true;
	}
}
