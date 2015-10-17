using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;
public class FileReader : MonoBehaviour 
{
    public static FileReader Instance;
    public string fileLocation;
    private int readIndex;
    public void LoadFile()
    {
        try
        {
            string line;
            // create a new stream reader, tell it which file to read and what encoding the file was saved as
            StreamReader reader = new StreamReader( fileLocation, Encoding.Default );
            using( reader )
            {
                do
                {
                    line = reader.ReadLine();
                    if( line != null ) // line is not empty
                    {
                        //Store Character
                        print( "Raw file : " + line );
                        StringParser.Instance.ParseDialogue( line );
                    }
                }
                while( line != null );
            }
        }
        catch( Exception e )
        {
            //Debug.Log( String.Format( "{0}\n", e.Message ) );
            print( String.Format( "{0}\n", e.Message ) );
            Console.WriteLine( "{0}\n", e.Message );
        }
    }
    void Awake()
    {
        readIndex = 0;
        Instance = this;
    }
    void Start()
    {
    }
}
