﻿using UnityEngine;
using System.Collections;

public class BgmCommand : Commands
{
    string bgm;

    public BgmCommand()
    {
        Debug.Log("<color=green>[BMG GCOMAND]</color> CREATED NEW BGM COMMAND");

        commandTag = "BGMcommand";
    }
    public override bool ExecuteCommand()
    {
        Debug.Log("<color=green>[BGM GCOMAND]</color> CHANGING SONG TO:" + bgm);
        MusicManager.instance.ChangeSong(bgm);
        return true;
        //return !loop;
    }
    public void Set(string str)
    {
        Debug.Log("<color=green>[BMG GCOMAND]</color> SETTING NEW COMMAND SONG TO:" + str);
        bgm = str;
    }
       
    public override void Reset()
    {
        //loop = true;
    }
	public override void PrintData()
	{
	}

	public override bool Destroy()
	{
        return true;
	}
}