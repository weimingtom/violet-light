using System;
using System.Collections;

public class TestimonyCommand : Commands
{
    StatementMode myStatementMode = new StatementMode();
    public void AddMainStatement(string str)
    {
        //parse text
        myStatementMode.AddMainStatement( str );
    }
    public void AddPushStatement(string str)
    {
        //parse text
        myStatementMode.AddPushStatement(str);
    }
    public void AddEndStatement(string str)
    {
        //parse text
        myStatementMode.AddEndText(str);
    }

    public override void PrintData()
    { 
    
    }
    public override void Reset()
    {
        
    }
    public override bool ExecuteCommand()
    {

        return false;
    }

    public override bool Destroy()
    {

        return true;
    }

}
