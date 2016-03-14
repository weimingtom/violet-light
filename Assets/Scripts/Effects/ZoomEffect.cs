using UnityEngine;
using System.Collections;

public class ZoomEffect : MonoBehaviour {

    public float zoomTime = 3.0f;
    public Vector3 position;
    private GameObject myCharacter;
    private short side;

    //CameraPos -3.43, 3.25, -10 Size: 0.5
    //SharpPos  -3.00, -0.5, -3.0

	void Start () 
    {
        CharacterManager.Instance.GetCharacter( "sharp", ref myCharacter, ref side );
        if( myCharacter == null )
        {
            Debug.Log( "What are you doing you dingus!" );
            Destroy( gameObject );
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	    


	}
}
