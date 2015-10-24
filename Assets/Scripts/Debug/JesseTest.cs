using UnityEngine;
using System.Collections;

public class JesseTest : MonoBehaviour 
{

    private SceneManager BgM;

	// Use this for initialization
	void Start () 
    {
        BgM = SceneManager.Instance;
        Vector3 Pos = new Vector3( 2, 1, -1 );

        InteractableManager.Instance.Spawn( "DefaultProp", Pos );
        Pos = new Vector3(-3, 1, -1 );
        InteractableManager.Instance.Spawn( "DefaultDoor", Pos );
	}
	
}
