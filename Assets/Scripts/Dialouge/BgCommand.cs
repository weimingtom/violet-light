using UnityEngine;
using System.Collections;

public class BgCommand : Commands
{
    string bg;
    float spd = 0.5f;
    //bool loop = false;
    public BgCommand()
    {
        commandTag = "loadcommand";
    }

    public void SetBg(string str)
    {
        bg = str;
    }
    public void SetSpd(float str)
    {
        spd = str;
    }
       
    public override void Reset()
    {
        //loop = true;
    }
	public override void PrintData()
	{
	}
	public override bool ExecuteCommand()
	{
        if (!GameManager.instance.IsDemoMode())
            SceneManager.Instance.ChangeBg(bg, spd);
        else
            DemoManager.Instance.ChangeBg(bg, spd);

        return true;
		//return !loop;
	}
	public override bool Destroy()
	{
        return true;
	}
}
