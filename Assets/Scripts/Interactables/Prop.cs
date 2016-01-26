using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(Collider2D))]
public class Prop : MonoBehaviour {

    public bool IsPickUp;
    public string DialougeScene = "null";

    void OnMouseDown()
    {
        if(/*DialougeString != "null"*/
            DialougeScene != "null"
            )
        {
            FileReader.Instance.LoadScene( DialougeScene/*, DialougeString*/);
        }
        if( IsPickUp )
        {
            //Add to inventory
            Debug.Log( "[Prop] Picked the item up!" );
            this.gameObject.SetActive(false);
        }
    }
}
