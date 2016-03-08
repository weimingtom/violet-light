using UnityEngine;
using System.Collections;

public class IconCommand : Commands
{
    private string position = "";
    private string itemName = "";
    private float scale = 0;
    bool destroy = false;
    public IconCommand(string pos, string item, float sc)
    {
        commandTag = "iconcommand";
        position = pos;
        itemName = item;
        scale = sc;
    }
    public IconCommand( bool dest )
    {
        destroy = dest;
    }
    public override void PrintData()
    {
        
    }
    public override bool ExecuteCommand()
    {
        if( destroy == false )
        {
            IconManager.instance.ShowIcon( itemName, position, scale );
        }
        else
        {
            IconManager.instance.DestroyIcon();
        }
        return true;
    }
    public override bool Destroy()
    {
        return true;
    }
    public override void Reset()
    {
        position = "";
        itemName = "";
        scale = 1;
    }
}
