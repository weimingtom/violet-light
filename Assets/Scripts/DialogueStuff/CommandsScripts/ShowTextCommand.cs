using UnityEngine;
using System.Collections;

public class ShowTextCommand : Commands
{
	bool InitialSetup = true;
	int indexPassed = 0;
	float timeTracker = 0;
    // TODO(jesse): Make set speed command
    float speed = 0.5f;
    //0.035f
    string conversationTag = "";
    string conversation = "";
	char passedChar = '\0';
    bool isMale = false;

    string htmlCommandFront = "";
    string htmlCommandBack = "";
    int htmlCommandCount = 0;
    bool htmlCheck = true;

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
            
			if(timeTracker >= speed)
			{
				AudioPlayer.instance.PlayBlip(!isMale);
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
                else
                {
                    //append html command based on how many command
                    CommandManager.Instance.AddCharIntoTextHolder( passedChar );
                    timeTracker = 0;
                    indexPassed++;
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
			return true;
		}
    }
    void RegisterHtmlCommand()
    {
        string temp = "";
        htmlCommandCount ++;
        while(DialogueHolder.Instance.GetDialogue(conversationTag)[indexPassed] != '>')
        {
            temp += DialogueHolder.Instance.GetDialogue( conversationTag )[indexPassed];
            indexPassed++;
        }
        temp += '>';
        htmlCommandFront += temp;
        temp.Insert(1, "/");
        htmlCommandBack += temp;
        indexPassed++;
    }
    void UnRegisterHtmlCommand()
    {
        htmlCommandCount --;
        int count = htmlCommandFront.Length - 1;
        char[] remover;
        bool check = true;
        int charIndex = 0;
        while( check )
        {
            remover[charIndex] = htmlCommandFront[count];
            if( htmlCommandFront[count] == '<' )
            {
                check = false;
            }
            htmlCommandFront.Remove( count );
            count = htmlCommandFront.Length - 1;
        }
        check = true;
    }
    void RunTextCommand(string[] _command)
    {

        switch( _command[0].ToLower() )
        {
        case "size":
        ChangeSize( _command[1] );
        break;
        case "color":
        break;
        case "time":
        break;
        case "eff":
        break;
        case "sfx":
        break;
        default:
        break;
        }
    }
    void ChangeSize( string _size )
    {
        switch( _size.ToLower() )
        {
        case "small":
        break;
        case "big":
        break;
        case "normal":
        break;
        }
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
