﻿using UnityEngine;
using System.Collections;

public class SpawnPuzzleCommand : Commands
{
    uint puzzleNumber;
    public void RegisterPuzzleNumber( uint _puzzleNumber )
    {
        Debug.Log("[Spawn Puzzle Command] ID Registered : " + puzzleNumber.ToString());
        puzzleNumber = _puzzleNumber;
    }
    public override bool ExecuteCommand()
    {
        PuzzleManager.Instance.StartPuzzle(puzzleNumber);
        return true;
    }
    public override void PrintData()
    {

    }
    public override void Reset()
    {
        
    }
    public override bool Destroy()
    {
        //throw new System.NotImplementedException();
        return true;
    }
}