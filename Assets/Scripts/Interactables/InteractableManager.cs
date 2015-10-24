using UnityEngine;
using System.Collections;

public class InteractableManager : MonoBehaviour 
{

    public static InteractableManager Instance;

    public GameObject[] Interactables;

    void Awake ()
    {
        Instance = this;
    }

    public void Spawn(string Name, Vector3 Position)
    {
        foreach(GameObject Actable in Interactables)
        {
            if( Actable.name == Name )
            {
                Debug.Log( "[Interactable Manager] Spawned Prop " + Name );
                Instantiate( Actable, Position, Quaternion.identity );
                return;
            }
        }
        Debug.Log( "[Interactable Manager] Cannot find Interactable" + Name );
    }
}
