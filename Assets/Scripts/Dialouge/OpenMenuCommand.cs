using System.Collections;

public class OpenMenuCommand : Commands
{
    bool execute = false;
    //potential to open other menu
    public OpenMenuCommand()
    {
        commandTag = "openmenucommand";
    }
    public override void PrintData()
    {
    
    }
    public override bool ExecuteCommand()
    {
        if( !execute )
        {
            MenuManager.instance.OpenEvidenceTab();
            //MenuManager.instance.calledByOpenMenuCommand = true;

            execute = true;
        }

        if(CommandManager.Instance.donePrompt)
        {
            MenuManager.instance.ForceCloseMenu();
            return true;
        }

        return false;
    }
    public override bool Destroy()
    {

        return true;
    }
    public override void Reset()
    {
        execute = false;
    }
}
