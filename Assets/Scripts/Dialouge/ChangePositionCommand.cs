using UnityEngine;
using System.Collections;

public class ChangePositionCommand : Commands
{
	string characterName;
	string newPosition;
    CharacterManager.Facings newFacing = CharacterManager.Facings.auto;
	public void SetNewPosition(string character, string pos)
	{
		characterName = character;
		newPosition = pos.ToLower();
	}
    public void SetFacing(string facing)
    {
        if (facing == "r")
        {
            newFacing = CharacterManager.Facings.right;
        }
        else
        {
            newFacing  = CharacterManager.Facings.left;

        }
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

        Debug.Log("[Change Pose Command] Setting Position to " + newPosition);        
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
        CharacterManager.Instance.ChangePosition( characterName, myPos,newFacing );
		return true;
	}
	public override bool Destroy()
	{
        return true;
	}
}
