using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AnimatedMenuManager : MonoBehaviour
{
    public List<GameObject> menuLayout;
    public List<GameObject> menuButton;
	// Use this for initialization
	void Start ()
    {
        GameObject[] allGo = this.GetComponentsInChildren<GameObject>();
        foreach( GameObject go in allGo )
        {
            switch( go.tag )
            {
                case "menu_bg":
                menuLayout.Add(go);
                break;
                case "button_bg":
                menuButton.Add(go);
                break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
