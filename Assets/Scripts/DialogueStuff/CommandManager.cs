﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class CommandManager : MonoBehaviour 
{
    bool bannerBoxStat;
    public Text myTextHolder;
    public GameObject myBannerBox;
    static public CommandManager Instance;
    bool checkBoxActive;
    //counter for CommandId
    int IDTracker;
	List<string> ID;
    //counter for command
    int commandTracker;
    List<Command> myCommand;
	//Dictionary<string, List<Command>> commands;
    //private List<Command> temporaryCommandsHolder;
    //private List<Command> RunnedCommand;

    public void SetTextHolder(string content)
    {
        myTextHolder.text = content;
    }
    void Start()
    {
        bannerBoxStat = true;
        checkBoxActive = false;
        IDTracker = 0;
        commandTracker = 0;
        Instance = this;
        ID = new List<string>();
        myCommand = new List<Command>();
        //commands = new Dictionary<string, List<Command>>();
        //temporaryCommandsHolder = new List<Command>();
    }
    public void RegisterID(string id)
	{
        ID.Add( id );
	}

    public void AddCommand(Command command)
    {
        myCommand.Add(command);
    }
    public void PrintData()
    {
        print("Coversation : " + ID[0]);
        for( int i = 0; i < myCommand.Count; i++ )
        {
            myCommand[i].PrintData();
        }
    }
    public void SetBox()
    {
        if( bannerBoxStat == true )
        {
            bannerBoxStat = false;
        }
        else if( bannerBoxStat == false )
        {
            bannerBoxStat = true;
        }
        myBannerBox.gameObject.SetActive( bannerBoxStat );
    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            SetBox();
        }
        if( myCommand[commandTracker].ExecuteCommand() )
        {
            if( commandTracker + 1 < myCommand.Count )
            {
                commandTracker++;
            }
            else
            {
                //swap scene
                Debug.Log("End of scene");
                Debug.Break();
            }
        }
        else
        {
            //print( "Waiting for time command" );
        }

        ////initialize the first set of command
        //if( FirstInitialization == false )
        //{
        //    FirstInitialization = true;
        //    //DeepCopyCommand();
        //    commands[ID[IDTracker]][0].ExecuteCommand();
        //    //RunnedCommand.Add(commands[ID[IDTracker]].ToArray());
        //}
        //else
        //{
        //    //check if the command is out of bound
        //    if( commandTracker >= RunnedCommand.Count && (IDTracker + 1) < ID.Count )
        //    {
        //        IDTracker++;
        //        commandTracker = 0;
                
        //        RunnedCommand = commands[ID[IDTracker]];
        //    }
        //    else
        //    { 
        //        //not out of bound
        //        //therefore execute command
        //        if( RunnedCommand[commandTracker].ExecuteCommand() )
        //        {
        //            commandTracker++;
        //        }
        //        else
        //        {
        //            print("[Command Manager] Execute Command return false");
        //        }
        //    }
        //}
    }
}

abstract public class Command
{
    public abstract void PrintData();
    public abstract bool ExecuteCommand();
}
public class ShowCharacterCommand : Command
{
    string CharacterName;
    string SpawnLocation;
    public override bool ExecuteCommand()
    {
        CharacterManager.Positions myPos = CharacterManager.Positions.Offscreen;
        switch(SpawnLocation)
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
        CharacterManager.Instance.ChangePosition(CharacterName, myPos);
        return true;
    }
    public override void PrintData()
    {
        Debug.Log("Show Character Command\nCharacter Name : " + CharacterName.ToString() + " ::Spawn Location : " + SpawnLocation.ToString());
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
}
public class WaitForTimeCommand : Command
{
    float waitingTime = 0;
    float totalTime = 0;
    public override void PrintData()
    {
        Debug.Log( "WaitForTimeCommand\nTime : " + waitingTime );
    }
    public void SetTime( float t )
    {
        waitingTime = t;
    }
    public float GetTime()
    {
        return waitingTime;
    }
    public override bool ExecuteCommand()
    {
        Debug.Log( "[Wait For Time]Time Before wait : " + Time.time );
        totalTime += Time.deltaTime;
        //StartCoroutine(Wait());
        Debug.Log( "[Wait For Time]Time After Wait Function is done :" + Time.time );
        return totalTime >= waitingTime;
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitingTime);
        Debug.Log( "[Wait For Time] Time on wait function : " + Time.time );
    }
}
public class ShowTextCommand : Command
{
    string conversationTag = "";
    
    public override bool ExecuteCommand()
    {
        CommandManager.Instance.SetTextHolder(DialogueHolder.Instance.GetDialogue( conversationTag ).ToString());
        return true;
    }
    public override void PrintData()
    {
        Debug.Log( "ShowTextCommand\nconversationTag : " + conversationTag );
    }
    public void SetConversation( string _conversationTag )
    {
        conversationTag = _conversationTag;
    }
    public string GetConversationTag()
    {
        return conversationTag;
    }
}
public class WaitForActionCommand : Command
{
    string actionTag = "";
    public override void PrintData()
    {
        Debug.Log( "actionTag\nActionTag : " + actionTag );
    }
    public void SetAction(string _actionTag)
    {
        actionTag = _actionTag;
    }
    public string GetAction()
    {
        return actionTag;
    }
    public override bool ExecuteCommand()
    {
        if( actionTag == "LClick" && Input.GetMouseButtonUp( 0 ) )
        {
            return true;
        }
        else if( actionTag == "RClick" && Input.GetMouseButtonUp( 1 ) )
        {
            return true;
        }
        return false;
    }
}
public class LocationCommand : Command
{
    string location = "";
    public override bool ExecuteCommand()
    {
        return true;
    }
    public override void PrintData()
    {
        Debug.Log( "LocationCommand\nlocation : " + location );
    }
    public void SetLocation(string _location)
    {
        location = _location;
    }
    public string GetLocation()
    {
        return location;
    }
}