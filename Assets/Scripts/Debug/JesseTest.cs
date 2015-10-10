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
	    if(Input.GetKeyDown(KeyCode.Space))
        {
            BgM.ChangeBackground("test2");
        }
	}
}
