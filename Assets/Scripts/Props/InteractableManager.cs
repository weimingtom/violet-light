using UnityEngine;
using System.Collections;

public class InteractableManager : MonoBehaviour 
{

    public static InteractableManager Instance;

    public GameObject[] InteractableList;

    private GameObject ParentObject;
    public bool doneLoading { get; set; }
    public int numberOfObject = 0;
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
                numberOfObject++;
                return;
            }
        }
        Debug.Log( "[Interactable Manager] Cannot find Interactable " + Name);
    }

    public int GetNumberOfInteractable()
    {
        return ParentObject.transform.GetChild(0).childCount;
    }
    public bool IsOnlyCharacterInScene()
    {
        bool check = false;
        if( CharacterManager.Instance.IsCharacter( ParentObject.transform.GetChild( 0 ).GetChild( 0 ).name )
            && GetNumberOfInteractable() == 1 )
        {
            check = true;
        }
        return check;
    }
    public void Clear()
    {
        doneLoading = false;
        Debug.Log( "[Interactable Manager] Clearing Scene");
        foreach( Transform child in ParentObject.transform )
        {
            Destroy( child.gameObject );
            Debug.Log( "[Interactable Manager] Destroyed " + child.name + "!");
        }
    }
}
