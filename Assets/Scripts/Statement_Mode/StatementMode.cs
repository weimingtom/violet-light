using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StatementMode
{
    //use set display command
    List<string> mainStatements = new List<string>();
    Dictionary<int, List<string>> pushStatements = new Dictionary<int, List<string>>();
    List<string> endText;

    int mainIndex = 0;
    int pushIndex = 0;
    int endTextIndex = 0;

    public bool push { get; set; }
    public bool end { get; set; }

    public void AddMainStatement(string str)
    {
        mainStatements.Add( str );
    }

    public void AddPushStatement(string str)
    {
        //Rule ! push need to folowed by main
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
                Debug.Log( "[Stement Holder] : no push statement registered!" );
                Debug.Break();
            }
            return pushStatements[mainIndex][pushIndex];
        }
        else if( end )
        {
            if( endText.Count == 0 )
            {
                Debug.Log( "[Stement Holder] : no end text registered!" );
                Debug.Break();
            }
            return endText[endTextIndex];
        }
        else
        {
            if(mainStatements.Count == 0)
            {
                Debug.Log( "[Stement Holder] : no main text registered!" );
                Debug.Break();
            }
            return mainStatements[mainIndex];
        }
    }

    public void AdvancePush()
    {
        pushIndex++;
        if( pushIndex == pushStatements.Count )
        {
            push = false;
            pushIndex = 0;
        }
    }

    public void AdvanceEnd()
    {
        endTextIndex++;
        if( endTextIndex == endText.Count )
        {
            end = false;
            endTextIndex = 0;
        }
    }

    public void AdvanceMain()
    {
        mainIndex++;
        if( mainIndex == mainStatements.Count )
        {
            //if reach max play end statement
            end = true;
            mainIndex = 0;
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
