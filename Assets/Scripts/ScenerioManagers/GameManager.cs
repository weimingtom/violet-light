using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    bool newGame = true;

	// Use this for initialization
	void Start () 
    {
        SceneManager.Instance.LoadCase( 1 );
        if (newGame)
            SceneManager.Instance.ChangeScene(0);
        else
            SceneManager.Instance.ChangeScene(SaveLoad.savedGames[0].currentScene());
	}

}
