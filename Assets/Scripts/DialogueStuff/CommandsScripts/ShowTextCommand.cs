using UnityEngine;
using System.Collections;

public class ShowTextCommand : Commands
{

    string conversationTag = "";

    public override bool ExecuteCommand()
    {
		CommandManager.Instance.TextBoxSwitch (true);
		CommandManager.Instance.TextSwitch (true);
        CommandManager.Instance.SetTextHolder( DialogueHolder.Instance.GetDialogue( conversationTag ).ToString() );
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
	public override void Destroy()
	{
		CommandManager.Instance.TextBoxSwitch (false);
		CommandManager.Instance.TextSwitch (false);
	}
}
