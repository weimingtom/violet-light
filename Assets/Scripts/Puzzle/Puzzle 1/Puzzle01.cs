using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Puzzle01 : Puzzle
{
    // Get Agent Scripts from these?
    public GameObject AgentA;
    public GameObject AgentB;

    public override void Initalize()
    {
        // set up witches
    }
    public override bool IsSolved()
    {
        // if witches
        return false;
    }
    public override void Reset()
    {
        // Reset Witches
    }
    void Update()
    {
        //Run witches logic
        //Check if they both reached the end, 
        //if they did either reset or change solved to true
    }
}
