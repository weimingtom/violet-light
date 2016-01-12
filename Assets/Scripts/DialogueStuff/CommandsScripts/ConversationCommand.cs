using UnityEngine;
using System.Collections;

public class ConversationCommand : MonoBehaviour
{
    int indexStart = 0;
    int indexEnd = 0;
    string[] command = new string[2];
    
    public void SetConversationCommand( int _indexStart, int _indexEnd, string[] _command )
    {
        indexStart = _indexStart;
        indexEnd = _indexEnd;
        command = _command;
    }
    public bool RunScriptCommand(int indexPassed)
    {
        if( indexPassed == indexStart )
        {
        
        }
        return true;
    }
}
