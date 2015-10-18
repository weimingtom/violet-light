using UnityEngine;
using System.Collections;

public class JesseTest : MonoBehaviour 
{

    private BackgroundManager BgM;

	// Use this for initialization
	void Start () 
    {
        BgM = gameObject.GetComponent<BackgroundManager>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            BgM.ChangeBackground("test2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            BgM.ChangeBackground( "test1", 0.7f );

	}
}
