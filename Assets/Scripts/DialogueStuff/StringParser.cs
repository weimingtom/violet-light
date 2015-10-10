using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public class StringParser : MonoBehaviour 
{
    static StringParser Instance;
    Dictionary<string, string> conversation;

    void Awake()
    {
        Instance = this;
    }
    public void RegisterDialogue(string mainString)
    {
        string conversationHeader = "a";
        string conversationContent = "b";
        conversation.Add(conversationHeader.ToString(), conversationContent.ToString());
    }
    void ParseDialogue(string mainString,ref string header, ref string content)
    {
        int locationCheck = 0;

        for( int index = 0; index < mainString.Length; index++ )
        {
            if( mainString[index] == '"' )
            {
                locationCheck++;
            }
            else
            { 
                if( locationCheck == 1 )
                {
                    header += mainString[index];
                }
                else if( locationCheck == 3 )
                {
                    content += mainString[index];
                }
            
            }

        }
    }
}
