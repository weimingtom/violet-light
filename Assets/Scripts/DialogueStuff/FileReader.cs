using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;
public class FileReader : MonoBehaviour 
{
    enum eTextType
    {
        none = -1,
        TextData = 0,
        CommandSequence = 1,
        Background = 2,
        size = 3
    }
    static public FileReader Instance;
    public string fileLocation;
    //=================================================================
    //  Load file function
    //  can be used to read both text sequence and command
    //=================================================================
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        ReadDialogue();
        ReadCommand();
    }
    private void RegisterFile(eTextType type, string line)
    {
        //print( "Raw file : " + line );
        if( line == "TextData" )
        {
            type = eTextType.TextData;
        }
        else if( line == "CommandSequence" )
        {
            type = eTextType.CommandSequence;
        }
        else if( line == "background" )
        {
            type = eTextType.Background;
        }
        else if( type == eTextType.TextData )
        {
            StringParser.Instance.ParseDialogue( line );
        }
        else if( type == eTextType.CommandSequence )
        {
            //StringParser.Instance.CommandParser( line );
        }
        else if( type == eTextType.Background )
        {
            StringParser.Instance.BackgroundReader( line, ref SceneManager.Instance.backgroundLookup );
        }
    }
    public void LoadFile()
    {
        try
        {
            string line;
            // create a new stream reader, tell it which file to read and what encoding the file was saved as
            StreamReader reader = new StreamReader( fileLocation, Encoding.Default );
            //This bool is used to switch between parsing to script or
            //eTextType type = eTextType.none;
            using( reader )
            {
                do
                {
                    line = reader.ReadLine();
                    if( line != null ) // line is not empty
                    {
                        //print("detected");
                        //RegisterFile(type, line);
                    }
                }
                while( line != null );
            }
        }
        catch( Exception e )
        {
            //location reading - ask teacher
            //Debug.Log( String.Format( "{0}\n", e.Message ) );
            //print( String.Format( "{0}\n", e.Message ) );
            Console.WriteLine( "{0}\n", e.Message );
        }
    }
    //================================================//
    //  Use this Read dialogue to get file from text  //
    //================================================//
	public void LoadScene(string _scene, string _dialogue)
	{
        CommandManager.Instance.Reinitialize();
		TextAsset command = Resources.Load(_scene) as TextAsset;
		TextAsset dialogue = Resources.Load(_dialogue) as TextAsset;
        SceneManager.Instance.SetInputBlocker( true );
		StringParser.Instance.ParseCommand(command.ToString());
		StringParser.Instance.ParseDialogue(dialogue.ToString());
	}
    private void ReadDialogue()
    {
        TextAsset dialogueContainer = Resources.Load( "conversation" ) as TextAsset;
        //print( "a contain : " + dialogueContainer.ToString() + "[Length :" + dialogueContainer.ToString().Length + "]" );
        StringParser.Instance.ParseDialogue( dialogueContainer.ToString() );
    }
    private void ReadCommand()
    {
        TextAsset commandContainer = Resources.Load("command") as TextAsset;
        //print( "Command raw string :" + commandContainer.ToString() );
        StringParser.Instance.ParseCommand( commandContainer.ToString());
    }
    public string ReadCommandText(string commandAddress)
    {
        return (Resources.Load(commandAddress) as TextAsset).ToString();
    }
    public void ReadCharacter(string fileName)
    {
        TextAsset commandContainer = Resources.Load( fileName ) as TextAsset;
        StringParser.Instance.ParseCharacters(commandContainer.ToString());
    }
    public void ReadBackgrounds(string fileName)
    {
        Debug.Log("Reading in backgrounds...");
        TextAsset commandContainer = Resources.Load(fileName) as TextAsset;
        StringParser.Instance.ParseBackgrounds(commandContainer.ToString());
    }
}
/*NOTE
//Dictionary<uniqueID, String> dialogStringEnglisth = new Dictionary<String, String>();
//dialogString.Add( "Conversation1_1", "Snowy\n\nYow!...\now! ...ow!" );
//dialogString.Add( "Conversation1_2", "Tintin\n\n?" );
//dialogString.Add( "Conversation1_3", "Tintin\n\nThere you are snowy. You see what comes of your dirty habit of exploring rubbish bins…\n I don’t go scavenging do i ?" );
//dialogString.Add( "Conversation1_4", "<end>" );

//Dictionary<uniqueID, String> dialogStringChinese = new Dictionary<String, String>();
//dialogString.Add( "Conversation1_1", "雪域\ñ \ nYow ！ ... \吧！ ...嗚！" );
//dialogString.Add( "Conversation1_2", "丁丁\ñ \ñ ？" );
//dialogString.Add( "Conversation1_3", "Tintin\n\nThere you are snowy. You see what comes of your dirty habit of exploring rubbish bins…\n I don’t go scavenging do i ?" );


//Dictionary<uniqueID, String> dialogString = dialogStringEnglisth;
//if (Options.Settings.Language == "Chinese")
//{
//    dialogString = dialogStringChinese;
//}

//class DialogPlaybackSystem
//{
//    public Play(int convoId)
//    {
//        String key = "Conversation" + convoId;

//        int index = 1;
//        String nextStr = dialogString[key + index]
//        while (nextStr != "<end>")
//        {
//            showText(nextStr);

//            while for click
//                index++
//                nextStr = dialogString[key + index];
//        }
//    }
//} 
*/