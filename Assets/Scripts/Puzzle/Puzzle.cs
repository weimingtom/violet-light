using UnityEngine;
using System.Collections;

public abstract class Puzzle : MonoBehaviour {

    public abstract void Initalize();
    public abstract bool IsSolved();
    public abstract void Reset();

}
