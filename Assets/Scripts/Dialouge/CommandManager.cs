﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class CommandManager : MonoBehaviour 
{
    static public CommandManager Instance;

    int destroyCount;
    bool done;
    public string dialogueToLoad { get; set; }
    public string correctItem { private get; set; }

    public bool prompt { get; set; }
    public int testimonyItemIndex { private get; set;}

    public bool testimonyMode { private get; set; }

    public Text myTextHolder;
    public Text myNameHolder;

    public GameObject myBannerBox;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject pushButton;

    int pushCommandTracker;
    int commandTracker;

    List<Commands> myCommand;
    //testimony stuff
    Dictionary<int, List<Commands>> myPushCommand;
    List<Commands> wrongTextCommand;
    int wrongTextCommandIndex;
    //
    public bool next { get; set; }
    public bool back { get; set; }
    public bool testimonyDone { get; set; }
    public bool runPushCommand { get; set; }


    //false command
    //public Dictionary<string, ShowTextCommand> falseCommand { get; set; }
    //public string falseDialogueName { get; set; }
    bool showFalseDialogue = false;

    void Awake()
    {
        Instance = this;
        wrongTextCommandIndex = 0;
        testimonyItemIndex = -1;
        runPushCommand = false;
        myTextHolder.supportRichText = true;
        destroyCount = 0;
        done = false;
        testimonyMode = false;
        commandTracker = 0;
        pushCommandTracker = 0;
        myCommand = new List<Commands>();
        myPushCommand = new Dictionary<int, List<Commands>>();
        wrongTextCommand = new List<Commands>();
    }

    void UpdateButton()
    {
        if( commandTracker >= 0 && commandTracker < myCommand.Count && myCommand.Count != 0)
        {
            if( testimonyMode )
            {
                if( runPushCommand == false )
                {
                    SetTestimonyButton(true);
                }
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
        pushButton.SetActive( toggle );
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
        if( runPushCommand == false )
        {
            runPushCommand = true;
        }
    }

    void ResetNextBackBool()
    {
        next = false;
        back = false;
    }

    public void NextButton()
    {
        //next = true;
        myCommand[commandTracker].Reset();
        commandTracker++;
        if( commandTracker >= myCommand.Count )
        {
            ResetMainCommand();
        }
    }

    public void BackButton()
    {
        do
        {
            myCommand[commandTracker].Reset();
            commandTracker--;
            if( commandTracker <= 0 )
            {
                commandTracker = 0;
                break;
            }
        }
        while( myCommand[commandTracker].commandTag != "showtextcommand" );
        myCommand[commandTracker].Reset();
    }

    public void AddCommand(Commands command)
    {
        myCommand.Add(command);
    }

    public void AddPushCommand( Commands command )
    {
        int commandCount = myCommand.Count - 1;
        if( !myPushCommand.ContainsKey( commandCount ) )
        {
            List<Commands> temporaryCommand = new List<Commands>();
            temporaryCommand.Add( command );
            myPushCommand.Add( commandCount, temporaryCommand );
        }
        else
        {
            myPushCommand[commandCount].Add( command );
        }
    }

    // prompt is constructed by
    // option, and present stuff
    public void CheckItem(string itemName)
    {

        string itemFileName = SceneManager.Instance.GetQuestStage() + "_" + SceneManager.Instance.GetSceneName() + "_" +  SceneManager.Instance.GetChar()  ;
        string defaultWrongItemSceneAddress = "Dialogue/false_item_presented_scene";		
        //check what type of present
        if(!myBannerBox.gameObject.activeInHierarchy )
        {
            if( FileReader.Instance.IsScene( itemFileName + "_" + itemName ) )
            {
                FileReader.Instance.LoadScene( itemFileName + "_" + itemName );
            }
            else
            {
                FileReader.Instance.LoadScene( itemFileName + "_item");            
            }
        }
        else
        {
            if( testimonyItemIndex != -1 )
            {
                //item is correct
                if( testimonyItemIndex == commandTracker && correctItem == itemName)
                {
                    //therefore load the correct scene
                    FileReader.Instance.LoadScene( dialogueToLoad );
                }
                else
                {
                    //check if there is default unique dialoue for that specifict item
                    if( Resources.Load( itemFileName + "_item" ) != null )
                    {
                        
                        wrongTextCommand = StringParser.Instance.ParseWrongCommand( itemFileName + "_item" );
                    }
                    else
                    {
                        //no unique command then load default
                        wrongTextCommand = StringParser.Instance.ParseWrongCommand( Resources.Load( defaultWrongItemSceneAddress ).ToString() );
                    }
                    showFalseDialogue = true;
                }
            }
            else
            {
                //therefore it is prompt sutff
                //assumption, it will do some mechanic as presenting item while dialogue is not presented
                if( FileReader.Instance.IsScene( itemFileName + "_" + itemName ) )
                {
                    //load the correct dialogue/scene
                    FileReader.Instance.LoadScene( itemFileName + "_" + itemName );
                }
                else
                {
                    //wrong item then do something elese
                    if( FileReader.Instance.IsScene( itemFileName + "_item" ) )
                    {
                        FileReader.Instance.LoadScene( itemFileName + "_item" );
                    }
                    else
                    {
                        //no default found load dummy
                        FileReader.Instance.LoadScene( defaultWrongItemSceneAddress );
                    }
                }
            }
        }
    }

    public void Reinitialize()
    {
        wrongTextCommandIndex = 0;
        dialogueToLoad = "";
        runPushCommand = false;
        testimonyMode = false;
        showFalseDialogue = false;
        SetTestimonyButton( false );
        correctItem = "";
        testimonyItemIndex = -1;
        pushCommandTracker = 0;
        destroyCount = 0;
        done = false;
        commandTracker = 0;
        myCommand.Clear();
        myPushCommand.Clear();
        wrongTextCommand.Clear();
    }

    void ResetMainCommand()
    {
        commandTracker = 0;
        for( int i = 0; i < myCommand.Count; i++ )
        {
            myCommand[i].Reset();
        }
    }

    void ResetPushCommand()
    {
        pushCommandTracker = 0;
        for( int i = 0; i < myPushCommand[commandTracker].Count; i++ )
        {
            myPushCommand[commandTracker][i].Reset();
        }
        runPushCommand = false;
    }
    void ResetWrongTextCommand()
    {
        wrongTextCommandIndex = 0;
        for( int i = 0; i < wrongTextCommand.Count; i++ )
        {
            wrongTextCommand[i].Reset();
        }
        showFalseDialogue = false;
    }
    void Update()
    {
        UpdateButton();
        switch( done )
        {
        case false:
        if( showFalseDialogue )
        {
            if( wrongTextCommand[wrongTextCommandIndex].ExecuteCommand() )
            {
                wrongTextCommandIndex++;
                if( wrongTextCommandIndex >= wrongTextCommand.Count )
                {
                    ResetWrongTextCommand();
                }
            }
        }
        else if( commandTracker < myCommand.Count )
        {
            if( runPushCommand )
            {
                if( myPushCommand.ContainsKey( commandTracker ) )
                {
                    if( myPushCommand[commandTracker][pushCommandTracker].ExecuteCommand() )
                    {
                        pushCommandTracker++;
                        if( pushCommandTracker >= myPushCommand[commandTracker].Count )
                        {
                            ResetPushCommand();
                        }
                    }
                }
                else
                {
                    runPushCommand = false;
                }
            }
            else
            {
                if( myCommand[commandTracker].ExecuteCommand() )
                {
                    commandTracker++;
                    if( testimonyMode && commandTracker >= myCommand.Count )
                    {
                        ResetMainCommand();
                    }
                }
            }
        }
        else if( commandTracker == myCommand.Count )
        {
            if( destroyCount < myCommand.Count
                && myCommand[destroyCount].Destroy() )
            {
                destroyCount++;
            }
            else if( destroyCount == myCommand.Count )
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
