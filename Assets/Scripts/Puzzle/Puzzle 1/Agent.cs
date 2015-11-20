using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Agent : MonoBehaviour 
{
	//for environment
	public GameObject[] col;
	private Vector2[] colPos;
	//for our player
	public float speed;
	public GameObject EndDestinations;
	private Vector2 temporaryDestination;
	private bool hitIntersections;

	void Awake () 
	{

		hitIntersections = false;
	}
	void Update () 
	{
		GotoDestination();
	}
	void OnTriggerStay2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "row_red"
		   && Mathf.Abs(this.transform.position.y - coll.transform.position.y) < 0.01
		   && hitIntersections == false)
		{
            HitIntersection(true);
            UpdateTemporaryDestinations(coll.gameObject.transform.position);
		}
	}


	void GotoDestination()
	{
		float step = speed * Time.deltaTime;
		Vector2 updatedPos = new Vector2(0.0f,0.0f);
        if( Mathf.Abs( this.transform.position.x - temporaryDestination.x) < 0.01 && hitIntersections == true)
        {

            HitIntersection( false );
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
		    this.transform.position = updatedPos;
        }
	}
	public void UpdateTemporaryDestinations(Vector2 destination)
	{
		temporaryDestination = destination;
        temporaryDestination.x += (Mathf.Abs(temporaryDestination.x * 0.5f) - 1);
	}
	public void HitIntersection(bool inter)
	{
		hitIntersections = true;
	}
}
