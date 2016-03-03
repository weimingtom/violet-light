using UnityEngine;
using System.Collections;

public class InventoryButton : MonoBehaviour {


    void OnMouseEnter()
    {
        MenuManager.instance.MouseIsAboveInv = true;
        Debug.Log( "[Inv But]Entered Inv Buton" );
    }

    void OnMouseExit()
    {
        MenuManager.instance.MouseIsAboveInv = false;
    }
}
