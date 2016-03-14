using UnityEngine;
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

    public bool allTextInScreen { get; set; }
    public bool donePrompt { get; set; }
    public int testimonyItemIndex { private get; set;}

    public bool testimonyMode { get; set; }

    public Text myTextHolder;
    public Text myNameHolder;

    public GameObject myBannerBox;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject pushButton;

    int pushCommandTracker;
    int commandTracker;

    List<Commands> myCommand;

    Dictionary<int, List<Commands>> myPushCommand;
    List<Commands> wrongTextCommand;
    int wrongTextCommandIndex;
    bool checkHideUI = false;
    public bool isExamine { get; set; }
    public bool next { get; set; }
    public bool back { get; set; }
    public bool testimonyDone { get; set; }
    public bool runPushCommand { get; set; }
    bool hideMenu = true;
    public string defaultWrongItemSceneAddress = "Dialogue/false_item_presented_scene";		

    bool showFalseDialogue = false;

    void Awake()
    {
        allTextInScreen = false;
        Instance = this;
        donePrompt = false;
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
        else if( pos == CharacterManager.Positions.Centre )
        {
            myNameHolder.alignment = TextAnchor.MiddleCenter;
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
            if( allTextInScreen == true )
            {
                myCommand[commandTracker].Reset();
                commandTracker--;
            }
        }
    }

    void ResetNextBackBool()
    {
        next = false;
        back = false;
    }

    public void NextButton()
    {
        if( !runPushCommand )
        {
            myCommand[commandTracker].Reset();
            commandTracker++;
            if( commandTracker >= myCommand.Count )
            {
                ResetMainCommand();
            }
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
    public void CheckItem(string iName)
    {
        string itemName = iName.Replace(' ', '_');
        string itemFileName = SceneManager.Instance.GetQuestStage() + "_" + SceneManager.Instance.GetSceneName() + "_" +  SceneManager.Instance.GetChar()  ;
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
            MenuManager.instance.ForceCloseMenu();
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
                    MenuManager.instance.ForceCloseMenu();
                }
            }
            else
            {
                if( myCommand[commandTracker].commandTag == "openmenucommand" )
                {
                    Debug.Log("[Command Manager] Check Item COrrect item = " + correctItem + "| your item:" + itemName);
                    if( itemName.ToLower() == correctItem.ToLower() )
                    {
                        donePrompt = true;
                    }
                    else
                    {
                        Debug.Break();
                        wrongTextCommand = StringParser.Instance.ParseWrongCommand( Resources.Load( defaultWrongItemSceneAddress ).ToString() );
                        showFalseDialogue = true;
                        MenuManager.instance.ForceCloseMenu();
                        myCommand[commandTracker].Reset();
                    }
                }
            }
        }
    }

    public void Reinitialize()
    {
        checkHideUI = false;
        SceneMenuManager.instance.hideAll();
        donePrompt = false;
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
        myCommand[commandTracker].Reset();
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
        if( checkHideUI == false )
        {
            SceneMenuManager.instance.HideFromCommandManager();
            checkHideUI = true;
        }
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
                if( isExamine )
                {
                    SceneMenuManager.instance.ExamineScene();
                }
                else
                {
                    SceneMenuManager.instance.EnteredNewScene();
                }
                done = true;
            }
        }
        break;

        case true:
            break;
        }
    }

}
