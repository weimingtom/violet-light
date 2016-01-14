using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ShowTextCommand : Commands
{
	bool InitialSetup = true;
	int indexPassed = 0;
	float timeTracker = 0;
    // TODO(jesse): Make set speed command
    float speed = 0.035f;
    //0.035f
    string conversationTag = "";
    string conversation = "";
	char passedChar = '\0';
    bool isMale = false;
    List<string> htmlFront = new List<string>();
    List<string> htmlBack = new List<string>();
    
    //time support
    float time = 0;
    float totalTime = 0;
    bool waitForTime = false;
    public override bool ExecuteCommand()
    {
		if (InitialSetup == true) 
		{
			CommandManager.Instance.TextBoxSwitch (true);
			CommandManager.Instance.TextSwitch (true);
			CommandManager.Instance.SetTextHolder("");

			InitialSetup = false;
		}
		if (indexPassed < DialogueHolder.Instance.GetDialogue(conversationTag).Length) 
		{
            if( waitForTime == true )
            {
                return false;
            }
			if(timeTracker >= speed)
			{
				
                passedChar = DialogueHolder.Instance.GetDialogue( conversationTag )[indexPassed];
                //check if it is html or not
                if( passedChar == '<'
                    && DialogueHolder.Instance.GetDialogue( conversationTag )[indexPassed + 1] != '/' )
                {
                    //Add command
                    RegisterHtmlCommand();
                }
                else if( passedChar == '<'
                    && DialogueHolder.Instance.GetDialogue( conversationTag )[indexPassed + 1] == '/' )
                {
                    //delete command
                    UnRegisterHtmlCommand();
                }
                else if(passedChar == '[')
                {
                    //register custom command
                    ParseCustomTextCommand();
                }
                else
                {
                    PassTextToCommandManager();
                }
            }
			else
			{
				timeTracker += Time.deltaTime;
			}
			return false;
		}
		else
		{
            if( Input.GetMouseButtonUp( 0 ) )
            {
                return true;
            }
            else
            {
                return false;
            }
		}
    }
    void ParseCustomTextCommand()
    {
        string commandTag = "";
        string commandValue = "";
        //bypass '['
        indexPassed++;
        while( DialogueHolder.Instance.GetDialogue( conversationTag )[indexPassed] != ' ' )
        {
            commandTag += DialogueHolder.Instance.GetDialogue(conversationTag)[indexPassed];
            indexPassed++;
        }
        //bypass ' '
        indexPassed++;
        while(DialogueHolder.Instance.GetDialogue(conversationTag)[indexPassed] != ']')
        {
            commandValue += DialogueHolder.Instance.GetDialogue( conversationTag )[indexPassed];
            indexPassed++;
        }
        //bypass ']'
        indexPassed++;
        RegisterTextCommand(commandTag.ToLower(), commandValue);
    }
    void RegisterTextCommand(string _tag, string _value)
    {
        switch( _tag )
        {
        case "time":
        SetWaitForTime(float.Parse(_value));
        break;
        default :
        break;
        }
    }
    float UpdateTime()
    {
        totalTime += Time.deltaTime;

        return 0;
    }
    void SetWaitForTime(float _t)
    {
        time = _t;
    }
    void PassTextToCommandManager()
    {
        AudioPlayer.instance.PlayBlip( !isMale );
        //append html command based on how many command
        if( htmlFront.Count > 0 && htmlBack.Count > 0 )
        {
            string passedStr = "";
            for( int i = 0; i < htmlFront.Count; i++ )
            {
                passedStr += htmlFront[i];
            }
            passedStr += passedChar;
            for( int i = htmlBack.Count - 1; i >= 0; i-- )
            {
                passedStr += htmlBack[i];
            }
            CommandManager.Instance.AddStringIntoTextHolder( passedStr );
        }
        else
        {
            CommandManager.Instance.AddCharIntoTextHolder( passedChar );
        }
        timeTracker = 0;
        indexPassed++;
    }
    void RegisterHtmlCommand()
    {
        //refactorized with list
        string frontHolder = "";
        string backholder = "";
        string temp = "";

        while(DialogueHolder.Instance.GetDialogue(conversationTag)[indexPassed] != '>')
        {
            temp += DialogueHolder.Instance.GetDialogue( conversationTag )[indexPassed];
            indexPassed++;
        }
        temp += '>';
        frontHolder += temp;
        switch( temp[1] )
        {
        case 's':
        backholder = "</size>";
        break;
        case 'c':
        backholder = "</color>";
        break;
        default:
        backholder += temp.Insert( 1, "/" );
        break;
        }
        htmlFront.Add(frontHolder);
        htmlBack.Add(backholder);
        indexPassed++;
    }
    void UnRegisterHtmlCommand()
    {
        while( DialogueHolder.Instance.GetDialogue( conversationTag )[indexPassed] != '>' )
        {
            //frontHolder += DialogueHolder.Instance.GetDialogue( conversationTag )[indexPassed];
            indexPassed++;
        }
        indexPassed++;
        htmlFront.RemoveAt(htmlFront.Count-1);
        htmlBack.RemoveAt(htmlBack.Count-1);
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
	public override bool Destroy()
	{
		CommandManager.Instance.TextBoxSwitch (false);
		CommandManager.Instance.TextSwitch (false);
		CommandManager.Instance.SetTextHolder("");
        return true;
	}
}
