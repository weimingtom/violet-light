﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class LocationManager : MonoBehaviour
{
    public GameObject[] myButtons;
    public Dictionary<int, List<string>> mData;
    bool initialize = false;
	// Use this for initialization
	void Start () 
    {
        mData = StringParser.Instance.ReadLocationData( "DialougeScripts/LocationSequence" );
        initialize = true;
	}

    void UpdateButton()
    {
        if( initialize == true )
        {
            int key = SceneManager.Instance.GetQuestStage();
            if( mData.ContainsKey( key ) )
            {
                foreach( GameObject go in myButtons )
                {
                    go.SetActive(false);
                }
                for( int i = 0; i < mData[key].Count; i++ )
                {
                    for( int j = 0; j < myButtons.Length; j++ )
                    {
                        if( myButtons[j].transform.name == mData[key][i] )
                        {
                            myButtons[j].SetActive(true);
                        }
                    }
                }
                initialize = false;
            }
            else
            {
                Debug.Log("key does not exists!");
                Debug.Break();
            }
        }
    }

    public void ButtonPressed(string location)
    {
        //note to jesse
        SceneManager.Instance.ChangeScene( location );
    }

	// Update is called once per frame
	void Update ()
    {
        UpdateButton();
	}
}
