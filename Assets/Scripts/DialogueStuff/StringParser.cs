using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class StringParser : MonoBehaviour
{
    static public StringParser Instance;
    
    void Awake()
    {
        Instance = this;
    }
    //Dialogue Stuff
    public void RegisterDialogue(string header, string content)
    {
        string conversationHeader = header;
        string conversationContent = content;
        DialogueHolder.Instance.AddDialogue( conversationHeader.ToString(), conversationContent.ToString() );
        //print("Header :" + conversationHeader);
        //print("Content :" + conversationContent);
    }
    public void RegisterCommand( string command, ref int index, string mainString )
    {
        switch( command )
        {
        case "Conversation":
        RegisterConversationID( ref index, mainString );
        break;
        case "ShowChar":
        RegisterCharacter( ref index, mainString );
        break;
        case "WaitForTime":
        RegisterWaitForTime( ref index, mainString );
        break;
        case "ShowText":
        RegisterShowText( ref index, mainString );
        break;
        case "WaitForAction":
        RegisterWaitForAction( ref index, mainString );
        break;
        case "ShowPrompt":
        RegisterLocation( ref index, mainString );
        break;
        case "StartPuzzle":
        RegisterPuzzleID( ref index, mainString );
        break;
        case "SetDisplayName":
        RegisterSetDisplayName( ref index, mainString );
        break;
        case "ChangePose":
        RegisterChangePose( ref index, mainString );
        break;
        case "ChangePosition":
        RegisterChangePosition( ref index, mainString );
        break;
        case "If":
        break;
        case "Location":
        break;
        case "ShowIcon":
        break;
        case "End":
        break;
        }
    }
    //Note(HENDRY) : Run Parse will be used instead of parse doalogue and parse command
    public void RunParse(string _mainString)
    {
        char[] delimiterChar = {'\r', '\n'};
        string[] extractedWord = _mainString.Split(delimiterChar, System.StringSplitOptions.RemoveEmptyEntries);
        Debug.Log("[RunParse] wordLength : " + extractedWord.Length);
        for( Int16 i = 0; i < extractedWord.Length; i++ )
        {
            NewParseCommand( extractedWord[i] );
            extractedWord[i].ToLower();
            Debug.Log("[RunParse] Data[" + i +"] : " + extractedWord[i]);
        }
        //Debug.Break();
    }
    void NewParseCommand(string command)
    {
        // NOTE(Hendry): Make sure it is not a comment
        // talk to the maker of sceneProp to work with new script
        char[] delimiter;
        string[] parsedCommand;
        if( command[0] != '/' )
        {
            switch( command[0] )
            {
            case '<':
                delimiter = new char[3];
                delimiter[0] = '<';
                delimiter[1] = '>';
                delimiter[2] = '"';
                parsedCommand = command.Split(delimiter, System.StringSplitOptions.RemoveEmptyEntries);
                DialogueHolder.Instance.AddDialogue( parsedCommand[0], parsedCommand[2] );
                ShowTextCommand showText = new ShowTextCommand();
                showText.SetConversation( parsedCommand[0] );
                CommandManager.Instance.AddCommand(showText);
                break;
            default:
                delimiter = new char[1];
                delimiter[0] = ' ';
                parsedCommand = command.Split(delimiter, System.StringSplitOptions.RemoveEmptyEntries);
                switch(parsedCommand[0].ToLower())
                {
                case "bg":

                    break;
                case "bgm":

                    break;
                case "show":
                    ShowCharacterCommand character = new ShowCharacterCommand();
                    character.SetCharacterName(parsedCommand[1]);
                    character.SetSpawnLocation(parsedCommand[2]);
                    CommandManager.Instance.AddCommand(character);
                    break;
                case "pose":
                    ChangePoseCommand newPoseCommand = new ChangePoseCommand();
                    newPoseCommand.SetNewPose(parsedCommand[1], parsedCommand[2]);
                    CommandManager.Instance.AddCommand(newPoseCommand);
                    break;
                case "eff":
                    
                    break;
                case "sfx":

                    break;
                }
                break;
            }
        }
    }
    public void ParseDialogue(string mainString)
    {
        int locationCheck = 0;
        string header = "";
        string content = "";
        /*
         * The format of the dialogue text
         * "header_name""content"
         * location check is used to track -> "
         */
        for( int index = 0; index < mainString.Length; index++ )
        {
            if( mainString[index] == '"' )
            {
                locationCheck++;
            }
            else if( mainString[index] == '\r')
            {
                //advance once to reach \n
                //go out from the if statement and increment once again to reach the next line
                if( locationCheck != 4 )
                {
                    //print( "[StringParser[Parse Dialogue]]Wrong Format!!\n" );
                    Debug.Break();
                }
                else 
                {
                    index++;
                    locationCheck = 0;
                    RegisterDialogue( header, content );
                    header = "";
                    content = "";
                }
            }
            else
            {
                if( locationCheck == 1 )
                {
                    //since we only have one of -> " character assume that we have the header
                    header += mainString[index];
                }
                else if( locationCheck == 3 )
                {
                    //after location check == 3 we have the content
                    content += mainString[index];
                }
            }
        }
        if( locationCheck == 4 )
        { 
            //register the last dialogue
            RegisterDialogue(header, content);
        }
    }
    //Command Stuff
    public void ParseCommand(string mainstring)
    {
        //IMPORTANT!
        //string[] tokens = mainstring.Split( "," );
        //RunParse(mainstring);
        string commandReaded = "";
        for( int i = 0; i < mainstring.Length; i++ )
        {
            //read
            if(mainstring[i] != ':')
            {   //to be a valid command therefore the mainstring[i]
                //parse file
                commandReaded += mainstring[i].ToString();
            }
            else if(mainstring[i] == ':')
            {
                i += 2;//get rid off space
                RegisterCommand(commandReaded, ref i, mainstring);
                commandReaded = "";
            }
        }
        CommandManager.Instance.PrintData();
    }
    void RegisterCharacter( ref int index, string mainString )
    {
        string characterName = "";
        string spawnLocation = "";
        bool spaceDetected = false;
        int checkForSpecialCharacter = 0;
        while(spaceDetected == false)
        {
            if( mainString[index] != ' ' )
            {
                characterName += mainString[index];
            }
            else
            {
                spaceDetected = true;
            }
            index++;
        }
        while(checkForSpecialCharacter < 2)
        {
            if( mainString[index] != '\r' && mainString[index] != '\n' )
            {
                spawnLocation += mainString[index];
            }
            else
            {
                checkForSpecialCharacter++;
            }
            index++;
        }
        index--;
        
        ShowCharacterCommand character = new ShowCharacterCommand();
        character.SetCharacterName(characterName);
        character.SetSpawnLocation(spawnLocation);
        CommandManager.Instance.AddCommand(character);
    }

    void RegisterChangePosition( ref int index, string mainString )
    {
        string characterName = "";
        string spawnLocation = "";
        bool spaceDetected = false;
        int checkForSpecialCharacter = 0;
        while( spaceDetected == false )
        {
            if( mainString[index] != ' ' )
            {
                characterName += mainString[index];
            }
            else
            {
                spaceDetected = true;
            }
            index++;
        }
        while( checkForSpecialCharacter < 2 )
        {
            if( mainString[index] != '\r' && mainString[index] != '\n' )
            {
                spawnLocation += mainString[index];
            }
            else
            {
                checkForSpecialCharacter++;
            }
            index++;
        }
        index--;

        ChangePositionCommand character = new ChangePositionCommand();
        character.SetNewPosition( characterName, spawnLocation );
        CommandManager.Instance.AddCommand( character );
    }

    void RegisterChangePose( ref int index, string mainString )
    {
        string characterName = "";
        string spawnLocation = "";
        bool spaceDetected = false;
        int checkForSpecialCharacter = 0;
        while( spaceDetected == false )
        {
            if( mainString[index] != ' ' )
            {
                characterName += mainString[index];
            }
            else
            {
                spaceDetected = true;
            }
            index++;
        }
        while( checkForSpecialCharacter < 2 )
        {
            if( mainString[index] != '\r' && mainString[index] != '\n' )
            {
                spawnLocation += mainString[index];
            }
            else
            {
                checkForSpecialCharacter++;
            }
            index++;
        }
        index--;

        ChangePoseCommand character = new ChangePoseCommand();
        character.SetNewPose( characterName, spawnLocation );
        CommandManager.Instance.AddCommand( character );
    }

    void RegisterConversationID(ref int index, string mainString)
    {
        string temporaryID = "";
        int checkForSpecialCharacter = 0;
        while(checkForSpecialCharacter < 2)
        {
            if( mainString[index] != '\r' && mainString[index] != '\n' )
            {
                temporaryID += mainString[index];
            }
            else
            {
                checkForSpecialCharacter++;
            }
            index++;
            //it is on the next line now
        }
        index--;
        CommandManager.Instance.RegisterID(temporaryID);
    }
    void RegisterWaitForTime(ref int index, string mainString)
    {
        string time = "";
        int checkForSpecialCharacter = 0;
        while( checkForSpecialCharacter < 2 )
        {
            if( mainString[index] != '\r' && mainString[index] != '\n' )
            {
                time += mainString[index];
            }
            else
            {
                checkForSpecialCharacter++;
            }
            index++;
            //it is on the next line now
        }
        index--;
        WaitForTimeCommand waitTimeCommand = new WaitForTimeCommand();
        waitTimeCommand.SetTime( float.Parse( time.ToString() ) );
        CommandManager.Instance.AddCommand( waitTimeCommand );
    }
    void RegisterShowText( ref int index, string mainString )
    {
        string conversationTag = "";
        int checkForSpecialCharacter = 0;
        while( checkForSpecialCharacter < 2 )
        {
            if( mainString[index] != '\r' && mainString[index] != '\n' )
            {
                conversationTag += mainString[index];
            }
            else
            {
                checkForSpecialCharacter++;
            }
            index++;
        }
        index--;
        ShowTextCommand showText = new ShowTextCommand();
        showText.SetConversation( conversationTag );
        CommandManager.Instance.AddCommand(showText);
    }
    void RegisterWaitForAction(ref int index, string mainString)
    {
        string actionTag = "";
        int checkForSpecialCharacter = 0;
        while( checkForSpecialCharacter < 2 )
        {
            if( mainString[index] != '\r' && mainString[index] != '\n' )
            {
                actionTag += mainString[index];
            }
            else
            {
                checkForSpecialCharacter++;
            }
            index++;
        }
        index--;
        WaitForActionCommand action = new WaitForActionCommand();
        action.SetAction(actionTag);
        CommandManager.Instance.AddCommand(action);
    }
    void RegisterPuzzleID(ref int index, string mainString)
    {
        string id = "";
        int checkForSpecialCharacter = 0;
        while( checkForSpecialCharacter < 2 )
        {
            if( mainString[index] != '\r' && mainString[index] != '\n' )
            {
                id += mainString[index];
            }
            else
            {
                checkForSpecialCharacter++;
            }
            index++;
            //it is on the next line now
        }
        index--;
        SpawnPuzzleCommand puzzleCmd = new SpawnPuzzleCommand();
        puzzleCmd.RegisterPuzzleNumber(uint.Parse(id.ToString()));
        CommandManager.Instance.AddCommand( puzzleCmd );
    }
    void RegisterSetDisplayName( ref int index, string mainString )
    {
        string conversationTag = "";
        int checkForSpecialCharacter = 0;
        while( checkForSpecialCharacter < 2 )
        {
            if( mainString[index] != '\r' && mainString[index] != '\n' )
            {
                conversationTag += mainString[index];
            }
            else
            {
                checkForSpecialCharacter++;
            }
            index++;
        }
        index--;
        SetDisplayNameCommand showText = new SetDisplayNameCommand();
        showText.SetName( conversationTag );
        CommandManager.Instance.AddCommand( showText );
    }
    void RegisterLocation( ref int index, string mainString )
    {
        string location = "";
        int checkForSpecialCharacter = 0;
        while( checkForSpecialCharacter < 2 )
        {
            if( mainString[index] != '\r' && mainString[index] != '\n' )
            {
                location += mainString[index];
            }
            else
            {
                checkForSpecialCharacter++;
            }
            index++;
        }
        index--;
        LocationCommand locationCommand = new LocationCommand();
        locationCommand.SetLocation(location);
        CommandManager.Instance.AddCommand(locationCommand);
    }
    public void BackgroundReader( string mainString, ref Dictionary<string, string> _background)
    {
        int locationCheck = 0;
        string header = "";
        string content = "";

        for( int index = 0; index < mainString.Length; index++ )
        {
            if( mainString[index] == ' ' )//check for space
            {
                locationCheck++;
            }
            else
            {
                if( locationCheck == 0 )
                {
                    header += mainString[index];
                }
                else if( locationCheck == 1 )
                {
                    content += mainString[index];
                }
            }
        }

        if( locationCheck != 1 )
        {
            print( "wrong format" );
        }
        else
        {
            print("Stuff registered\nheader :" + header +"\ncontent :" + content);
            _background.Add( header, content );
        }
    }

    /************************* ADAM'S FILE READING ********************************/
    /******************** FOR READING THE CHARACTERS IN ***************************/

    public void ParseCharacters(string mainString)
    {

        /*  EXAMPLE STRING
         * 
         * Char Violet "Violet Light" 
         * Pose Violet neutral "Textures/Portraits/violet_neutral"
         * Pose Violet happy "Textures/Portraits/violet_happy"
         * Pose Violet sad "Textures/Portraits/violet_sad"
         * Expr Violet neutral "Textures/Expressions/violet_neutral_expr"
         * Char Alexander "Alexander Strong"
         * Pose Alexander neutral "Textures/Portraits/alex_neutral"
         * Pose Alexander happy "Textures/Portraits/alex_happy"
         * Pose Alexander sad "Textures/Portraits/alex_sad"
         * Expr Alexander curious "Textures/Expressions/alex_curious_expr"
         * 
         */

        CharacterManager CM;
        CM = CharacterManager.Instance;

        
        int i = 0;

        while (i < mainString.Length)
        {
            string what = "";
            string who = "";

            while (mainString[i] != ' ')
            {
                what += mainString[i].ToString();
                i++;
            }
            i++;
            while (mainString[i] != ' ')
            {
                who += mainString[i].ToString();
                i++;
            }

            string name = "";

            if (what == "Char")
            {
                i += 2;
                while (mainString[i] != '"')
                {
                    name += mainString[i].ToString();
                    i++;
                }
                CM.addCharacter( who, name );
                
            }
            else
            {
                i++;
                while (mainString[i] != ' ')
                {
                    name += mainString[i].ToString();
                    i++;
                }
                string filePath = "";
                i += 2;
                while (mainString[i] != '"')
                {
                    filePath += mainString[i].ToString();
                    i++;
                }

                if (what == "Pose")
                {
                    CM.AddCharacterPose( who, name, filePath );
                }
                else if (what == "Expr")
                {
                    CM.AddCharacterExpression( who, name, filePath );
                }
                else
                {
                    Debug.Log("ERROR: unrecognized command in Loading Characters! StringParser -> ParseCharacters()");
                }
                
            }

            i += 3;
        }
        CM.SetAllToNeutral();


    }

    public void ParseBackgrounds(string mainString)
    {
        /*          EXAMPLE STRING
         * alleyway "Textures/Backgrounds/case1_alley"
         * test2 "Textures/Backgrounds/backstreet_test2"
         */

        int i = 0;

        while (i < mainString.Length)
        {
            string name = "";
            string filepath = "";


            while (mainString[i] != ' ')
            {
                name += mainString[i].ToString();
                i++;
            }
            i += 2;
            while (mainString[i] != '"')
            {
                filepath += mainString[i].ToString();
                i++;
            }
            SceneManager.Instance.backgroundLookup.Add(name, filepath);
            i += 3;
        }


    }
}

