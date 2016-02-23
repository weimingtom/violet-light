using UnityEngine;
using System.Collections;

public class ChangePoseCommand : Commands
{
	string characaterName;
	string newPose;
    //bool loop = false;
    public ChangePoseCommand()
    {
        commandTag = "changeposecommand";
    }
	public void SetNewPose(string character, string pos)
	{
		characaterName = character;
        newPose = pos;
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
        CharacterManager.Instance.ChangeCharacterPose( characaterName, newPose );
        return true;
		//return !loop;
	}
	public override bool Destroy()
	{
        CharacterManager.Instance.ChangeCharacterPose( characaterName, "neutral" );
        return true;
	}
}
