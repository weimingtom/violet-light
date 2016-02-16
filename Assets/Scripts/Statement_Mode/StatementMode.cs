using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StatementMode
{
    //use set display command

    List<string> mainStatements;
    Dictionary<int, List<string>> pushStatements;
    List<string> endText;


    int mainIndex;
    int pushIndex;
    int endTextIndex;

    public bool push { get; set; }
    public bool end { get; set; }
	// Use this for initialization
	public void OnInitialize ()
    {
        mainStatements = new List<string>();
        pushStatements = new Dictionary<int, List<string>>();
        endText = new List<string>();
        mainIndex = 0;
        pushIndex = 0;
        endTextIndex = 0;
	}

    public void AddMainStatement(string str)
    {
        mainStatements.Add( str );
    }
    public void AddPushStatement(string str)
    {
        if( mainStatements.Count == 0 )
        {
            Debug.Log( "[Stement Holder] : failed no main statement!" );
            Debug.Break();
        }
        else
        {
            pushStatements[mainStatements.Count - 1].Add( str );
        }
    }

    public void AddEndText( string str )
    {
        endText.Add( str );
    }

    public string GetText()
    {
        if( push )
        {
            if( pushStatements.Count == 0 )
            {
                Debug.Log( "[Stement Holder] : no push statement added!" );
                Debug.Break();
            }
            return pushStatements[mainIndex][pushIndex];
        }
        else if( end )
        {
            if( endText.Count == 0 )
            {
                Debug.Log("[Stement Holder] : no end text!");
                Debug.Break();
            }
            return endText[endTextIndex];
        }
        else
        {
            return mainStatements[mainIndex];
        }
    }
    public void AdvancePush()
    {
        if( pushIndex < pushStatements.Count )
        {
            pushIndex++;
        }
    }

    public void AdvanceEnd()
    {
        if( endTextIndex < endText.Count )
        {
            endTextIndex++;
        }
    }

    public void AdvanceMain()
    {
        if( mainIndex < mainStatements.Count )
        {
            mainIndex++;
        }
    }

    public void RewindMain()
    {
        if( mainIndex > 0 )
        {
            mainIndex--;
        }
    }

}
