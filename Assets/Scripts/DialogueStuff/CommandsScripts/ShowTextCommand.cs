using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ShowTextCommand : Commands
{
	bool InitialSetup = true;
	int indexPassed = 0;
	float timeTracker = 0;
    // TODO(jesse): Make set speed command
    float speed = 0.015f;
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
    bool pause = false;
    bool skipCheck = false;
    public override bool ExecuteCommand()
    {
        if( InitialSetup == true )
        {
            CommandManager.Instance.TextBoxSwitch( true );
            CommandManager.Instance.TextSwitch( true );
            CommandManager.Instance.SetTextHolder( "" );
            CommandManager.Instance.SetNameIntoNameBox( DialogueHolder.Instance.GetCharacterNameFromToken( conversationTag ) );
            InitialSetup = false;
            return false;
        }
        // NOTE(jesse): This is so you can't go to next dialogue when menu is open
        if( !MenuManager.instance.GetMenuActive() )
        {
            if ((indexPassed < DialogueHolder.Instance.GetDialogue(conversationTag).Length || waitForTime == true || pause == true)) 
		    {
                if( waitForTime == true )
                {
                    UpdateTime();
                }
                else if( pause == true  )
                {
                    if( Input.GetMouseButtonUp( 0 ) )
                    {
                            pause = false;
                    }
                }
                else if(SceneManager.Instance.GetCanSkip())
                {
                    if( Input.GetMouseButtonDown( 0 ) )
                    { 
                        string passedString = "";
                        bool checkSpecial = false;
                        skipCheck = true;
                        for( int i = 0; i < DialogueHolder.Instance.GetDialogue( conversationTag ).Length; i++ )
                        {
                            if(DialogueHolder.Instance.GetDialogue( conversationTag )[i] == '['
                                ||DialogueHolder.Instance.GetDialogue( conversationTag )[i] == ']')
                            {
                                checkSpecial = !checkSpecial;
                            }
                            else if(checkSpecial == false)
                            {
                                passedString += DialogueHolder.Instance.GetDialogue( conversationTag )[i];
                            }
                        }
                        CommandManager.Instance.SetTextHolder( passedString );
                        indexPassed = DialogueHolder.Instance.GetDialogue( conversationTag ).Length;
                    }
                }
            }
            if(indexPassed < DialogueHolder.Instance.GetDialogue(conversationTag).Length)
            {
			    if(timeTracker >= speed)
			    {
                    if( indexPassed >= DialogueHolder.Instance.GetDialogue( conversationTag ).Length )
                    {
                        Debug.Log( "index length : " + indexPassed + " conversation length : " + DialogueHolder.Instance.GetDialogue( conversationTag ).Length );
                        Debug.Break();
                    }
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
                if( Input.GetMouseButtonDown( 0 ) && skipCheck == false)
                {
                        return true;
                }
                else
                {
                    skipCheck = false;
                    return false;
                }
		    }
        }
        return false;
    }
    void ParseCustomTextCommand()
    {
        string commandTag = "";
        string commandValue = "";
        //bypass '['
        indexPassed++;
        while( DialogueHolder.Instance.GetDialogue( conversationTag )[indexPassed] != ' ' && DialogueHolder.Instance.GetDialogue( conversationTag )[indexPassed] != ']')
        {
            commandTag += DialogueHolder.Instance.GetDialogue(conversationTag)[indexPassed];
            indexPassed++;
        }
        char test = DialogueHolder.Instance.GetDialogue( conversationTag )[indexPassed];
        Debug.Log( "test : [" + test +"]");
        //Debug.Break();
        if( DialogueHolder.Instance.GetDialogue( conversationTag )[indexPassed] == ']' )
        {
            indexPassed++;
        }
        else
        { 
            //bypass ' '
            indexPassed++;
            while(DialogueHolder.Instance.GetDialogue(conversationTag)[indexPassed] != ']')
            {
                commandValue += DialogueHolder.Instance.GetDialogue( conversationTag )[indexPassed];
                indexPassed++;
            }
            //bypass ']'
            indexPassed++;
        }
        RegisterTextCommand(commandTag.ToLower(), commandValue);
    }
    void RegisterTextCommand(string _tag, string _value)
    {
        switch( _tag )
        {
        case "time":
        SetWaitForTime(float.Parse(_value));
        break;
        case "pause":
        Debug.Log("[Show Text Command]pause command : " + _tag);
        pause = true;
        break;
        case "eff":
        FXManager.Instance.Spawn( _value );
        break;
        default:
        Debug.Log("[Show Text Command]command not found command : " + _tag);
        break;
        }
    }
    void UpdateTime()
    {
        totalTime += Time.deltaTime;
        Debug.Log("waiting for time");
        if(totalTime >= time)
        {
            waitForTime = false;
            time = 0;
            totalTime = 0;
        }
    }
    void SetWaitForTime(float _t)
    {
        time = _t;
        waitForTime = true;
    }
    void PassTextToCommandManager()
    {
        AudioPlayer.instance.PlayBlip( !isMale );
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
