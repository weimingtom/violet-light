using UnityEngine;
using System.Collections;

[RequireComponent( typeof( Collider2D ) )]
public class Door : MonoBehaviour
{
    public int RoomToChangeTo;
    public bool IsUnlocked = true;

    void OnMouseDown()
    {
        if( IsUnlocked )
        {
            SceneManager.Instance.OpenSecondaryScene( RoomToChangeTo );
        }
   }
}
