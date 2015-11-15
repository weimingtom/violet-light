using UnityEngine;
using System.Collections;


public class Row : MonoBehaviour 
{
    enum ColorCode
    { 
        eNoColor,
        eRed,
        eBlue,
        eGreen,
        eYellow
    }
    public GameObject[][] row;
    private Vector2[][] positions;
    private ColorCode[][] color;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
