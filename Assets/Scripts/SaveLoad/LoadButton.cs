using UnityEngine;
using System.Collections;

public class LoadButton : MonoBehaviour {

    public int id;

    void OnClick()
    {
        SaveLoad.LoadGame( id );
    }
	
}
