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
    public GameObject leftButton;
    public GameObject rightButton;
    static public CommandManager Instance;

    //counter for CommandId
	List<string> SceneId;
    //counter for command
    int commandTracker;
    List<Commands> myCommand;

    void UpdateButton()
    {
        leftButton.SetActive(false);
        rightButton.SetActive(false);
    }

    public void Terminate()
    {
        commandTracker = myCommand.Count;

    }

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
        //loop = true;
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
        bool check = true;
        while(check)
        {
            commandTracker++;
            if( commandTracker >= myCommand.Count )
            {
                commandTracker = 0;
            }
            myCommand[commandTracker].Reset();
            myCommand[commandTracker].ExecuteCommand();
            if( myCommand[commandTracker].commandTag == "showtextcommand" )
            {
                check = false;
            }
        }
    }
    public void RewindCommand()
    {
        if( commandTracker > 0 )
        {
            do
            {
                commandTracker--;
                myCommand[commandTracker].Reset();
            } while( myCommand[commandTracker].commandTag != "showtextcommand"
                    && commandTracker != 0 );
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
        UpdateButton();
        switch( done )
        {
        case false:
		    if(commandTracker < myCommand.Count)
		    {
			    if ( myCommand[commandTracker].ExecuteCommand() ) 
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
            break;

        case true:
            break;
        }
    }
}
