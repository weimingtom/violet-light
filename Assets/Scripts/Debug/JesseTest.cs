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
        CM.characterList["Violet"].AddPose("neutral", "Textures/Portraits/violet_neutral");
        CM.characterList["Violet"].ChangePose("neutral");

        CM.characterList.Add( "Alexander", new CharacterManager.Character() );
        CM.characterList["Alexander"].Initialize( "Alexander Strong" );
        CM.characterList["Alexander"].AddPose( "neutral", "Textures/Portraits/alexander_neutral" );
        CM.characterList["Alexander"].ChangePose( "neutral" );

        CM.characterList.Add( "Sharpe", new CharacterManager.Character() );
        CM.characterList["Sharpe"].Initialize( "Detective Sharpe" );
        CM.characterList["Sharpe"].AddPose( "neutral", "Textures/Portraits/sharp_neutral" );
        CM.characterList["Sharpe"].ChangePose( "neutral" );
    }

    void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.Instance.ChangeScene( 0 );
        }

    }
}
