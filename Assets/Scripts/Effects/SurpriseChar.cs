using UnityEngine;
using System.Collections;

public class SurpriseChar : MonoBehaviour {

    //private string characterName;

    private float stretchValue = 2.0f;
    private float rotateDeg = -80.0f;
    private const float duration = 0.18f;
    private float counter = 0.0f;
    private short invert = 1;
    private bool once = false;

    private GameObject myCharacter;

    //come back to this later and fix invert. I did it in a dumb way...

	// Use this for initialization
	public void Init (string charName) 
    {
        //characterName = charName;
        CharacterManager.Instance.GetCharacter( charName, ref myCharacter, ref invert );
        stretchValue*= invert;
        if( myCharacter == null )
            Destroy( gameObject );
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        counter += Time.deltaTime;

        myCharacter.transform.Rotate( new Vector3( 0.0f, 0.0f, 1.0f ), (rotateDeg * Time.deltaTime) * invert );
        myCharacter.transform.localScale = new Vector3( myCharacter.transform.localScale.x + (stretchValue * Time.deltaTime * invert), myCharacter.transform.localScale.y + (stretchValue * Time.deltaTime * invert), 1.0f );

        if( !once && counter >= duration * 0.5 )
        { 
            invert *= -1;
            once = true;
        }
        if( counter >= duration )
        {
            myCharacter.transform.rotation = Quaternion.identity;
            myCharacter.transform.localScale = new Vector3( -1.0f * invert, 1.0f, 1.0f );
            Destroy( gameObject );
        }

	}
}
