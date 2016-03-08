using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class locationManager : MonoBehaviour
{
    public static locationManager Instance;
    public GameObject[] myButtons;
    public Dictionary<int, List<string>> mData = new Dictionary<int,List<string>>();
    public bool initialize { get; set; }
	// Use this for initialization

    void Awake()
    {
        Instance = this;
        initialize = true;
    }

    void Start()
    {
        StringParser.Instance.ReadLocationData( ref mData ,"Dialogue/locations_scene_1" );// + SceneManager.Instance.GetScene().ToString());
        UpdateButton();

    }

    public void UpdateButton()
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
                Debug.Log("<color=red>key does not exists!</color> key passed : " + key);
                //Debug.Break();
            }
        }
    }
    public void SetButton(string btnName , bool isOn )
    {
        foreach( GameObject go in myButtons )
        {
            if( go.transform.name == btnName )
            {
                go.SetActive(isOn);
                break;
            }
        }
    }
    public void ButtonPressed(string location)
    {
        //note to jesse
        SceneManager.Instance.ChangeScene( location );
        MenuManager.instance.CloseMenu();
    }

	// Update is called once per frame
    void OnEnable()
    {
        initialize = true;
        UpdateButton();
	}
}
