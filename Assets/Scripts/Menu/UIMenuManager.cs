using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UIMenuManager : MonoBehaviour
{
    public static UIMenuManager instance;
    public GameObject menu;
    public List<GameObject> menuUIs;
    public List<GameObject> mainTabs;
	// Use this for initialization
    void Awake()
    {
        instance = this;
    }

	void Update ()
    {
	
	}
}
