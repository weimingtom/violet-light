using UnityEngine;
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
    //counter for CommandId
	List<int> charterCommandTracker;
	List<string> SceneId;
    //counter for command
    int commandTracker;
    List<Commands> myCommand;

	//Dictionary<string, List<Command>> commands;
    //private List<Command> temporaryCommandsHolder;
    //private List<Command> RunnedCommand;

    public void SetTextHolder(string content)
    {
		myTextHolder.text = content;
	}
	public void TextSwitch(bool status)
	{
		myTextHolder.gameObject.SetActive (status);
	}
	public void TextBoxSwitch(bool status)
	{
		myBannerBox.gameObject.SetActive (status);
	}
    void Start()
    {
        bannerBoxStat = true;
        commandTracker = 0;
        Instance = this;
		SceneId = new List<string>();
        myCommand = new List<Commands>();
        //commands = new Dictionary<string, List<Command>>();
        //temporaryCommandsHolder = new List<Command>();
    }
    public void RegisterID(string id)
	{
		SceneId.Add( id );
	}

    public void AddCommand(Commands command)
    {
        myCommand.Add(command);
    }
    public void PrintData()
    {
		print("Coversation : " + SceneId[0]);
        for( int i = 0; i < myCommand.Count; i++ )
        {
            myCommand[i].PrintData();
        }
    }
    void Update()
    {
		if(commandTracker < myCommand.Count)
		{
			if (myCommand [commandTracker].ExecuteCommand ()) 
			{
				commandTracker++;
				Debug.Log("Command Tracker Status : " + commandTracker);
			} 
		}
		else if (commandTracker == myCommand.Count)
		{
			/*
				 * Destroy everything
				 */
			for (int i = 0; i < myCommand.Count; i++) {
				myCommand [i].Destroy ();
			}
			//print( "Waiting for time command" );
		} 
		else
		{

		}
    }
}

//public class ShowCharacterCommand : Command
//{
//    string CharacterName;
//    string SpawnLocation;
//    public override bool ExecuteCommand()
//    {
//        CharacterManager.Positions myPos = CharacterManager.Positions.Offscreen;
//        switch(SpawnLocation)
//        {
//        case "Offscreen":
//        myPos = CharacterManager.Positions.Offscreen;
//        break;
//        case "Left1":
//        myPos = CharacterManager.Positions.Left1;
//        break;
//        case "Left2":
//        myPos = CharacterManager.Positions.Left2;
//        break;
//        case "Centre":
//        myPos = CharacterManager.Positions.Centre;
//        break;
//        case "Right1":
//        myPos = CharacterManager.Positions.Right1;
//        break;
//        case "Right2":
//        myPos = CharacterManager.Positions.Right2;
//        break;
//        }
//        CharacterManager.Instance.ChangePosition(CharacterName, myPos);
//        return true;
//    }
//    public override void PrintData()
//    {
//        Debug.Log("Show Character Command\nCharacter Name : " + CharacterName.ToString() + " ::Spawn Location : " + SpawnLocation.ToString());
//    }
//    public string GetCharacter()
//    {
//        return CharacterName;
//    }
//    public string GetSpawnLocation()
//    {
//        return SpawnLocation;
//    }
//    public void SetCharacterName( string name )
//    {
//        CharacterName = name;
//    }
//    public void SetSpawnLocation( string location )
//    {
//        SpawnLocation = location;
//    }
//}
//public class WaitForTimeCommand : Command
//{
//    float waitingTime = 0;
//    float totalTime = 0;
//    public override void PrintData()
//    {
//        Debug.Log( "WaitForTimeCommand\nTime : " + waitingTime );
//    }
//    public void SetTime( float t )
//    {
//        waitingTime = t;
//    }
//    public float GetTime()
//    {
//        return waitingTime;
//    }
//    public override bool ExecuteCommand()
//    {
//        Debug.Log( "[Wait For Time]Time Before wait : " + Time.time );
//        totalTime += Time.deltaTime;
//        //StartCoroutine(Wait());
//        Debug.Log( "[Wait For Time]Time After Wait Function is done :" + Time.time );
//        return totalTime >= waitingTime;
//    }
//    private IEnumerator Wait()
//    {
//        yield return new WaitForSeconds(waitingTime);
//        Debug.Log( "[Wait For Time] Time on wait function : " + Time.time );
//    }
//}
//public class ShowTextCommand : Command
//{
//    string conversationTag = "";
    
//    public override bool ExecuteCommand()
//    {
//        CommandManager.Instance.SetTextHolder(DialogueHolder.Instance.GetDialogue( conversationTag ).ToString());
//        return true;
//    }
//    public override void PrintData()
//    {
//        Debug.Log( "ShowTextCommand\nconversationTag : " + conversationTag );
//    }
//    public void SetConversation( string _conversationTag )
//    {
//        conversationTag = _conversationTag;
//    }
//    public string GetConversationTag()
//    {
//        return conversationTag;
//    }
//}
//public class WaitForActionCommand : Command
//{
//    string actionTag = "";
//    public override void PrintData()
//    {
//        Debug.Log( "actionTag\nActionTag : " + actionTag );
//    }
//    public void SetAction(string _actionTag)
//    {
//        actionTag = _actionTag;
//    }
//    public string GetAction()
//    {
//        return actionTag;
//    }
//    public override bool ExecuteCommand()
//    {
//        if( actionTag == "LClick" && Input.GetMouseButtonUp( 0 ) )
//        {
//            return true;
//        }
//        else if( actionTag == "RClick" && Input.GetMouseButtonUp( 1 ) )
//        {
//            return true;
//        }
//        return false;
//    }
//}
//public class LocationCommand : Command
//{
//    string location = "";
//    public override bool ExecuteCommand()
//    {
//        return true;
//    }
//    public override void PrintData()
//    {
//        Debug.Log( "LocationCommand\nlocation : " + location );
//    }
//    public void SetLocation(string _location)
//    {
//        location = _location;
//    }
//    public string GetLocation()
//    {
//        return location;
//    }
//}