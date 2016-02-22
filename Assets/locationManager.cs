using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class locationManager : MonoBehaviour
{
    GameObject[] myButtons;
	// Use this for initialization
	void Start () 
    {
        myButtons = this.GetComponentsInChildren<GameObject>();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
