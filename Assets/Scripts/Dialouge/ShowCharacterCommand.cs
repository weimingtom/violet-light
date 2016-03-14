using UnityEngine;
using System;
using System.Collections;

public class ShowCharacterCommand : Commands 
{
    string CharacterName;
    string SpawnLocation;
    //bool loop = false;
    CharacterManager.Facings newFacing = CharacterManager.Facings.auto;

    public ShowCharacterCommand()
    {
        commandTag = "showcharactercommand";
    }
    public override bool ExecuteCommand()
    {
        CharacterManager.Positions myPos = CharacterManager.Positions.Offscreen;

        Debug.Log("[Change Pose Command] Setting Position to " + SpawnLocation);
        switch (SpawnLocation)
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
        CharacterManager.Instance.ChangePosition( CharacterName, myPos, newFacing );

        return true;
    }
    public override void Reset()
    {
        CharacterManager.Instance.ChangePosition( CharacterName, CharacterManager.Positions.Offscreen );
        //loop = true;
    }
    public override void PrintData()
    {
        Debug.Log( "Show Character Command\nCharacter Name : " + CharacterName.ToString() + " ::Spawn Location : " + SpawnLocation.ToString() );
    }
    public string GetCharacter()
    {
        return CharacterName;
    }
    public string GetSpawnLocation()
    {
        return SpawnLocation;
    }
    public void SetCharacterName( string name )
    {
        CharacterName = name;
    }
    public void SetSpawnLocation( string location )
    {
        SpawnLocation = location.ToLower();
    }

    public void SetFacing( string facing )
    {
        if( facing == "r" )
        {
            newFacing = CharacterManager.Facings.right;
        }
        else
        {
            newFacing = CharacterManager.Facings.left;

        }
    }

	public override bool Destroy()
	{
        CharacterManager.Instance.ChangePosition( CharacterName, CharacterManager.Positions.Offscreen );
        return true;
	}
}
