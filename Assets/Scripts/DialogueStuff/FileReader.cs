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
    public static FileReader Instance;
    public string fileLocation;
    //===================================================
    //  Load file function
    //  can be used to read both text sequence and command
    //===================================================
    private void RegisterFile(eTextType type, string line)
    {
        print( "Raw file : " + line );
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
            StringParser.Instance.CommandParser( line );
        }
        else if( type == eTextType.Background )
        {
            StringParser.Instance.BackgroundReader( line, ref BackgroundManager.Instance.backgroundLookup );
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
            eTextType type = eTextType.none;
            using( reader )
            {
                do
                {
                    line = reader.ReadLine();
                    if( line != null ) // line is not empty
                    {
                        print("detected");
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
            print( String.Format( "{0}\n", e.Message ) );
            Console.WriteLine( "{0}\n", e.Message );
        }
    }
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //LoadFile();
        TextAsset myText = Resources.Load( "a" ) as TextAsset;
        print("derp contain : " + myText.ToString());
        Debug.Break();
    }
}
