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
        //Debug.Break();
        CharacterManager.Positions myPos = CharacterManager.Positions.Offscreen;
        switch( SpawnLocation )
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
	public override void Destroy()
	{
		CharacterManager.Instance.ChangePosition( CharacterName, CharacterManager.Positions.Offscreen);
	}
}
