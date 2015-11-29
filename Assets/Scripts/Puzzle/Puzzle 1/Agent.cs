using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Agent : MonoBehaviour 
{
	//for environment
	//for our player
	bool GameStart;
	public float speed;
	public GameObject EndDestinations;
	private Vector2 temporaryDestination;
	private bool hitIntersections;

	void Awake () 
	{
		GameStart = false;
		hitIntersections = false;
	}
	void Update () 
	{
		if (GameStart == false)
		{
			if (Input.GetKeyUp ("space"))
			{
				GameStart = true;
			}
		}
		if (GameStart == true)
		{
			GotoDestination();
		}
		if (Mathf.Abs(this.transform.position.y - EndDestinations.transform.position.y) < 0.01 && GameStart == true) 
		{
			GameStart = false;
			if(!WinConditionReached())
			{
				Debug.Log("Dead");
			}
			else
			{
				Debug.Log("Win");
			}
		}
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
		Vector2 updatedPos = new Vector2(0.0f,0.0f);
		Vector2 usedDestination;
		
		if(hitIntersections == true)
		{	
			if(Mathf.Abs(this.transform.position.y - temporaryDestination.y) > 0.01f)	//Navigate Up
			{
                //step = speed * Time.deltaTime * 8.0f;
				usedDestination = temporaryDestination;
				usedDestination.x = this.transform.position.x;
				updatedPos = Vector2.MoveTowards(this.transform.position, usedDestination, step);
				updatedPos.x = this.transform.position.x;
				this.transform.position = updatedPos;
			}
			else if(Mathf.Abs(this.transform.position.y - temporaryDestination.y) < 0.01f
			        && Mathf.Abs(this.transform.position.x - temporaryDestination.x) > 0.01f) //Navigate Horisontally
			{
                //step = speed * Time.deltaTime * 2.0f;
				usedDestination = temporaryDestination;
				usedDestination.y = this.transform.position.y;
				updatedPos = Vector2.MoveTowards(this.transform.position, usedDestination, step);
				updatedPos.y = this.transform.position.y;
				this.transform.position = updatedPos;
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
        //check position of the row
        if( this.transform.position.x > destination.x ) // this mean that the bar is on the left of agent
        {
            temporaryDestination.x -= 2;
        }
        else if( this.transform.position.x < destination.x ) // bar is on the right of agent
        {
            temporaryDestination.x += 2;
        }
	}

}
