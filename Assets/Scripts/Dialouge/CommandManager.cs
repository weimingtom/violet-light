using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class CommandManager : MonoBehaviour 
{
    int destroyCount;
    bool done;

    public string correctItem { get; set; }
    public int presentItemIndex { get; set; }

    public bool prompt { get; set; }
    public int testimonyItemIndex { get; set;}

    public Text myTextHolder;
    public Text myNameHolder;

    public GameObject myBannerBox;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject pushButton;

    static public CommandManager Instance;

    int commandTracker;
    List<Commands> myCommand;

    public bool next { get; set; }
    public bool back { get; set; }
    public bool testimonyDone { get; set; }
    public bool push { get; set; }

    //false command
    public Dictionary<string, ShowTextCommand> falseCommand { get; set; }
    public string falseDialogueName { get; set; }
    bool showFalseDialogue = false;

    void UpdateButton()
    {
        if( commandTracker > 0 )
        {
            if( myCommand[commandTracker].commandTag == "testimonycommand" )
            {
                SetTestimonyButton( true );
            }
            else
            {
                SetTestimonyButton( false );
            }
        }
        else
        {
            SetTestimonyButton( false );
        }
    }
    
    void SetTestimonyButton(bool toggle)
    {
        leftButton.SetActive( toggle );
        rightButton.SetActive( toggle );
        pushButton.SetActive(toggle);
    }
    public void Terminate()
    {
        commandTracker = myCommand.Count;
    }
    public void SetNamePosition(CharacterManager.Positions pos)
    {
        if( (pos == CharacterManager.Positions.Left1) || (pos == CharacterManager.Positions.Left2) )
        {
            myNameHolder.alignment = TextAnchor.MiddleLeft;
        }
        else if( (pos == CharacterManager.Positions.Right1) || (pos == CharacterManager.Positions.Right2) )
        {
            myNameHolder.alignment = TextAnchor.MiddleRight;
        }
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
        falseCommand = new Dictionary<string, ShowTextCommand>();
        push = false;
        myTextHolder.supportRichText = true;
        destroyCount = 0;
        done = false;
        commandTracker = 0;
        Instance = this;
        myCommand = new List<Commands>();
    }

    //testimony stuff
    public void PushButton()
    {
        if( push == false )
        {
            push = true;
        }
    }

    void ResetNextBackBool()
    {
        next = false;
        back = false;
    }

    public void NextButton()
    {
        next = true;
    }

    public void BackButton()
    {
        back = true;
    }

    public void AddCommand(Commands command)
    {
        myCommand.Add(command);
    }
    // prompt is constructed by
    // option, and present stuff
    public void CheckItem(string itemName)
    {
        //therefore it is presenting on the scene
        if( prompt == true )
        {
            
        }
        if( itemName.ToLower() == correctItem.ToLower() )
        {
            //present in scene
            if( presentItemIndex == -1 )
            {
                //TODO(Jesse) : Load something

            }//present in testimony
            else if( presentItemIndex != -1  )
            {
                //check if it is presented in corret text coordinate
                if( testimonyItemIndex == presentItemIndex )
                {
                    //advance when correct item is presented
                    commandTracker++;
                }
                else
                {
                    //fail
                    showFalseDialogue = true;
                }
            }
        }
        else
        {
            //do something if fail
            showFalseDialogue = true;
        }
    }

    public void Reinitialize()
    {
        SetTestimonyButton( false );
        correctItem = "none";
        presentItemIndex = -1;
        testimonyItemIndex = -1;
        destroyCount = 0;
        done = false;
        commandTracker = 0;
        myCommand.Clear();
        falseCommand.Clear();
    }

    void Update()
    {
        UpdateButton();
        switch( done )
        {
        case false:

            if( showFalseDialogue )
            {
                if( falseCommand[falseDialogueName].ExecuteCommand() )
                {
                    showFalseDialogue = false;
                }
            }
		    else if(commandTracker < myCommand.Count)
		    {
			    if ( myCommand[commandTracker].ExecuteCommand() ) 
			    {
				    commandTracker++;
			    }
                if( myCommand[commandTracker].commandTag == "testimonyCommand" )
                {
                    ResetNextBackBool();
                }
		    }
		    else if (commandTracker == myCommand.Count)
		    {
                if( destroyCount < myCommand.Count
                    && myCommand[destroyCount].Destroy())
                {
                    destroyCount++;
                }
                else if(destroyCount == myCommand.Count)
                {
                    SetTestimonyButton( false );
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
