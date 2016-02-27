using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SuspectManager : MonoBehaviour
{
    struct SuspectHolder
    {
        public Image suspectImage;
        public Text suspectDescriptions;
    }
    SuspectHolder mySuspect;
    GameObject myGo;
	// Use this for initialization
	void Start ()
    {
	    mySuspect = new SuspectHolder();
        mySuspect.suspectImage.transform.position = new Vector2( 100, 100 );
        mySuspect.suspectDescriptions.transform.position = new Vector2( 100, 100 );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
