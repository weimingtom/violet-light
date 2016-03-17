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
    public void RunRegisterCharacterCode( string _address, ref Dictionary<string, string> _characterDict, ref List<string> maleChar, ref List<string> femaleChar)
    {
        char[] delimiterChar = { '\r', '\n' };
        string[] extractedWord = (Resources.Load( _address ) as TextAsset).ToString().Split( delimiterChar, System.StringSplitOptions.RemoveEmptyEntries );
        string[] passedChar;
        for( int i = 0; i < extractedWord.Length; i++ )
        {
            passedChar = extractedWord[i].Split( '-' );
            if( passedChar.Length == 3 )
            {
                switch(passedChar[2].ToLower())
                {
                case "m":
                maleChar.Add(passedChar[1]);
                break;
                case "f":
                femaleChar.Add(passedChar[1]);
                break;
                }
            }
            _characterDict.Add( passedChar[0].ToLower(), passedChar[1] );
        }
    }

    public void RunParse( string mainString, bool isFalseItem = false )
    {
        bool testimony = false;
        char[] delimiterChar = { '\r', '\n', '\t' };
        string[] extractedWord = mainString.Split( delimiterChar, System.StringSplitOptions.RemoveEmptyEntries );
        Debug.Log( "[RunParse] wordLength : " + extractedWord.Length );
            
        if( extractedWord[0][0] == '['
            && (extractedWord[0][1] == 't'
            || extractedWord[0][1] == 'T'))
        {
            testimony = true;
        }
        if( testimony )
        {
            //TestimonyCommand tesCmd = new TestimonyCommand();
            for( Int16 i = 0; i < extractedWord.Length; i++ )
            {
                string passed = extractedWord[i].TrimStart('\t');
                ParseCommandTestimony( passed );
                extractedWord[i].ToLower();
                //Debug.Log( "[RunParse] Data[" + i + "] : " + extractedWord[i] );
            }
            //CommandManager.Instance.AddCommand(tesCmd);
            Debug.Log("[Run Parse] Finished Parsing!");
            //Debug.Break();
        }
        else
        {
            for( Int16 i = 0; i < extractedWord.Length; i++ )
            {
                ParseCommand( extractedWord[i]);
                extractedWord[i].ToLower();
                //Debug.Log( "[RunParse] Data[" + i + "] : " + extractedWord[i] );
            }
        }
    }

    void ParseCommandTestimony( string command )
    {
        /*
         *[0] 
         *[1][0][1] 
         *[2]
         *[3]
         */
        string[] extracted; 
        //check if
        if( command[0] == '[' && (command[1] == 't' || command[1] == 'T') )
        {
            //item//
            char[] delimiter = { ' ', ']', '[' };
            extracted = command.Split( delimiter, StringSplitOptions.RemoveEmptyEntries );
            CommandManager.Instance.testimonyMode = true;
            CommandManager.Instance.correctItem = extracted[2];
            CommandManager.Instance.testimonyItemIndex = int.Parse( extracted[1] );
            CommandManager.Instance.dialogueToLoad = extracted[3];
        }
        else
        {
            //do testimony part
            //char[] delimiter = { '\t', '+', '$', '"', '-' };
            if( command[0] == '+' && command[1] == '$' )
            {
                CommandManager.Instance.AddCommand( RegisterTextCommand( command ) );
            }
            else if( command[0] == '+' && command[1] != '$' )
            {
                CustomCommand( command,  false);
            }
            else if( command[0] == '%' && command[1] == '$' )
            {
                CommandManager.Instance.AddPushCommand( RegisterTextCommand( command ) );
            }
            else if( command[0] == '%' && command[1] != '$' )
            {
                CustomCommand( command, true );
            }
            else if( command[0] == '$' )
            {
                CommandManager.Instance.AddCommand( RegisterTextCommand( command ) );
            }
            else
            {
                ParseCommand( command );
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

    public List<Commands> ParseWrongCommand(string command)
    {
        List<Commands> commands = new List<Commands>();
        char[] delimiters = new char[3];
        delimiters[0] = '\r';
        delimiters[1] = '\n';
        string[] extractedCommandStr = command.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries);
        for( int i = 0; i < extractedCommandStr.Length; i++ )
        {
            if( extractedCommandStr[i][0] == '$' )
            {
                commands.Add(RegisterTextCommand(extractedCommandStr[0]));
            }
        }
        return commands;
    }

    ShowTextCommand RegisterTextCommand( string main )
    {
        char[] delimiter;
        string[] parsedCommand;
        delimiter = new char[4];
        //delimiter = new char[4];
        delimiter[0] = '$';
        delimiter[1] = '"';

        delimiter[2] = '+';
        delimiter[3] = '%';

        parsedCommand = main.Split( delimiter, System.StringSplitOptions.RemoveEmptyEntries );
        if( parsedCommand.Length == 3 )
        {
            DialogueHolder.Instance.AddDialogue( ref parsedCommand[0], parsedCommand[2] );
        }
        else if( parsedCommand.Length == 2 )
        {
            DialogueHolder.Instance.AddDialogue( ref parsedCommand[0], parsedCommand[1] );
        }
        ShowTextCommand showText = new ShowTextCommand();
        showText.SetConversation(parsedCommand[0].ToLower());
        return showText;
    }

    void CustomCommand( string command, bool pushCommand = false )
    {
        char[] delimiter = new char[3];
        delimiter[0] = ' ';
        delimiter[1] = '\t';
        delimiter[2] = '%';
        string[] parsedCommand = command.Split( delimiter, System.StringSplitOptions.RemoveEmptyEntries );
        switch( parsedCommand[0].ToLower() )
        {
            case "bgm":
                {

                    if (parsedCommand.Length > 1)
                    {
                        Debug.Log("<color=green>[StringParser]</color> CREATED NEW BGM COMMAND + " + parsedCommand[1].ToLower());
                        MusicCommand BGM = new MusicCommand();
                        BGM.Set(parsedCommand[1].ToLower());
                    }
                }
                break;
        //NOTE(Hendry)::Add command here
        case "bg":
        BgCommand bgc = new BgCommand();
        if( parsedCommand.Length == 2 )
        {
            bgc.SetBg(parsedCommand[1].ToLower());
            if (parsedCommand.Length == 3)
            {
                bgc.SetSpd(int.Parse(parsedCommand[2]));
            }
            if( pushCommand )
            {
                CommandManager.Instance.AddPushCommand(bgc);
            }
            else
            {
                CommandManager.Instance.AddCommand( bgc );
            }
        }

        break;

        

        case "show":
        ShowCharacterCommand character = new ShowCharacterCommand();
        if( parsedCommand.Length >= 3 )
        {
            character.SetCharacterName( parsedCommand[1].ToLower() );
            character.SetSpawnLocation( parsedCommand[2].ToLower() );
            if( pushCommand )
            {
                CommandManager.Instance.AddPushCommand( character );
            }
            else
            {
                CommandManager.Instance.AddCommand( character );
            }
            if (parsedCommand.Length >= 4)
            {
                character.SetFacing( parsedCommand[3].ToLower() );
            }
        }
        break;

        case "pose":
        ChangePoseCommand newPoseCommand = new ChangePoseCommand();
        newPoseCommand.SetNewPose( parsedCommand[1].ToLower(), parsedCommand[2].ToLower() );
        if( pushCommand )
        {
            CommandManager.Instance.AddPushCommand( newPoseCommand );
        }
        else
        {
            CommandManager.Instance.AddCommand( newPoseCommand );
        }
        break;
		
		case "location":
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
                Debug.Log( "[String Parser]<color=red>wrong command</color> !! the format is icon iconName on/off" );
                //Debug.Break();
            }
            locationManager.Instance.SetButton(parsedCommand[1], set);
        }
        else
        {
            Debug.Log( "[String Parser]<color=red>wrong command</color> !! the format is icon iconName on/off" );
            //Debug.Break();
        }
		break;
        
		case "eff":
        EffectCommand newEffect = new EffectCommand();
        newEffect.SetEffect( parsedCommand[1] );
        if( pushCommand )
        {
            CommandManager.Instance.AddPushCommand( newEffect );
        }
        else
        {
            CommandManager.Instance.AddCommand( newEffect );
        }
        break;

        case "item":
        ItemManager.Instance.AddItem( parsedCommand[1].ToLower() );
        break;

        case "icon":
        //Note(Hendry) : format is -> icon itemName position scale
        // position[] = middle/mid/m left/l right/r
        // scale float value
        // to destroy -> icon destroy
        IconCommand iconCommand;
        if( parsedCommand[1].ToLower() == "destroy" )
        {
            iconCommand = new IconCommand( true );
        }
        else
        {
            iconCommand = new IconCommand( parsedCommand[1], parsedCommand[2], float.Parse( parsedCommand[3] ) );
        }
        CommandManager.Instance.AddCommand( iconCommand );
        break;

        case "prompt":
        // note : prompt will call the menu then open evidence tab
        // format -> prompt itemName
        CommandManager.Instance.correctItem = parsedCommand[1];
        OpenMenuCommand menuCommand = new OpenMenuCommand();
        CommandManager.Instance.AddCommand( menuCommand );
        break;

        case "advquest":
        SceneManager.Instance.AdvQuest();
        break;


        case "load":
        LoadCommand dialogue = new LoadCommand();
        if( parsedCommand.Length == 2 )
        {
            dialogue.SetLoad( parsedCommand[1].ToLower() );
            CommandManager.Instance.AddCommand( dialogue );
        }
        break;
            case"fade":
                {
                    if (parsedCommand.Length > 2)
                    {
                        FadeCommand foo = new FadeCommand();

                        if (parsedCommand[1].ToLower() == "in")
                            foo.SetFade(-1, float.Parse(parsedCommand[2]));
                        else
                            foo.SetFade(1, float.Parse(parsedCommand[2]));

                        CommandManager.Instance.AddCommand(foo);
                    }
                    else if(parsedCommand.Length > 1)
                    {
                        FadeCommand foo = new FadeCommand();

                        if (parsedCommand[1].ToLower() == "in")
                            foo.SetFade(-1);
                        else
                            foo.SetFade(1);

                        CommandManager.Instance.AddCommand(foo);
                    }
                    else
                    {
                        Debug.Log("STRING PARSER - FADE COMMAND NOT LONG ENOUGH");
                    }
                }
                break;
        }
    }
    
    public void ReadLocationData(ref Dictionary<int, List<string>> value, string loc )
    {
        char[] delimiter = { '\r', '\n' };
        string[] data = (Resources.Load( loc ) as TextAsset).ToString().Split( delimiter, System.StringSplitOptions.RemoveEmptyEntries );
        //Dictionary<int, List<string>> value = new Dictionary<int, List<string>>();
        string compare = "";
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
                value.Add(currentID, new List<string>());
                //value[currentID] = new List<string>();
            }
            else
            {
                value[currentID].Add( data[i].ToLower() );
            }
        }
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

