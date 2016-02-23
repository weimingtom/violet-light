using System;
using System.Collections;

public class TestimonyCommand : Commands
{
    StatementMode myStatementMode = new StatementMode();

    public TestimonyCommand()
    {
        commandTag = "testimonycommand";
    }

    public void AddMainStatement(string nm, string str)
    {
        //parse text
        myStatementMode.AddMainStatement( nm, str );
    }

    public void AddPushStatement( string nm, string str )
    {
        //parse text
        myStatementMode.AddPushStatement( nm, str);
    }

    public void AddEndStatement(string nm, string str)
    {
        //parse text
        myStatementMode.AddEndText( nm, str);
    }

    public void ActivatePushStatement()
    {
        myStatementMode.push = true;
    }

    public void Next()
    {
        myStatementMode.AdvanceText();
    }

    public void Back()
    {
        myStatementMode.RewindText();
    }

    public override void PrintData()
    {
        
    }

    public override void Reset()
    {
        
    }

    public override bool ExecuteCommand()
    {
        CommandManager.Instance.SetTextHolder( myStatementMode.GetText() );
        return false;
    }

    public override bool Destroy()
    {
        return true;
    }

}
