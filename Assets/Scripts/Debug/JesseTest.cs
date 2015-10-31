using UnityEngine;
using System.Collections;

public class JesseTest : MonoBehaviour 
{

    private CharacterManager CM;

	// Use this for initialization
	void Start () 
    {
        InteractableManager.Instance.Spawn( "TestArea1", Vector3.zero);

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


    }

   



}
