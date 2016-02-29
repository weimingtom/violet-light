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
    public string scriptFolder = "Dialogue/";
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
    //update upload wrong scene here
    public void LoadScene( string _scene)
	{
        Debug.Log("[File Reader] Loading in scene " + _scene);
        CommandManager.Instance.Reinitialize();
		TextAsset command = Resources.Load(scriptFolder + _scene) as TextAsset;
        SceneManager.Instance.SetInputBlocker( true );
        StringParser.Instance.RunParse(command.ToString());
        SceneManager.Instance.SetScenePlayed(_scene);
	}
    // NOTE(jesse): Check if the scene exists
    public bool IsScene(string _scene)
    {
        Debug.Log("[File Reader] Checking if scene exists: " + _scene);
        if (Resources.Load(scriptFolder + _scene)!=null)
        { return true; }
        return false;
    }
    public string ReadCommandText(string commandAddress)
    {
        return (Resources.Load(commandAddress) as TextAsset).ToString();
    }
    public void ReadCharacter(string fileName)
    {
        Debug.Log("[File Reader] Reading in characters at | " + scriptFolder + fileName);
        TextAsset commandContainer = Resources.Load(scriptFolder + fileName) as TextAsset;
        StringParser.Instance.ParseCharacters(commandContainer.ToString());
    }
    public void ReadBackgrounds(string fileName)
    {
        Debug.Log("[File Reader] Reading in backgrounds at | " + scriptFolder + fileName);
        TextAsset commandContainer = Resources.Load(scriptFolder + fileName) as TextAsset;
        StringParser.Instance.ParseBackgrounds(commandContainer.ToString());
    }

    public void ReadScenes( string fileName )
    {
        Debug.Log("[File Reader] Reading in Dialogue Scenes at | " + scriptFolder + fileName);
        TextAsset commandContainer = Resources.Load( scriptFolder + fileName ) as TextAsset;
        StringParser.Instance.ParseScene( commandContainer.ToString() );
    }
}
