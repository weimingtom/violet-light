using UnityEngine;
using System;
using System.Collections;

public class FadeCommand : Commands 
{
    int fadeDir;
    float fadeSpd;
    public FadeCommand()
    {
        commandTag = "fadecommand";
    }
    public override bool ExecuteCommand()
    {
        if(fadeSpd!=0.0f)
        {
            FadeOutScreen.instance.BeginFade(fadeDir, fadeSpd);
            return true;
        }
        FadeOutScreen.instance.BeginFade(fadeDir);
        return true;
    }
    public override void Reset()
    {
        //loop = true;
    }
    public override void PrintData()
    {
    }
    public void SetFade( int dir, float speed = 0.0f)
    {
        fadeDir = dir;
        fadeSpd = speed;
    }
	public override bool Destroy()
	{
        return true;
	}
}
