using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;
public class FileReader : MonoBehaviour 
{
    static public string fileLocation;
    private bool LoadFile(string _fileName)
    {
        try
        {
            string line;
            // create a new stream reader, tell it which file to read and what encoding the file was saved as
            StreamReader reader = new StreamReader( _fileName, Encoding.Default );
            using( reader )
            {
                do
                {
                    line = reader.ReadLine();
                    if( line != null ) // line is not empty
                    {
                        //Store Character
                        print( line );
                    }
                }
                while( line != null );
            }
        }
        catch( Exception e )
        {
            Debug.Log( String.Format( "{0}\n", e.Message ) );
            Console.WriteLine( "{0}\n", e.Message );
            return false;
        }
        return true;
    }
	
}
