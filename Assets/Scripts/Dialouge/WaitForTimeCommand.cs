
using UnityEngine;
using System.Collections;

public class WaitForTimeCommand : Commands
{
    float waitingTime = 0;
    float totalTime = 0;
    bool Initialized = false;

    void Initialize( float t )
    {
        Initialized = true;
    }
    public override void PrintData()
    {
        Debug.Log( "WaitForTimeCommand\nTime : " + waitingTime );
    }
    void SetTime( float t )
    {
        waitingTime = t;
    }
    public float GetTime()
    {
        return waitingTime;
    }
    public override bool ExecuteCommand()
    {
        if( Initialized == false )
        {
            return true;
        }
        else
        {
            if( Input.anyKey )
            {
                return true;
            }
            totalTime += Time.deltaTime;
            return totalTime >= waitingTime;
        }
    }
    public override void Reset()
    {
        totalTime = 0;
        waitingTime = 0;
    }
	public override bool Destroy()
	{
        Reset();
        return true;
	}
   
}
