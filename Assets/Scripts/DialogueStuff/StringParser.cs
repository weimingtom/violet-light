﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public class StringParser : MonoBehaviour 
{
    static public StringParser Instance;
    Dictionary<string, string> conversation;
    void Awake()
    {
        Instance = this;
        conversation = new Dictionary<string,string>();
    }
    public void RegisterDialogue(string header, string content)
    {
        string conversationHeader = header;
        string conversationContent = content;
        conversation.Add(conversationHeader.ToString(), conversationContent.ToString());
        print("Header :" + conversationHeader);
        print("Content :" + conversationContent);
        Debug.Break();
    }
    public void ParseDialogue(string mainString)
    {
        int locationCheck = 0;
        string header = "";
        string content = "";
        for( int index = 0; index < mainString.Length; index++ )
        {
            if( mainString[index] == '"')
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

        if( locationCheck != 4 )
        {
            print( "wrong format" );
            Debug.Break();
        }
        else
        { 
            RegisterDialogue(header, content);
        }
    }
    public void CommandParser(string mainstring)
    {
        for( int i = 0; i < mainstring.Length; i++ )
        {

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
            Debug.Break();
        }
        else
        {
            _background.Add( header, content );
        }
    }
}