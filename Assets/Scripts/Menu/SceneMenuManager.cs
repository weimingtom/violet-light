using UnityEngine;
using System.Collections;

public class SceneMenuManager : MonoBehaviour
{
    static public SceneMenuManager instance;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	public void EnteredNewScene()
    {
        // Check if there is a character in the scene
            // if yes then show button for PRESENT and for TALK
                // IF THE TALK BUTTON IS PRESSED RUN:
                    //TalkToCharacterInScene()
                // if Present button is pressed
                    //MenuManager.instance.OpenItemMenu
            // if no then don't show those buttons
        // Check if there are things to click on in the background
            // if yes then show INVESTIGATE button
                //if investigate button is clicked HIDE THE UI FOR THIS MENU AND SHOW A BACK BUTTON
                    // IF BACK BUTTON is pressed show this screen again.
        // The player should not be able to click on anything in
        
        // Show move button no matter what
    }

    void TalkToCharacterInScene()
    {
        Prop Character = GameObject.Find(SceneManager.Instance.GetChar()).GetComponent<Prop>();
        Character.Talk();
    }

}
