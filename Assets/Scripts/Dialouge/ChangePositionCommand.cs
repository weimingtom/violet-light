using UnityEngine;
using System.Collections;

public class ChangePositionCommand : Commands
{
	string characterName;
	string newPosition;

	public void SetNewPosition(string character, string pos)
	{
		characterName = character;
		newPosition = pos.ToLower();
	}
    public override void Reset()
    {
    }
	public override void PrintData()
	{
	}
	public override bool ExecuteCommand()
	{
        CharacterManager.Positions myPos = CharacterManager.Positions.Offscreen;
        switch( newPosition )
        {
        case "offscreen":
        myPos = CharacterManager.Positions.Offscreen;
        break;
        case "left1":
        myPos = CharacterManager.Positions.Left1;
        break;
        case "left":
        myPos = CharacterManager.Positions.Left1;
        break;
        case "left2":
        myPos = CharacterManager.Positions.Left2;
        break;
        case "centre":
        myPos = CharacterManager.Positions.Centre;
        break;
        case "center":
        myPos = CharacterManager.Positions.Centre;
        break;
        case "right1":
        myPos = CharacterManager.Positions.Right1;
        break;
        case "right":
        myPos = CharacterManager.Positions.Right1;
        break;
        case "right2":
        myPos = CharacterManager.Positions.Right2;
        break;
        default:
        myPos = CharacterManager.Positions.Offscreen;
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
