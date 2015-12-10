using UnityEngine;
using System.Collections;

public class ShowTextCommand : Commands
{
	bool InitialSetup = true;
	int indexPassed = 0;
	float timeTracker = 0;
	float speed = 0.05f;
    string conversationTag = "";
	char passedChar = '\0';

    public override bool ExecuteCommand()
    {
		if (InitialSetup == true) 
		{

            //myAudio = ; //new AudioSource("audio");
            //myAudio.transform.position = Vector3.zero;
            //myAudio.clip = Resources.Load( "Audio/TypeWritterSound" ) as AudioClip;
			CommandManager.Instance.TextBoxSwitch (true);
			CommandManager.Instance.TextSwitch (true);
			CommandManager.Instance.SetTextHolder("");
			InitialSetup = false;
		}
		if (indexPassed < DialogueHolder.Instance.GetDialogue(conversationTag).ToString().Length) 
		{
			if(timeTracker >= speed)
			{
				AudioPlayer.instance.Play();
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

        //CommandManager.Instance.SetTextHolder( DialogueHolder.Instance.GetDialogue( conversationTag ).ToString() );
        //return true;
		//CommandManager.Instance.TextBoxSwitch (true);
		//CommandManager.Instance.TextSwitch (true);
        //CommandManager.Instance.SetTextHolder( DialogueHolder.Instance.GetDialogue( conversationTag ).ToString() );
        //return true;
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
