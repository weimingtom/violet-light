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

    public void Spawn( string Name )
    {
        foreach( GameObject Effect in Effects )
        {
            if( Effect.name == Name )
            {
                Debug.Log( "[FX Manager] Spawned Prop " + Name );
                Instantiate( Effect, Vector3.zero, Quaternion.identity );
                return;
            }
        }
        Debug.Log( "[FX Manager] Cannot find Interactable " + Name);
    }
}