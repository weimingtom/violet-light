using UnityEngine;
using System.Collections;

public class JesseTest : MonoBehaviour 
{

    private SceneManager BgM;

	// Use this for initialization
	void Start () 
    {
        BgM = SceneManager.Instance;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            BgM.ChangeScene(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            BgM.ChangeScene(1);

	}
}
