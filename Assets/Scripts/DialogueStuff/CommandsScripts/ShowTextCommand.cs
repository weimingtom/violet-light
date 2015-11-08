using UnityEngine;
using System.Collections;

public class ShowTextCommand : Commands
{
	float speed = 0.2f;
	float timeTracker = 0.0f;
	bool InitialSetup = true;
    string conversationTag = "";
	char passedChar = '\0';
	int indexPassed = 0;
    public override bool ExecuteCommand()
    {
<<<<<<< HEAD
		if (InitialSetup == true) 
		{
			CommandManager.Instance.TextBoxSwitch (true);
			CommandManager.Instance.TextSwitch (true);
			CommandManager.Instance.SetTextHolder("");
			InitialSetup = false;
		}
		if (indexPassed < DialogueHolder.Instance.GetDialogue(conversationTag).ToString().Length) 
		{
			if(timeTracker >= speed)
			{
				passedChar = DialogueHolder.Instance.GetDialogue(conversationTag).ToString()[indexPassed];
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
=======
        CommandManager.Instance.SetTextHolder( DialogueHolder.Instance.GetDialogue( conversationTag ).ToString() );
        return true;
>>>>>>> origin/master
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
