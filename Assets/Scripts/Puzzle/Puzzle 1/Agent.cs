using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Agent : MonoBehaviour 
{
	//for environment
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
        CheckMouse(1);
		GotoDestination();
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "row" && hitIntersections == false )
		{
			HitIntersection(true);
			Vector2 newPos = col.gameObject.transform.position;
			UpdateTemporaryDestinations(newPos);
		}
	}
    void CheckMouse(int index)
    {
        if( Input.GetMouseButtonUp( 0 ) )
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
            if( hitCollider )
            {
                switch( hitCollider.transform.name )
                {
                case "blue_row":
                    Row.Instance.BlueRowSwitch();
                    break;
                case "red_row":
                    Row.Instance.RedRowSwitch();
                    break;
                case "green_row":
                    Row.Instance.GreenRowSwitch();
                    break;
                case "yellow_row":
                    Row.Instance.YellowRowSwitch();
                    break;
                }
                Debug.Log("Hit something : "+hitCollider.transform.name);
            }

        }
    }
	void GotoDestination()
	{
		float step = speed * Time.deltaTime;
		Vector2 updatedPos = new Vector2(0.0f,0.0f);
		if(hitIntersections == true)
		{
            
			
			if(Mathf.Abs(this.transform.position.y - temporaryDestination.y) > 0.05f)	//Navigate Up
			{
                step = speed * Time.deltaTime * 8.0f;
				updatedPos = Vector2.MoveTowards(this.transform.position, temporaryDestination, step);
				updatedPos.x = this.transform.position.x;
				this.transform.position = updatedPos;
			}
			else if(Mathf.Abs(this.transform.position.y - temporaryDestination.y) < 0.05f
			        && Mathf.Abs(this.transform.position.x - temporaryDestination.x) > 0.05f) //Navigate Horisontally
			{
                step = speed * Time.deltaTime * 2.0f;
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

}
