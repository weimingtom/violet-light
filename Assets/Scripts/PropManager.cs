using UnityEngine;
using System.Collections;

public class PropManager : MonoBehaviour 
{

    public static PropManager Instance;

    public GameObject[] Props;

    void Awake ()
    {
        Instance = this;
    }

    public void Spawn(string Name, Vector3 Position)
    {
        foreach(GameObject CurrentProp in Props)
        {
            if(CurrentProp.name == Name)
            {
                Debug.Log( "[Prop Manager] Spawned Prop " + Name );
                Instantiate( CurrentProp, Position, Quaternion.identity);
                return;
            }
        }
        Debug.Log( "[Prop Manager] Cannot find Prop " + Name );
    }
}
