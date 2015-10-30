using UnityEngine;
using System.Collections;

public class JesseTest : MonoBehaviour 
{

    private SceneManager BgM;
    private CharacterManager CM;

	// Use this for initialization
	void Start () 
    {
        BgM = SceneManager.Instance;
        Vector3 Pos = new Vector3( 2, 1, -1 );

        InteractableManager.Instance.Spawn( "DefaultProp", Pos );
        Pos = new Vector3(-3, 1, -1 );
        InteractableManager.Instance.Spawn( "DefaultDoor", Pos );

        CM = CharacterManager.Instance;

        CM.characterList.Add( "Violet", new CharacterManager.Character());
        CM.characterList["Violet"].Initialize("Violet Light");
        CM.characterList["Violet"].AddPose("neutral", "Textures/Portraits/Tintin");
	    CM.characterList["Violet"].ChangePose("neutral");
    }

    void Update()
    {

        if( Input.anyKeyDown )
        {
            CM.ChangePosition("Violet", CharacterManager.Positions.Left1);
        }


    }

   



}
