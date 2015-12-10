
using UnityEngine;
using System.Collections;

public class WaitForTimeCommand : Commands
{
    float waitingTime = 0;
    float totalTime = 0;
    public override void PrintData()
    {
        Debug.Log( "WaitForTimeCommand\nTime : " + waitingTime );
    }
    public void SetTime( float t )
    {
        waitingTime = t;
    }
    public float GetTime()
    {
        return waitingTime;
    }
    public override bool ExecuteCommand()
    {
        if( Input.anyKey )
        {
            return true;
        }
        totalTime += Time.deltaTime;
        return totalTime >= waitingTime;
    }
	public override bool Destroy()
	{
        return true;
	}
   
}
