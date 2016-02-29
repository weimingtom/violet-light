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
    List<string> endText = new List<string>();

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

    public void AddPushStatement( string nmTag, string statement)
    {
        //Rule ! push need to folowed by main
        if( mainStatements.Count == 0 )
        {
            Debug.Log( "[Stement Holder] : failed no main statement!" );
            Debug.Break();
        }
        else
        {
            if( !pushStatementsNameTag.ContainsKey( mainStatements.Count - 1 ) )
            {
                List<string> temporaryList = new List<string>();
                temporaryList.Add(nmTag);
                pushStatementsNameTag.Add( mainStatements.Count - 1, temporaryList );
            }
            else
            {
                pushStatementsNameTag[mainStatements.Count - 1].Add(nmTag);
            }

            if( !pushStatements.ContainsKey( mainStatements.Count - 1 ) )
            {
                List<string> temporaryList = new List<string>();
                temporaryList.Add( statement );
                pushStatements.Add(mainStatements.Count - 1 , temporaryList);
            }
            else
            {
                pushStatements[mainStatements.Count - 1].Add( statement );
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

            CommandManager.Instance.presentItemIndex = mainIndex;
            return mainStatements[mainIndex];
        }
    }

    void AdvancePush()
    {
        pushIndex++;

        if( pushIndex == pushStatements.Count )
        {
            push = false;
            CommandManager.Instance.push = push;
            pushIndex = 0;
        }
    }

    void AdvanceEnd()
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