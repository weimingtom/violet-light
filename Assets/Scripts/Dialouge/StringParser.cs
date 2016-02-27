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

    //Note(HENDRY) : Run Parse will be used instead of parse doalogue and parse command
    public void RunRegisterCharacterCode( string _address, ref Dictionary<string, string> _characterDict )
    {
        char[] delimiterChar = { '\r', '\n' };
        string[] extractedWord = (Resources.Load( _address ) as TextAsset).ToString().Split( delimiterChar, System.StringSplitOptions.RemoveEmptyEntries );
        string[] passedChar;
        for( int i = 0; i < extractedWord.Length; i++ )
        {
            passedChar = extractedWord[i].Split( '-' );
            _characterDict.Add( passedChar[0].ToLower(), passedChar[1] );
        }
    }

    public void RunParse( string _mainString, bool isFalseItem = false )
    {
        if( isFalseItem )
        {
            ParseFalseItem( _mainString );
        }
        else
        {
            bool testimony = false;
            char[] delimiterChar = { '\r', '\n' };
            string[] extractedWord = _mainString.Split( delimiterChar, System.StringSplitOptions.RemoveEmptyEntries );
            Debug.Log( "[RunParse] wordLength : " + extractedWord.Length );
            if( extractedWord[0][0] == '[' )
            {
                testimony = true;
            }
            if( testimony )
            {
                TestimonyCommand tesCmd = new TestimonyCommand();
                for( Int16 i = 0; i < extractedWord.Length; i++ )
                {
                    ParseCommand( ref tesCmd, extractedWord[i] );
                    extractedWord[i].ToLower();
                    Debug.Log( "[RunParse] Data[" + i + "] : " + extractedWord[i] );
                }
            }
            else
            {
                for( Int16 i = 0; i < extractedWord.Length; i++ )
                {
                    ParseCommand( extractedWord[i] );
                    extractedWord[i].ToLower();
                    Debug.Log( "[RunParse] Data[" + i + "] : " + extractedWord[i] );
                }
            }
        }
    }

    public void ParseFalseItem( string str )
    {
        char[] delimiterChar = { '\r', '\n' };
        string[] command = str.Split( delimiterChar, System.StringSplitOptions.RemoveEmptyEntries );
        for( int i = 0; i < command.Length; i+= 2 )
        {
           CommandManager.Instance.falseCommand[command[0]] = RegisterTextCommand( str );
        }
    }

    void ParseCommand( ref TestimonyCommand tes, string str )
    {
        string[] extracted; 
        if( Char.ToLower( str[0] ) == 't' )
        {
            char[] delimiter = { ' ', ']', '[' };
            extracted = str.Split( delimiter );
            CommandManager.Instance.correctItem = extracted[2];
            CommandManager.Instance.presentItemIndex = int.Parse(extracted[1]);
            //tes.SetItem( int.Parse( extracted[1] ), extracted[2] );
        }
        else
        {
            char[] delimiter = { '+', '$', '"' };
            switch( str[0] )
            {
            case '+':
            extracted = str.Split( delimiter, System.StringSplitOptions.RemoveEmptyEntries );
            tes.AddMainStatement( extracted[0], extracted[1] );
            break;

            case '-':
            delimiter[0] = '-';
            extracted = str.Split( delimiter, System.StringSplitOptions.RemoveEmptyEntries );
            tes.AddPushStatement( extracted[0], extracted[1] );
            break;
            case '$':
            extracted = str.Split( delimiter, System.StringSplitOptions.RemoveEmptyEntries );
            if( extracted.Length == 2 )
            {
                tes.AddEndStatement( extracted[0], extracted[1] );
            }
            else if( extracted.Length == 3 )
            {
                tes.AddEndStatement( extracted[0], extracted[1] );
            }
            break;
            default:
            CustomCommand( str );
            break;
            }
        }
    }

    void ParseCommand( string command )
    {
        // NOTE(Hendry): Make sure it is not a comment
        // talk to the maker of sceneProp to work with new script
        if( command[0] != '/' && command[0] != ' ' )
        {
            if( command[0] == '$' )
            {
                CommandManager.Instance.AddCommand( RegisterTextCommand( command ) );
            }
            else
            {
                CustomCommand( command );
            }
        }
    }

    ShowTextCommand RegisterTextCommand( string main )
    {
        char[] delimiter;
        string[] parsedCommand;
        delimiter = new char[2];
        delimiter[0] = '$';
        delimiter[1] = '"';
        parsedCommand = main.Split( delimiter, System.StringSplitOptions.RemoveEmptyEntries );
        if( parsedCommand.Length == 3 )
        {
            DialogueHolder.Instance.AddDialogue( ref parsedCommand[0], parsedCommand[2] );
        }
        else if( parsedCommand.Length == 2 )
        {
            DialogueHolder.Instance.AddDialogue( ref parsedCommand[0], parsedCommand[1] );
        }
        else
        {
            Debug.Log( "[Parse Command]Unexpected number of character" );
        }
        ShowTextCommand showText = new ShowTextCommand();
        showText.SetConversation(parsedCommand[0].ToLower());
        return showText;
    }

    void CustomCommand( string command )
    {
        char[] delimiter = new char[1];
        delimiter[0] = ' ';
        string[] parsedCommand = command.Split( delimiter, System.StringSplitOptions.RemoveEmptyEntries );
        switch( parsedCommand[0].ToLower() )
        {
        //NOTE(Hendry)::Add command here
        case "bg":
        break;

        case "bgm":
        break;

        case "show":
        ShowCharacterCommand character = new ShowCharacterCommand();
        if( parsedCommand.Length == 3 )
        {
            character.SetCharacterName( parsedCommand[1].ToLower() );
            character.SetSpawnLocation( parsedCommand[2].ToLower() );
            CommandManager.Instance.AddCommand( character );
        }
        break;

        case "pose":
        ChangePoseCommand newPoseCommand = new ChangePoseCommand();
        newPoseCommand.SetNewPose( parsedCommand[1].ToLower(), parsedCommand[2].ToLower() );
        CommandManager.Instance.AddCommand( newPoseCommand );
        break;
		
		case "icon":
        if( parsedCommand.Length == 3 )
        {
            bool set = false;
            if( parsedCommand[2].ToLower() == "on" )
            {
                set = true;
            }
            else if( parsedCommand[2].ToLower() == "off" )
            {
                set = false;
            }
            else
            {
                Debug.Log( "[String Parser]<color:red>wrong command</color> !! the format is icon iconName on/off" );
                Debug.Break();
            }
            LocationManager.Instance.SetButton(parsedCommand[1], set);
        }
        else
        {
            Debug.Log( "[String Parser]<color:red>wrong command</color> !! the format is icon iconName on/off" );
            Debug.Break();
        }
		break;
        
		case "eff":
        EffectCommand newEffect = new EffectCommand();
        newEffect.SetEffect( parsedCommand[1] );
        CommandManager.Instance.AddCommand( newEffect );
        break;

        case "item":
        ItemManager.Instance.AddItem( parsedCommand[1].ToLower() );
        break;

        case "decisions":
        break;

        case "advquest":
        SceneManager.Instance.AdvQuest();
        break;
        }
    }

    public Dictionary<int, List<string>> ReadLocationData( string loc )
    {
        char[] delimiter = { '\r', '\n' };
        string[] data = (Resources.Load( loc ) as TextAsset).ToString().Split( delimiter, System.StringSplitOptions.RemoveEmptyEntries );
        Dictionary<int, List<string>> value = new Dictionary<int, List<string>>();
        string compare = "empty";
        int currentID = 0;
        for( int i = 0; i < data.Length; i++ )
        {
            compare = "";
            for( int j = 0; j < 2; j++ )
            {
                compare += data[i][j];
            }
            if( compare == "id" )
            {
                string[] id = data[i].ToString().Split( ' ' );
                currentID = int.Parse( id[1] );
                value[currentID] = new List<string>();
            }
            else
            {
                value[currentID].Add( data[i].ToLower() );
            }
        }
        return value;
    }

    public void BackgroundReader( string mainString, ref Dictionary<string, string> _background )
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
            print( "Stuff registered\nheader :" + header + "\ncontent :" + content );
            _background.Add( header, content );
        }
    }



    /************************* ADAM'S FILE READING ********************************/
    /******************** FOR READING THE CHARACTERS IN ***************************/

    public void ParseCharacters( string mainString )
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

        while( i < mainString.Length )
        {
            string what = "";
            string who = "";

            while( mainString[i] != ' ' )
            {
                what += mainString[i].ToString();
                i++;
            }
            i++;
            while( mainString[i] != ' ' )
            {
                who += mainString[i].ToString();
                i++;
            }
            Debug.Log( "[character manager] Adding Char: " + who + " | what: " + what );

            string name = "";

            if( what == "Char" )
            {
                i += 2;
                while( mainString[i] != '"' )
                {
                    name += mainString[i].ToString();
                    i++;
                }
                CM.addCharacter( who.ToLower(), name.ToLower() );
            }
            else
            {
                i++;
                while( mainString[i] != ' ' )
                {
                    name += mainString[i].ToString();
                    i++;
                }
                string filePath = "";
                i += 2;
                while( mainString[i] != '"' )
                {
                    filePath += mainString[i].ToString();
                    i++;
                }

                if( what == "Pose" )
                {
                    CM.AddCharacterPose( who.ToLower(), name.ToLower(), filePath );
                }
                else if( what == "Expr" )
                {
                    CM.AddCharacterExpression( who.ToLower(), name.ToLower(), filePath );
                }
                else
                {
                    Debug.Log( "ERROR: unrecognized command in Loading Characters! StringParser -> ParseCharacters()" );
                }

            }

            i += 3;
        }
        CM.SetAllToNeutral();
    }

    public void ParseBackgrounds( string mainString )
    {
        /*          EXAMPLE STRING
         * alleyway "Textures/Backgrounds/case1_alley"
         * test2 "Textures/Backgrounds/backstreet_test2"
         */
        int i = 0;
        while( i < mainString.Length )
        {
            string name = "";
            string filepath = "";


            while( mainString[i] != ' ' )
            {
                name += mainString[i].ToString();
                i++;
            }
            i += 2;
            while( mainString[i] != '"' )
            {
                filepath += mainString[i].ToString();
                i++;
            }
            SceneManager.Instance.backgroundLookup.Add( name.ToLower(), filepath );
            i += 3;
        }
    }

    public void ParseScene( string mainString )
    {
        /*      EXAMPLE STRING
         *  BG=alleyway
         *  ID=0
         *  NM=Alley Way
         *  TM=1005
         *  PF=TestArea1
         *  DONE
         */

        string bg = "", name = "", prefab = "";
        uint id = 0, time = 0;


        for( int i = 0; i < mainString.Length; ++i )
        {

            while( mainString[i] != ' ' && mainString[i] != '\r' && mainString[i] != '\n' )
            {
                char operation = ' ';
                string dogma = "";
                bool success;

                operation = mainString[i];
                i += 2;
                for( int j = ++i; mainString[j] != '\r'; ++j, ++i )
                {
                    dogma += mainString[j];
                }

                switch( operation )
                {
                case 'B':
                bg = dogma;
                break;

                case 'I':
                success = uint.TryParse( dogma, out id );
                if( !success )
                    Debug.Log( "ERROR: Could Not Parse ID from string to uint [StringParser.cs]" );
                break;

                case 'N':
                name = dogma;
                break;

                case 'T':
                success = uint.TryParse( dogma, out time );
                if( !success )
                    Debug.Log( "ERROR: Could Not Parse time(TM) from string to uint [StringParser.cs]" );
                break;


                case 'P':
                prefab = dogma;
                break;

                case 'D':
                if( bg == null || name == null || prefab == null || id == 0 || time == 0 )
                    Debug.Log( "WARNING: Some scenes contain undefined data. [StringParser.cs]" );
                SceneManager.Instance.NewScene( bg, id, name, time, prefab );
                bg = "";
                name = "";
                prefab = "";
                id = 0;
                time = 0; ;
                break;

                default:
                Debug.Log( "ERROR: Unrecognized command in Parse Scene. [String Parser.cs]" );
                break;
                }



            }
        }




    }
}

