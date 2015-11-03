using UnityEngine;
using System.Collections;

public class InteractableManager : MonoBehaviour 
{

    public static InteractableManager Instance;

    public GameObject[] InteractableList;

    private GameObject ParentObject;

    void Awake ()
    {
        Instance = this;
        ParentObject = new GameObject("ParentObject");
    }

    public void Spawn(string Name, Vector3 Position)
    {
        foreach(GameObject Actable in InteractableList)
        {
            if( Actable.name == Name )
            {
                Debug.Log( "[Interactable Manager] Spawned Prop " + Name );
                GameObject Interactable = Instantiate( Actable, Position, 
                                                       Quaternion.identity ) 
                                                       as GameObject;
                Interactable.transform.parent = ParentObject.transform;
                return;
            }
        }
        Debug.Log( "[Interactable Manager] Cannot find Interactable " + Name );
    }

    public void Clear()
    {
        Debug.Log( "[Interactable Manager] Clearing Scene");
        foreach( Transform child in ParentObject.transform )
        {
            Destroy( child.gameObject );
            Debug.Log( "[Interactable Manager] Destroyed " + child.name + "!");
        }
    }
}
