using UnityEngine;
using System.Collections;

public abstract class Puzzle : MonoBehaviour {

    public abstract void Initalize();
    public abstract bool IsSolved();
    public abstract void Submit();
    public abstract void Reset();
    //public abstract void RunPuzzle();

}
