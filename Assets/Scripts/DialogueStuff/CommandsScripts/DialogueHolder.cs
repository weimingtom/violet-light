using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class DialogueHolder : MonoBehaviour 
{
    static public DialogueHolder Instance;
    Dictionary<string, string> dialogue;
    Dictionary<string, string> characterNameDictionary;
    int index = 0;
	// Use this for initialization
	void Awake () 
    {
        Instance = this;
        dialogue = new Dictionary<string, string>();
        characterNameDictionary = new Dictionary<string, string>();
	}
    void Start()
    {
        StringParser.Instance.RunRegisterCharacterCode( "DialougeScripts/CharacterName", ref characterNameDictionary );
    }
    public string GetCharacterNameFromToken(string _token)
    {
        try
        {
            return characterNameDictionary[_token];
        }
        catch( KeyNotFoundException )
        {

            string failed = _token + " no name found";
            Debug.Log(failed);
            //Debug.Break();
            return "no name found";
        }
    }
    public void AddDialogue(ref string header, string content )
    {
        if( !dialogue.ContainsKey( header ) )
        {
            dialogue.Add( header.ToString(), content.ToString() );
        }
        else
        {
            header += index.ToString();
            index++;
            dialogue.Add( header.ToString(), content.ToString() );
        }
    }
    public string GetDialogue( string index )
    {
        return dialogue[index];
    }
    
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
