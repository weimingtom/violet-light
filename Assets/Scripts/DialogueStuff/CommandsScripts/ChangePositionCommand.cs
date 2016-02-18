using UnityEngine;
using System.Collections;

public class ChangePositionCommand : Commands
{
	string characterName;
	string newPosition;

	public void SetNewPosition(string character, string pos)
	{
		characterName = character;
		newPosition = pos;
	}
	public override void PrintData()
	{
	}
	public override bool ExecuteCommand()
	{
        CharacterManager.Positions myPos = CharacterManager.Positions.Offscreen;
        switch( newPosition )
        {
        case "Offscreen":
        myPos = CharacterManager.Positions.Offscreen;
        break;
        case "Left1":
        myPos = CharacterManager.Positions.Left1;
        break;
        case "Left2":
        myPos = CharacterManager.Positions.Left2;
        break;
        case "Centre":
        myPos = CharacterManager.Positions.Centre;
        break;
        case "Right1":
        myPos = CharacterManager.Positions.Right1;
        break;
        case "Right2":
        myPos = CharacterManager.Positions.Right2;
        break;
        }
        CharacterManager.Instance.ChangePosition( characterName, myPos );
		return true;
	}
	public override bool Destroy()
	{
        return true;
	}
}
