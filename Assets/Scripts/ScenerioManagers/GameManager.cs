using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    bool newGame = true;
    public GameObject cutscene;

	// Use this for initialization
	void Start () 
    {
        SceneManager.Instance.LoadCase( 1 );
        FadeOutScreen.instance.BeginFade( -1 );
        if( newGame )
        { 
            //SceneManager.Instance.ChangeScene(0);
            GameObject newCutscene = Instantiate(cutscene);
        }
        else
        {
            SceneManager.Instance.LoadGame(SaveLoad.savedGames[0]);
        }
	}

}
