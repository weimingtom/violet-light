using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class CommandManager : MonoBehaviour 
{
    public Text myTextHolder;
    public GameObject myBannerBox;
    static public CommandManager Instance;
    //counter for CommandId
	List<string> SceneId;
    //counter for command
    int commandTracker;
    List<Commands> myCommand;
	public void AddCharIntoTextHolder(char c)
	{
		myTextHolder.text += c;
	}
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
			for (int i = 0; i < myCommand.Count; i++) 
			{
				myCommand [i].Destroy ();
			}
			//print( "Waiting for time command" );
		} 
    }
}
