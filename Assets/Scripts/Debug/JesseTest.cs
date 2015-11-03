using UnityEngine;
using System.Collections;

public class JesseTest : MonoBehaviour 
{

    private CharacterManager CM;

	// Use this for initialization
	void Start () 
    {


        /*CM = CharacterManager.Instance;

        CM.characterList.Add( "Violet", new CharacterManager.Character());
        CM.characterList["Violet"].Initialize("Violet Light");
        CM.characterList["Violet"].AddPose("neutral", "Textures/Portraits/Tintin");
	    CM.characterList["Violet"].ChangePose("neutral");*/
    }

    void Update()
    {

        if( Input.anyKeyDown )
        {
           // CM.ChangePosition("Violet", CharacterManager.Positions.Left1);
        }
        if(Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.Instance.ChangeScene( 0 );
        }


    }

   



}
