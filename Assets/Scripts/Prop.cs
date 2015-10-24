using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider2D))]
public class Prop : MonoBehaviour {

    public bool IsPickUp;
    public string Description;
    public string DialougeCall;

    void OnMouseDown()
    {
        if(IsPickUp)
        {
            //Add to inventory
            Debug.Log( "[Prop] Picked the item up!" );
        }
	}
}
