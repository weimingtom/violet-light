using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
        string charLoca = "Dialogue/character_names_scene_1";// +SceneManager.Instance.GetScene().ToString();
        StringParser.Instance.RunRegisterCharacterCode( charLoca, ref characterNameDictionary );
    }
    public string GetCharacterNameFromToken(string _token)
    {
        try
        {
            return characterNameDictionary[_token.ToLower()];
        }
        catch( KeyNotFoundException )
        {
            string[] passedString = _token.Split('_');
            try
            {
                return characterNameDictionary[passedString[0].ToLower()];
            }
            catch( KeyNotFoundException )
            {
                Debug.Log("Nothing found, is the id special character ?");
                return _token;
            }
        }
    }
    public void AddDialogue(ref string header, string content )
    {
        // NOTE(jesse) This should get rid of the extra space at the beginning of all dialogue

        string newContent = content.ToString();
        content.TrimStart( ' ' );
        content.TrimStart( '\n' );
        if( !dialogue.ContainsKey( header.ToLower() ) )
        {
            dialogue.Add( header.ToLower(), newContent );
        }
        else
        {
            header += "_" + index.ToString();
            index++;
            dialogue.Add( header.ToLower(), newContent );
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
