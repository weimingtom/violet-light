using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class CommandManager : MonoBehaviour 
{
    int destroyCount;
    bool done;

    public string dialogueToLoad { get; set; }
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
    //testimony stuff
    Dictionary<int, List<Commands>> pushCommand;
    List<int> pushCommandCoordinate = List<int>();
    /////
    public bool next { get; set; }
    public bool back { get; set; }
    public bool testimonyDone { get; set; }
    public bool push { get; set; }

    //false command
    public Dictionary<string, ShowTextCommand> falseCommand { get; set; }
    public string falseDialogueName { get; set; }
    bool showFalseDialogue = false;

    void Awake()
    {
        Instance = this;
        push = false;
        myTextHolder.supportRichText = true;
        destroyCount = 0;
        done = false;
        commandTracker = 0;
        falseCommand = new Dictionary<string, ShowTextCommand>();
        myCommand = new List<Commands>();
        pushCommand = new Dictionary<int, List<Commands>>();
    }

    void UpdateButton()
    {
        if( commandTracker > 0 && commandTracker < myCommand.Count )
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

    public void AddPushCommand( Commands command)
    {
        int commandCount = myCommand.Count - 1;
        if( !pushCommand.ContainsKey( commandCount ) )
        {
            List<Commands> temporaryCommand = new List<Commands>();
            temporaryCommand.Add( command );
            pushCommand.Add( commandCount, temporaryCommand );
        }
        else
        {
            pushCommand[commandCount].Add( command );
        }
    }

    // prompt is constructed by
    // option, and present stuff
    public void CheckItem(string itemName)
    {
		if(!myBannerBox.gameObject.activeInHierarchy)
        {
            string itemFileName = SceneManager.Instance.GetQuestStage() + "_" + SceneManager.Instance.GetSceneName() + "_" +  SceneManager.Instance.GetChar()  ;
            if( FileReader.Instance.IsScene( itemFileName + "_" + itemName ) )
            {
                FileReader.Instance.LoadScene( itemFileName + "_" + itemName );
            }
            else
            {
                FileReader.Instance.LoadScene( itemFileName + "_item");            
            }
        }
        else if( itemName.ToLower() == correctItem.ToLower() )
        {
            //check if it is presented in corret text coordinate
            if( testimonyItemIndex == presentItemIndex )
            {
                //advance when correct item is presented
                //commandTracker++;
                FileReader.Instance.LoadScene( dialogueToLoad );
            }
            else
            {
                //fail
                showFalseDialogue = true;
            }
        }
    }

    public void Reinitialize()
    {
        showFalseDialogue = false;
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
                    myCommand[commandTracker].ExecuteCommand();
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
