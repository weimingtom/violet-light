using System.Collections;

public class OpenMenuCommand : Commands
{
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
        MenuManager.instance.OpenEvidenceTab();
        return true;
    }
    public override bool Destroy()
    {

        return true;
    }
    public override void Reset()
    {
    
    }
}
