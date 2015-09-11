using UnityEngine;
using System.Collections;

public class PortraitManager : MonoBehaviour {

	public GameObject [] portraits;

	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void Enter(int portrait, int position)
	{
		portraits [portrait].SetActive (false);


	}

}
