using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PresentItemManager : MonoBehaviour
{
    public Dictionary<string, PresentHandler> presentItem;
    void Start()
    {
        presentItem = new Dictionary<string, PresentHandler>();
    }
    //TODO: Do this
    void AddItem( string timmingId )
    {
        PresentHandler presentItem;
        //presentItem.SetPresentHandler();
        //presentItem.Add(timmingId, );
    }

}
