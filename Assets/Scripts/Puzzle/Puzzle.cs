using UnityEngine;
using System.Collections;

public abstract class Puzzle : MonoBehaviour 
{

    public abstract void Initalize();
    public abstract void Submit();
    public abstract void Reset();
	public abstract PuzzleStatus GetStatus();

}
