using UnityEngine;
using System.Collections;

public class LoadCommand : Commands
{
	string dialogue;
    //bool loop = false;
    public LoadCommand()
    {
        commandTag = "loadcommand";
    }

    public void SetLoad(string str)
    {
        dialogue = str;
    }
       
    public override void Reset()
    {
        //loop = true;
    }
	public override void PrintData()
	{
	}
	public override bool ExecuteCommand()
	{
        FileReader.Instance.LoadScene( dialogue );
        return true;
		//return !loop;
	}
	public override bool Destroy()
	{
        return true;
	}
}
