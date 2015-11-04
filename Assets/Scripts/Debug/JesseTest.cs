using UnityEngine;
using System.Collections;

public class JesseTest : MonoBehaviour 
{

    private CharacterManager CM;

	// Use this for initialization
	void Start () 
    {


        CM = CharacterManager.Instance;

        CM.characterList.Add("Violet", new CharacterManager.Character());
        CM.characterList["Violet"].Initialize("Violet Light");
        CM.characterList["Violet"].AddPose("neutral", "Textures/Portraits/Tintin");
        CM.characterList["Violet"].ChangePose("neutral");
    }

    void Update()
    {
    
        if( Input.GetKeyDown(KeyCode.Z) )
        {
            CM.ChangePosition("Violet", CharacterManager.Positions.Left1);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            CM.ChangePosition("Violet", CharacterManager.Positions.Left2);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CM.ChangePosition("Violet", CharacterManager.Positions.Centre);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            CM.ChangePosition("Violet", CharacterManager.Positions.Right1);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            CM.ChangePosition("Violet", CharacterManager.Positions.Right2);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            CM.ChangePosition("Violet", CharacterManager.Positions.Offscreen);
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.Instance.ChangeScene( 0 );
        }


    }

   



}
