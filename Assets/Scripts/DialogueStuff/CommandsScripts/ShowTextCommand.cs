using UnityEngine;
using System.Collections;
class ConversationCommand
{
    int indexStart = 0;
    int indexEnd = 0;
    string command = "";
    public void SetConversationCommand(int _indexStart, int _indexEnd, string _command)
    {
        indexStart = _indexStart;
        indexEnd = _indexEnd;
        command = _command;
    }
}
public class ShowTextCommand : Commands
{
	bool InitialSetup = true;
	int indexPassed = 0;
	float timeTracker = 0;
    // TODO(jesse): Make set speed command
	float speed = 0.035f;
    string conversationTag = "";
    string conversation = "";
	char passedChar = '\0';
    bool isMale = false;

    public override bool ExecuteCommand()
    {
		if (InitialSetup == true) 
		{
			CommandManager.Instance.TextBoxSwitch (true);
			CommandManager.Instance.TextSwitch (true);
			CommandManager.Instance.SetTextHolder("");
            //conversation = DialogueHolder.Instance.GetDialogue(conversationTag);
            LookForCommand();
			InitialSetup = false;
		}
		if (indexPassed < conversation.Length) 
		{
			if(timeTracker >= speed)
			{
				AudioPlayer.instance.PlayBlip(!isMale);
				passedChar = conversation[indexPassed];
				CommandManager.Instance.AddCharIntoTextHolder(passedChar);
				timeTracker = 0;
				indexPassed++;
				//CommandManager.Instance.SetTextHolder( DialogueHolder.Instance.GetDialogue( conversationTag ).ToString() );
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
    void LookForCommand()
    {
        bool registerCommand = false;
        string command = "";
        for( int i = 0; i < DialogueHolder.Instance.GetDialogue( conversationTag ).Length; i++ )
        {
            if( registerCommand == false )
            {
                if( DialogueHolder.Instance.GetDialogue( conversationTag )[i] == '[' )
                {
                    registerCommand = true;
                }
                else
                {
                    conversation += DialogueHolder.Instance.GetDialogue( conversationTag )[i];
                }
            }
            else
            {
                //Register Text Command here
                if( DialogueHolder.Instance.GetDialogue( conversationTag )[i] != ']' )
                {
                    command += DialogueHolder.Instance.GetDialogue( conversationTag )[i];
                }
                else
                {
                    registerCommand = false;
                    //got both of command
                    string[] commandAndData = command.Split(' ');
                    command = "";
                    Debug.Log(commandAndData);
                }
            }
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
