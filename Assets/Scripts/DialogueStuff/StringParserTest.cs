using UnityEngine;
using System.Collections;

public class StringParserTest : MonoBehaviour 
{
    public static StringParserTest Instance;
    void Awake()
    {
        Instance = this;
    }
    public void ParseCommand()
    {
        string[] command = FileReader.Instance.ReadCommandText("commandTest").ToString().Split(':');
        for( int i = 0; i < command.Length; i++ )
        {
            Debug.Log("index[" + i + "] - " + command[i]);
        }
    }
}
