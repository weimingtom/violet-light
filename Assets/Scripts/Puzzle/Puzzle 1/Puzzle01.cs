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
    public bool Running;

    public override void Initalize()
    {
        puzzleStatus = PuzzleStatus.NotRunning;
        Running = false;

        //AgentA.GetComponent<Agent>().Reset();
        //AgentB.GetComponent<Agent>().Reset();
        //Rows.GetComponent<Row>().Reset();
    }
    public override bool IsSolved()
    {
        if( AgentA.GetComponent<Agent>().ReachTop()
            && AgentA.GetComponent<Agent>().GetWinStatus()
            && AgentB.GetComponent<Agent>().ReachTop()
            && AgentB.GetComponent<Agent>().GetWinStatus())
        {
            puzzleStatus = PuzzleStatus.Win;

            return true;
        }
        else if( AgentA.GetComponent<Agent>().ReachTop()
            && AgentB.GetComponent<Agent>().ReachTop()
            && (AgentA.GetComponent<Agent>().GetWinStatus() == false || !AgentB.GetComponent<Agent>().GetWinStatus() == false))
        {
            puzzleStatus = PuzzleStatus.Lose;
        }
        return false;
    }
    public override void Reset()
    {
       //Reset Witches
       if(puzzleStatus == PuzzleStatus.Running)
       {
            AgentA.GetComponent<Agent>().Reset();
            AgentB.GetComponent<Agent>().Reset();
            Rows.GetComponent<Row>().Reset();
            puzzleStatus = PuzzleStatus.NotRunning;
       }
    }
    public override void RunPuzzle()
    {
        if(puzzleStatus == PuzzleStatus.Running )
        {
            Rows.GetComponent<Row>().RunGame();
            AgentA.GetComponent<Agent>().RunGame();
            AgentB.GetComponent<Agent>().RunGame();
        }
    }
    public override void Submit()
    {
        puzzleStatus = PuzzleStatus.Running;
    }
}
