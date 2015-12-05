using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Agent : MonoBehaviour 
{
	//for environment
	//for our player
	private bool gameStart = false;
	public float speed;
	public GameObject EndDestinations;
	private Vector2 temporaryDestination;
	private bool hitIntersections;
    private Vector2 startPosition;
    bool reachTop;
    bool winStatus;
    public float SizeofTravelWidth = 1.75f;
    public float ZValue = -5f;

    public void StartRun()
    {
        reachTop = false;
        gameStart = true;
        winStatus = false;
    }
    public void Reset()
    {
        reachTop = false;
        gameStart = false;
        winStatus = false;
        this.transform.position = startPosition;
    }
	void Start () 
	{
		hitIntersections = false;
        this.transform.position = new Vector3( this.transform.position.x, this.transform.position.y, ZValue );
        startPosition = this.transform.position;
	}
    public void RunGame()
    {
        //GameStart = true;
        if( gameStart )
        {
            GotoDestination();
        }
        if( Mathf.Abs( this.transform.position.y - EndDestinations.transform.position.y ) < 0.01 && gameStart == true )
        {
            gameStart = false;
            reachTop = true;
            if( WinConditionReached() )
            {
                Debug.Log( "Win" );
                winStatus = true;
            }
        }
    }
    public bool ReachTop()
    {
        return reachTop;
    }
    public bool GetWinStatus()
    {
        return winStatus;
    }
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "row" && hitIntersections == false)
		{
			hitIntersections = true;
			Vector2 newPos = col.gameObject.transform.position;
			UpdateTemporaryDestinations(newPos);
		}
	}
	void GotoDestination()
	{
		float step = speed * Time.deltaTime;
		Vector3 updatedPos = new Vector3(0.0f,0.0f, ZValue);
		Vector3 usedDestination;
		
		if(hitIntersections == true)
		{	
			if(Mathf.Abs(this.transform.position.y - temporaryDestination.y) > 0.01f)	//Navigate Up
			{
				usedDestination = temporaryDestination;
				usedDestination.x = this.transform.position.x;
				updatedPos = Vector2.MoveTowards(this.transform.position, usedDestination, step);
				updatedPos.x = this.transform.position.x;
				this.transform.position = updatedPos;
                this.transform.position = new Vector3( this.transform.position.x, this.transform.position.y, ZValue );
			}
			else if(Mathf.Abs(this.transform.position.y - temporaryDestination.y) < 0.01f
			        && Mathf.Abs(this.transform.position.x - temporaryDestination.x) > 0.01f) //Navigate Horisontally
			{
				usedDestination = temporaryDestination;
				usedDestination.y = this.transform.position.y;
				updatedPos = Vector2.MoveTowards(this.transform.position, usedDestination, step);
				updatedPos.y = this.transform.position.y;
				this.transform.position = updatedPos;
                this.transform.position = new Vector3( this.transform.position.x, this.transform.position.y, ZValue );
			}
			else if(Mathf.Abs(this.transform.position.y - temporaryDestination.y) < 0.01f
			        && Mathf.Abs(this.transform.position.x - temporaryDestination.x) < 0.01f)
			{
				hitIntersections = false;
			}
		}
		else
		{
			usedDestination = EndDestinations.transform.position;
			usedDestination.x = this.transform.position.x;
			updatedPos = Vector2.MoveTowards(this.transform.position, usedDestination, step);
			updatedPos.x = this.transform.position.x;
			this.transform.position = updatedPos;
            this.transform.position = new Vector3( this.transform.position.x, this.transform.position.y, ZValue );
		}
	}
	bool WinConditionReached()
	{
		if (Mathf.Abs (this.transform.position.x - EndDestinations.transform.position.x) < 0.01f)
		{
			return true;
		}
		return false;
	}
	public void UpdateTemporaryDestinations(Vector2 destination)
	{
		temporaryDestination = destination;
        if( this.transform.position.x > destination.x ) // this mean that the bar is on the left of agent
        {
            temporaryDestination.x -= SizeofTravelWidth;
        }
        else if( this.transform.position.x < destination.x ) // bar is on the right of agent
        {
            temporaryDestination.x += SizeofTravelWidth;
        }
	}

}
