using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CommandScripts : MonoBehaviour 
{
    enum eCommand
    { 
        showCharacter,
        From,
        Wait,
        ShowText,
        location,
        EndConversation
    }
    struct Wait
    {
        float waitForTime;
        bool waitForClick;
    }
    void showCharacter()
    { 
    }
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
