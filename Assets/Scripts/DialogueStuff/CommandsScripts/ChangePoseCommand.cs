using UnityEngine;
using System.Collections;

public class ChangePoseCommand : Commands
{
	string characaterName;
	string newPose;

	public void SetNewPose(string character, string pos)
	{
		characaterName = character;
        newPose = pos;
	}
	public override void PrintData()
	{
	}
	public override bool ExecuteCommand()
	{
        CharacterManager.Instance.ChangeCharacterPose( characaterName, newPose );
		return true;
	}
	public override bool Destroy()
	{
        CharacterManager.Instance.ChangeCharacterPose( characaterName, "neutral" );
        return true;
	}
}
