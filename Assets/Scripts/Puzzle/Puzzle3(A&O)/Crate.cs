using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour {

    public int id;
    public int sign;

    void OnMouseDown()
    {
        SendMessageUpwards( "Clicked", id );
    }
}
