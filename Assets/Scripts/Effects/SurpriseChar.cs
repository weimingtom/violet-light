using UnityEngine;
using System.Collections;

public class SurpriseChar : MonoBehaviour {

    private string characterName;

    private const float stretchValue = 10.0f;
    private const float rotateDeg = -15.0f;
    private const float duration = 0.5f;
    private float counter = 0.0f;
    private short invert = 1;

    private GameObject myCharacter;

	// Use this for initialization
	public void Init (string charName) 
    {
        characterName = charName;
        CharacterManager.Instance.GetCharacter( charName, ref myCharacter, ref invert );
        if( myCharacter == null )
            Destroy( gameObject );
	}
	
	// Update is called once per frame
	void Update () 
    {
        counter += Time.deltaTime;
        //Debug.Break();

        myCharacter.transform.Rotate( new Vector3( 0.0f, 0.0f, 1.0f ), (rotateDeg * Time.deltaTime) * invert );
        myCharacter.transform.localScale = new Vector3( myCharacter.transform.rotation.x + (stretchValue * Time.deltaTime * invert), myCharacter.transform.rotation.y + (stretchValue * Time.deltaTime * invert), 1.0f );

        
        if( counter >= duration )
        {
            myCharacter.transform.rotation = Quaternion.identity;
            myCharacter.transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f );
            Destroy( gameObject );
        }
        else if(counter >= duration * 0.5)
        { 
            invert *= -1;
        }

	}
}
