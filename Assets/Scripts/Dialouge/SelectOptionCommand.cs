using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class SelectOptionCommand : Commands
{

	private List<string> mOption;
	private List<Button> mButton;
	void Start()
	{

	}
	public override void PrintData()
	{

	}
    public override void Reset()
    {
    }
	public override bool ExecuteCommand()
	{
		return true;
	}
	public override bool Destroy()
	{
		return true;
	}
}
