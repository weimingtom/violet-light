using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Agent2 : MonoBehaviour 
{
    // NOTE(jesse): testingWitches controls whether the witches should be moving
    // or not, after the submit button is pressed
	static public Agent2 instance;
	private bool runningSimulation;
    private bool touchedCorrectCastle;
	public float movementSpeed;
	public GameObject goalCastle;

    // TODO(jesse): Change this name/functionality?
	private Vector2 seekDestination;
    private bool crossingRow;
    private bool failedPath;

    private Vector2 startPosition;
    
    // NOTE(jesse): Appearence issues, columnWidth helps if you rescale puzzle
    // Zvalue forces everything to that value 
    public float columnWidth = 1.75f;
	void Awake()
	{
		instance = this;
		//Vector2 startPos = new Vector2 (4.0f, -5.0f);
		//this.transform.position = startPos;
	}
    public void Submit()
    {
        Reset();
        runningSimulation = true;
    }

    public void Reset()
    {
        crossingRow = false;
        runningSimulation = false;
        touchedCorrectCastle = false;
        this.transform.localPosition = startPosition;
    }

	void Start () 
	{
        failedPath = false;
        //this.transform.position = new Vector3( this.transform.position.x, Column.instance.GetYPos()*1.2f, ZValue );
        startPosition = this.transform.localPosition;
        Reset();
	}

	void Update()
	{

        if( runningSimulation )
        {
            GotoDestination();
            
            if( this.transform.localPosition.y >= goalCastle.transform.localPosition.y)
            {
                runningSimulation = false;
                if(!touchedCorrectCastle)
                    failedPath = true;
            }
        }
    }
    public bool GetLostStatus()
    {
        return failedPath;
    }
    public bool GetWinStatus()
    {
        return touchedCorrectCastle;
    }

	void OnTriggerEnter2D(Collider2D col)
	{
        if( col.gameObject.tag == "row" && crossingRow == false )
		{
            crossingRow = true;
			Vector2 newPos = col.gameObject.transform.localPosition;
			UpdateSeekDestination(newPos);
		}
        else if( col.gameObject.tag == "destination" + this.transform.name )
        {
            touchedCorrectCastle = true;
        }
	}

	void GotoDestination()
	{
		float step = movementSpeed * Time.deltaTime;

        if( crossingRow == true )
		{	
            // NOTE(hendry): The moves the agent first to the center of the row, before it starts moving
            // horizontally
            if( this.transform.localPosition.y <= seekDestination.y )
			{
                seekDestination.x = this.transform.localPosition.x;
			}
            // NOTE(jesse): else move horizontally
			else if(Mathf.Abs(this.transform.localPosition.x - seekDestination.x) > 0.01f
			        && this.transform.localPosition.y >= seekDestination.y) 
            {
                seekDestination.y = this.transform.localPosition.y;			
            }
			else
			{
				crossingRow = false;
			}
		}
		else
		{
            seekDestination = goalCastle.transform.localPosition;
            seekDestination.x = this.transform.localPosition.x;
		}
        this.transform.localPosition = Vector3.MoveTowards( this.transform.localPosition, seekDestination, step );
    }

	public void UpdateSeekDestination(Vector2 destination)
	{
        seekDestination = destination;

        // NOTE(hendry): this means that the bar is on the left of agent
        if( this.transform.localPosition.x > destination.x ) 
        {
            seekDestination.x -= columnWidth;
        }
        else 
        {
            seekDestination.x += columnWidth;
        }
	}

}
