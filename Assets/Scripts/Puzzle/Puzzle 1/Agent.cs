using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Agent : MonoBehaviour 
{
	//for environment
	//for our player
    public Transform[] columnsPosition;
	public float speed;
	public GameObject EndDestinations;
	private Vector2 temporaryDestination;
	private bool hitIntersections;
	private bool reset;

	void Awake () 
	{
		hitIntersections = false;
	}
	void Update () 
	{
		GotoDestination();
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "row_red" && hitIntersections == false )
		{
			reset = true;
			HitIntersection(true);
			Vector2 newPos = col.gameObject.transform.position;
			UpdateTemporaryDestinations(newPos);
			Debug.Log("Enter");
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
	}
//    void OnTriggerStay2D( Collider2D coll )
//	{
//		if(coll.gameObject.tag == "row_red"
//		   && Mathf.Abs(this.transform.position.y - coll.transform.position.y) < 0.01
//		   && hitIntersections == false)
//		{
//            Debug.Log("triggered");
//            Debug.Break();
//            HitIntersection(true);
//            travelUpPos = coll.gameObject.transform.localScale.y * 0.5f;
//            UpdateTemporaryDestinations(coll.gameObject.transform.position);
//		}
//	}
	void GotoDestination()
	{
		float step = speed * Time.deltaTime;
		Vector2 updatedPos = new Vector2(0.0f,0.0f);

		if(hitIntersections == true)
		{
			step = speed * Time.deltaTime * 5.0f;
			if(Mathf.Abs(this.transform.position.y - temporaryDestination.y) > 0.05f)	//Navigate Up
			{
				updatedPos = Vector2.MoveTowards(this.transform.position, temporaryDestination, step);
				updatedPos.x = this.transform.position.x;
				Debug.Log("New Pos :" + updatedPos);
				this.transform.position = updatedPos;
			}
			else if(Mathf.Abs(this.transform.position.y - temporaryDestination.y) < 0.05f
			        && Mathf.Abs(this.transform.position.x - temporaryDestination.x) > 0.05f) //Navigate Horisontally
			{
				updatedPos = Vector2.MoveTowards(this.transform.position, temporaryDestination, step);
				updatedPos.y = this.transform.position.y;
				this.transform.position = updatedPos;
			}
			else if(Mathf.Abs(this.transform.position.y - temporaryDestination.y) < 0.05f
			        && Mathf.Abs(this.transform.position.x - temporaryDestination.x) < 0.05f)
			{
				HitIntersection(false);
			}

		}
		else
		{
			updatedPos = Vector2.MoveTowards(this.transform.position, EndDestinations.transform.position, step);
			updatedPos.x = this.transform.position.x;
			this.transform.position = updatedPos;
		}

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
	public void HitIntersection(bool stat)
	{
		hitIntersections = stat;
	}
	private bool TravelToTop()
	{
		return false;
	}
}
