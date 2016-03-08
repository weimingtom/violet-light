using UnityEngine;
//using UnityEditor;
using System.Collections;

public class FXManager : MonoBehaviour {

    static public FXManager Instance;
    public GameObject[] Effects;

    void Awake()
    {
        Instance = this;
    }

    public void Spawn( string Name, string objName = null )
    {
        foreach( GameObject Effect in Effects )
        {
            if( Effect.name.ToLower() == Name.ToLower() )
            {
                Debug.Log( "[FX Manager] Spawned Prop " + Name );
                GameObject newObj = GameObject.Instantiate( Effect, Vector3.zero, Quaternion.identity ) as GameObject;
                if( objName != null )
                    newObj.GetComponent<SurpriseChar>().Init( objName );
                return;
            }
        }
        Debug.Log( "[FX Manager] Cannot find Interactable " + Name);
    }
}