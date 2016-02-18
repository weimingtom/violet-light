using System;
using System.Collections;

public abstract class Commands
{
    public abstract void PrintData();
    public abstract bool ExecuteCommand();
	public abstract bool Destroy();
    public abstract void Reset();
}
