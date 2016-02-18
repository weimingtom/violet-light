using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class CommandManager : MonoBehaviour 
{
    int destroyCount;
    bool done;
    public Text myTextHolder;
    public Text myNameHolder;
    public GameObject myBannerBox;
    static public CommandManager Instance;
    
    //counter for CommandId
	List<string> SceneId;
    //counter for command
    int commandTracker;
    public bool loop { get; set; }
    List<Commands> myCommand;
	public void AddCharIntoTextHolder(char c)
	{
		myTextHolder.text += c;
	}
    public void AddStringIntoTextHolder( string str )
    {
        myTextHolder.text += str;
    }
    public void SetNameIntoNameBox( string name )
    {
        myNameHolder.text = name;
    }
    public void SetTextHolder(string content)
    {
		myTextHolder.text = content;
	}
	public void TextSwitch(bool status)
	{
        myTextHolder.gameObject.SetActive( status );
        myNameHolder.gameObject.SetActive( status );
	}
	public void TextBoxSwitch(bool status)
	{
		myBannerBox.gameObject.SetActive (status);
	}

    void Start()
    {
        loop = true;
        myTextHolder.supportRichText = true;
        destroyCount = 0;
        done = false;
        commandTracker = 0;
        Instance = this;
		SceneId = new List<string>();
        myCommand = new List<Commands>();
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
    public void AdvanceCommand()
    {
        commandTracker++;
        myCommand[commandTracker].Reset();
    }
    public void RewindCommand()
    {
        if( commandTracker > 0 )
        {
            commandTracker--;
            myCommand[commandTracker].Reset();
        }
    }
    public void Reinitialize()
    {
        destroyCount = 0;
        done = false;
        commandTracker = 0;
        SceneId.Clear();
        myCommand.Clear();
    }
    void Update()
    {
        switch( done )
        {
        case false:
		    if(commandTracker < myCommand.Count)
		    {
			    if (myCommand [commandTracker].ExecuteCommand ()) 
			    {
				    commandTracker++;
                    Debug.Log("Run command no  : " + commandTracker);
			    } 
		    }
		    else if (commandTracker == myCommand.Count)
		    {

			    /*
			     * Destroy everything
                 * Added destroy count, to reversed back to 
			     */
                if( loop == false )
                {
                    if( destroyCount < myCommand.Count
                        && myCommand[destroyCount].Destroy())
                    {
                        destroyCount++;
                    }
                    else if(destroyCount == myCommand.Count)
                    {
                        SceneManager.Instance.SetInputBlocker( false );
                        done = true;
                    }
                }
		    } 
            break;

        case true:
            break;
        }
    }
}
