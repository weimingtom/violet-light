using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class DialogueHolder : MonoBehaviour 
{
    static public DialogueHolder Instance;
    Dictionary<string, string> dialogue;

	// Use this for initialization
	void Awake () 
    {
        Instance = this;
        dialogue = new Dictionary<string, string>();
	}
    public void AddDialogue( string header, string content )
    {
        dialogue.Add(header.ToString(), content.ToString());
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
