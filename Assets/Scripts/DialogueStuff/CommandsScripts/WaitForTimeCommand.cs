
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
        Debug.Log( "[Wait For Time]Time Before wait : " + Time.time );
        totalTime += Time.deltaTime;
        //StartCoroutine(Wait());
        Debug.Log( "[Wait For Time]Time After Wait Function is done :" + Time.time );
        return totalTime >= waitingTime;
    }
	public override void Destroy()
	{
		
	}
   
}
