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
    float travelUpPos;
    bool goUp;
	void Awake () 
	{
     
		hitIntersections = false;
	}
	void Update () 
	{
		GotoDestination();
	}
    void OnTriggerStay2D( Collider2D coll )
	{
		if(coll.gameObject.tag == "row_red"
		   && Mathf.Abs(this.transform.position.y - coll.transform.position.y) < 0.01
		   && hitIntersections == false)
		{
            Debug.Log("triggered");
            Debug.Break();
            HitIntersection(true);
            travelUpPos = coll.gameObject.transform.localScale.y * 0.5f;
            UpdateTemporaryDestinations(coll.gameObject.transform.position);
		}
	}
    void OnTriggerExit2D(Collider2D coll)
    {
        if( coll.gameObject.tag == "row_red" )
        {
            goUp = false;
            HitIntersection(false);
        }
    }
	void GotoDestination()
	{
		float step = speed * Time.deltaTime;
		Vector2 updatedPos = new Vector2(0.0f,0.0f);
        if( Mathf.Abs( this.transform.position.x - temporaryDestination.x) < 0.01)
        {
            Debug.Log("swap");
            this.transform.position = temporaryDestination; 
            goUp = true;
            //HitIntersection( false );
        }
        else if( Mathf.Abs( this.transform.position.y - travelUpPos ) < 0.01 )
        {
            goUp = false;
        }
        else
        {
		    /*
			    Note :
			    if it hit intersection therefore move through x -> y is the same
			    else go navigate through y -> x is the same
		     */
		    switch(hitIntersections)
		    {
		    case true:
			    updatedPos = Vector2.MoveTowards(this.transform.position, temporaryDestination, step);
			    updatedPos.y = this.transform.position.y;
			    break;
		    case false:
			    updatedPos = Vector2.MoveTowards(this.transform.position, EndDestinations.transform.position, step);
			    updatedPos.x = this.transform.position.x;
			    break;
		    }

            switch( goUp )
            {
            case true:
                Vector2 position = new Vector2( this.transform.position.x, travelUpPos );
                updatedPos = Vector2.MoveTowards(this.transform.position, position, step);
                break;
            }
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
}
