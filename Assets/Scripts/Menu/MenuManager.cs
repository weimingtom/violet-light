using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public GameObject menu;
    private Vector2 hiddenPos;
    private Vector2 ActivePos;
    private bool active;

	void Start () 
    {
        hiddenPos = new Vector2(0.0f, Screen.height * 2);
        ActivePos = new Vector2( 0.0f, Screen.height );
        menu.transform.localPosition = hiddenPos;
        active = false;
    }
	

	void Update () 
    {
	    //check if menu button was pressed
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            active = !active;
            if(active)
                OpenMenu();
            else
                CloseMenu();
        }

        //if menu is open test for button presses 
        if( active )
        { 
        
        
        }
        
	}

    private void OpenMenu()
    {
        menu.transform.localPosition = ActivePos;
    }

    private void CloseMenu()
    {
        menu.transform.localPosition = hiddenPos;
    }
}

