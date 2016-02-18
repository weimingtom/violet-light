using UnityEngine;
using System;
using System.Collections;

public class ShowCharacterCommand : Commands 
{
    string CharacterName;
    string SpawnLocation;
    public override bool ExecuteCommand()
    {
        Debug.Log("character spawned");
        CharacterManager.Positions myPos = CharacterManager.Positions.Offscreen;
        switch( SpawnLocation )
        {
        case "offscreen":
        myPos = CharacterManager.Positions.Offscreen;
        break;
        case "left1":
        myPos = CharacterManager.Positions.Left1;
        break;
        case "left2":
        myPos = CharacterManager.Positions.Left2;
        break;
        case "centre":
        myPos = CharacterManager.Positions.Centre;
        break;
        case "right1":
        myPos = CharacterManager.Positions.Right1;
        break;
        case "right2":
        myPos = CharacterManager.Positions.Right2;
        break;
        default:
        Debug.Log("No position found\nName : " + CharacterName + " Spawn location :" + SpawnLocation);
        Debug.Break();
        break;
        }
        CharacterManager.Instance.ChangePosition( CharacterName, myPos );
        return true;
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
        SpawnLocation = location;
    }
	public override bool Destroy()
	{
        //if(something)
        CharacterManager.Instance.ChangePosition( CharacterName, CharacterManager.Positions.Offscreen );
        return true;
	}
}
