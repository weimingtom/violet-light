using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CommandScripts : MonoBehaviour
{
    private int conversationID;
    private List<string> locations;
    private int currentlocationIndex;
    void Start()
    {
        conversationID = 0;
        currentlocationIndex = 0;
    }
    public void SetConversationID(int id)
    {
        conversationID = id;
    }
    public void SetLocation(string _location)
    { 
        locations.Add(_location);
    }
    public string GetLocation()
    {
        //warning :: this only able to go forward
        return locations[currentlocationIndex++];
    }

    //enum eCommand
    //{ 
    //    showCharacter,
    //    From,
    //    Wait,
    //    ShowText,
    //    location,
    //    EndConversation
    //}
    public struct WaitCommand
    {
        float waitForTime;
        bool waitForClick;
    }
    public struct ShowCharacterCommand
    {
        string character;
        string direction;
    }
    public struct ShowTextCommand
    {
        bool onOffSwitch;
    }
}
