using UnityEngine;
using System.Collections;

public class ChangePositionCommand : Commands
{
	string characaterName;
	string newPosition;

	public void SetNewPosition(string character, string pos)
	{
		characaterName = character;
		newPosition = pos;
	}
	public override void PrintData()
	{
		Debug.Log ("[Change Character Command] Contain CharacterName : " + characaterName.ToString() + ":: New Position : " + newPosition.ToString() );
	}
	public override bool ExecuteCommand()
	{
		return true;
	}
	public override bool Destroy()
	{
        return true;
	}
}
