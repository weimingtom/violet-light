using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CommandManager : MonoBehaviour 
{
    public Text DialogueText;
    static public CommandManager Instance;
	List<string> ID;
	Dictionary<string, List<Command>> commands;
    private List<Command> temporaryCommandsHolder;
    void Start()
    {
        Instance = this;
        ID = new List<string>();
        commands = new Dictionary<string, List<Command>>();
        temporaryCommandsHolder = new List<Command>();
    }
    public void RegisterID(string id)
	{
        ID.Add( id );
	}
    public void RegisterSetOfCommand()
    {
        if( ID.Count > 0 )
        {
            commands.Add( ID[(ID.Count - 1)], temporaryCommandsHolder );
            temporaryCommandsHolder.Clear();
        }
        else
        {
            print("Please Register ID First");
            Debug.Break();
        }
    }
    public void AddCommand(Command command)
    {
        temporaryCommandsHolder.Add(command);
    }
    public void PrintData()
    {
        print("Coversation : " + ID[0]);
        for( int i = 0; i < temporaryCommandsHolder.Count; i++ )
        {
            temporaryCommandsHolder[i].PrintData();
        }
    }
    public void RunCommand()
    {
        
    }
}

abstract public class Command : MonoBehaviour
{
    public abstract void PrintData();
    public abstract void ExecuteCommand();
}
public class ShowCharacterCommand : Command
{
    string CharacterName;
    string SpawnLocation;
    public override void ExecuteCommand()
    {
        
    }
    public override void PrintData()
    {
        print("Show Character Command\nCharacter Name : " + CharacterName.ToString() + " ::Spawn Location : " + SpawnLocation.ToString());
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
    public override void PrintData()
    {
        print( "WaitForTimeCommand\nTime : " + waitingTime);
    }
    public void SetTime( float t )
    {
        waitingTime = t;
    }
    public float GetTime()
    {
        return waitingTime;
    }
    public override void ExecuteCommand()
    {
        print("[Wait For Time]Time Before wait : " + Time.time);
        StartCoroutine(Wait());
        print("[Wait For Time]Time After Wait Function is done :" + Time.time);
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitingTime);
        print( "[Wait For Time] Time on wait function : " + Time.time );
    }
}
public class ShowTextCommand : Command
{
    string conversationTag = "";
    public Text myText;
    void AssignText()
    {
        StringParser.Instance.GetDialogue( conversationTag );
        myText.text = conversationTag.ToString();
    }
    public override void ExecuteCommand()
    {
        
    }
    public override void PrintData()
    {
        print( "ShowTextCommand\nconversationTag : " + conversationTag );
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
        print( "actionTag\nActionTag : " + actionTag );
    }
    public void SetAction(string _actionTag)
    {
        actionTag = _actionTag;
    }
    public string GetAction()
    {
        return actionTag;
    }
    public override void ExecuteCommand()
    {
        
    }
    private void ActionChecker()
    {
        /*
         This function will be used to select the appropriate wait command
         */
        switch( actionTag )
        {
        case "LClick":
        WaitForLeftClick();
        break;
        case "RClick":
        break;
        }
    }
    private IEnumerator WaitForLeftClick()
    {
        bool wait = true;
        while( wait )
        {
            print("[Wait for Left Click] waiting....");
            if( Input.GetMouseButtonDown( 0 ) || Input.GetMouseButtonUp( 0 ) )
            {
                wait = false;
            }
        }
        yield return null;
    }

}
public class LocationCommand : Command
{
    string location = "";
    public override void ExecuteCommand()
    {
        
    }
    public override void PrintData()
    {
        print( "LocationCommand\nlocation : " + location );
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