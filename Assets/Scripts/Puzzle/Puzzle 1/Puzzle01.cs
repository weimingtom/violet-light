using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Puzzle01 : Puzzle
{
	// Get Agent Scripts from these?
	public GameObject AgentA;
	public GameObject AgentB;
	public GameObject Rows;
	
	PuzzleStatus puzzleStatus;
	
	public override void Initalize()
	{
		puzzleStatus = PuzzleStatus.Running;
		//Agent1.instance.Reset ();
		//Agent2.instance.Reset ();
	}
	
	public override PuzzleStatus GetStatus()
	{
		if (Agent1.instance.GetWinStatus () && Agent2.instance.GetWinStatus ())
		{
			puzzleStatus = PuzzleStatus.Win;
		}
		else if(Agent1.instance.GetLostStatus() || Agent2.instance.GetLostStatus())
		{
			puzzleStatus = PuzzleStatus.Lose;
		}
		return puzzleStatus;
		//        if (   AgentA.GetComponent<Agent>().GetWinStatus()
		//            && AgentB.GetComponent<Agent>().GetWinStatus() )
		//        {
		//            puzzleStatus = PuzzleStatus.Win;
		//        }
		//        else if( AgentA.GetComponent<Agent>().GetLostStatus()
		//               ||AgentB.GetComponent<Agent>().GetLostStatus() )
		//        {
		//            puzzleStatus = PuzzleStatus.Lose;
		//        }
		//
		//        return puzzleStatus;
	}
	
	public override void Reset()
	{
		Agent1.instance.Reset ();
		Agent2.instance.Reset ();
		//AgentA.GetComponent<Agent>().Reset();
		//AgentB.GetComponent<Agent>().Reset();
        Row.Instance.Reset();
		//Rows.GetComponent<Row>().    Reset();
	}
	
	public override void Submit()
	{
		Agent1.instance.Submit ();
		Agent2.instance.Submit ();
		//AgentA.GetComponent<Agent>().Submit();
		//AgentB.GetComponent<Agent>().Submit();
	}
}
