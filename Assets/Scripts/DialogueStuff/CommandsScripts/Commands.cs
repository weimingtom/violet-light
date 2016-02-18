using System;
using System.Collections;

public abstract class Commands
{
    public string commandTag { get; set; }
    protected Commands()
    {
        commandTag = "commands";
    }
    public abstract void PrintData();
    public abstract bool ExecuteCommand();
	public abstract bool Destroy();
    public abstract void Reset();
}
