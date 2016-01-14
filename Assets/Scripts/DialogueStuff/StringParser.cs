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
    //Note(HENDRY) : Run Parse will be used instead of parse doalogue and parse command
    public void RunParse(string _mainString)
    {
        char[] delimiterChar = {'\r', '\n'};
        string[] extractedWord = _mainString.Split(delimiterChar, System.StringSplitOptions.RemoveEmptyEntries);
        Debug.Log("[RunParse] wordLength : " + extractedWord.Length);
        for( Int16 i = 0; i < extractedWord.Length; i++ )
        {
            ParseCommand( extractedWord[i] );
            extractedWord[i].ToLower();
            Debug.Log("[RunParse] Data[" + i +"] : " + extractedWord[i]);
        }
        //Debug.Break();
    }
    void ParseCommand(string command)
    {
        // NOTE(Hendry): Make sure it is not a comment
        // talk to the maker of sceneProp to work with new script
        char[] delimiter;
        string[] parsedCommand;
        if( command[0] != '/' )
        {
            switch( command[0] )
            {
            case '$':
                delimiter = new char[3];
                delimiter[0] = '$';
                delimiter[1] = '$';
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

