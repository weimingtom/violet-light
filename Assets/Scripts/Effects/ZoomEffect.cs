using UnityEngine;
using System.Collections;

public class ZoomEffect : MonoBehaviour {

    public float endlag = 0.5f;
    public float zoomTime = 3.0f;
    private float counter = 0.0f;

    private Vector3 camOrigPos;
    private float camOrigSize;
    private float camNewSize = 0.5f;

    private Vector3 position;
    private Vector3 offset = new Vector3(-0.43f, 3.75f, -7.0f);
    private GameObject myCharacter;
    private short side;

    //CameraPos -3.43, 3.25, -10 Size: 0.5
    //SharpPos  -3.00, -0.5, -3.0

	void Start () 
    {
        CharacterManager.Instance.GetCharacter( "sharp", ref myCharacter, ref side );
        position = myCharacter.transform.position + offset;
        camOrigPos = Camera.main.transform.position;
        camOrigSize = Camera.main.orthographicSize;

        if( myCharacter == null )
        {
            Debug.Log( "What are you doing you dingus!" );
            Destroy( gameObject );
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        counter += Time.deltaTime;
        if( counter <= zoomTime )
        {
            float percentage = counter / (zoomTime - endlag);
            //Camera.main.transform.position = Vector3.Lerp( camOrigPos, position, Mathf.SmoothStep( 0.0f, 1.0f, percentage ) );
            //Camera.main.orthographicSize = Mathf.Lerp( camOrigSize, camNewSize, Mathf.SmoothStep( 0.0f, 1.0f, percentage ) );
            Camera.main.transform.position = Vector3.Lerp( camOrigPos, position, percentage );
            Camera.main.orthographicSize = Mathf.Lerp( camOrigSize, camNewSize, percentage );
        }
        else 
        {
            Camera.main.transform.position = camOrigPos;
            Camera.main.orthographicSize = camOrigSize;
            Destroy( gameObject );
        }
	}
}
