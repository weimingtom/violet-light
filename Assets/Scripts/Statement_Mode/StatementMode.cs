using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StatementMode
{
    //use set display command
    List<string> nameTag = new List<string>();
    List<string> endNameTag = new List<string>();
    List<string> mainStatements = new List<string>();
    Dictionary<int, List<string>> pushStatements = new Dictionary<int, List<string>>();
    Dictionary<int, List<string>> pushStatementsNameTag = new Dictionary<int, List<string>>();
    List<string> endText;

    int mainIndex = 0;
    int pushIndex = 0;
    int endTextIndex = 0;

    public bool push { get; set; }
    public bool end { get; set; }

    public void AddMainStatement( string nmTag ,string str)
    {
        nameTag.Add( nmTag );
        mainStatements.Add( str );
    }

    public void AddPushStatement( string nmTag, string str)
    {
        //Rule ! push need to folowed by main
        if( mainStatements.Count == 0 )
        {
            Debug.Log( "[Stement Holder] : failed no main statement!" );
            Debug.Break();
        }
        else
        {
            try
            {
                pushStatements[mainStatements.Count - 1].Add( nmTag );
            }
            catch( System.Collections.Generic.KeyNotFoundException )
            {
                pushStatements[mainStatements.Count - 1] = new List<string>();
                pushStatements[mainStatements.Count - 1].Add( nmTag );
            }
        }
    }

    public void AddEndText(string nm, string str )
    {
        endNameTag.Add( nm );
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

    public void AdvanceText()
    {
        if( push )
        {
            AdvancePush();
        }
        else if( end )
        {
            AdvanceEnd();
        }
        else
        {
            mainIndex++;
            if( mainIndex == mainStatements.Count )
            {
                //if reach max play end statement
                end = true;
                mainIndex = 0;
            }
        }
    }

    public void RewindText()
    {
        if( !push && !end )
        {
            if( mainIndex > 0 )
            {
                mainIndex--;
            }
        }
    }
}
