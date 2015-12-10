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
    }

    public override PuzzleStatus GetStatus()
    {
        if (   AgentA.GetComponent<Agent>().GetWinStatus()
            && AgentB.GetComponent<Agent>().GetWinStatus() )
        {
            puzzleStatus = PuzzleStatus.Win;
        }
        else if( AgentA.GetComponent<Agent>().GetLostStatus()
               ||AgentB.GetComponent<Agent>().GetLostStatus() )
        {
            puzzleStatus = PuzzleStatus.Lose;
        }

        return puzzleStatus;
    }

    public override void Reset()
    {
        AgentA.GetComponent<Agent>().Reset();
        AgentB.GetComponent<Agent>().Reset();
        Rows.GetComponent<Row>().    Reset();
    }

    public override void Submit()
    {
        AgentA.GetComponent<Agent>().Submit();
        AgentB.GetComponent<Agent>().Submit();
    }
}
