using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(Collider2D))]
public class Prop : MonoBehaviour {

    public bool IsPickUp;
    public string Description  = "null";
    public string DialougeScene = "null";
    public string DialougeString = "null";

    void OnMouseDown()
    {
        if( IsPickUp )
        {
            //Add to inventory
            Debug.Log( "[Prop] Picked the item up!" );
            Destroy( this.gameObject );
        }
        if(DialougeString != "null")
        {
            FileReader.Instance.LoadScene( DialougeScene,DialougeString );
        }
    }
}
