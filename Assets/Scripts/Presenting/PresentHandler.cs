using UnityEngine;
using System.Collections;
public class PresentHandler
{
    public string correctItem           { get; set; }

    public void SetPresentHandler( string correct )
    {
        correctItem = correct;
    }

    void Start()
    {
        correctItem = "none";
    }

    public bool CheckItem(string itemPresented)
    {
        if( itemPresented == correctItem )
        {
            Debug.Log( "[Present Handler] Correct" );
            return true;
        }
        Debug.Log( "[Present Handler]Wrong Item, item presented is :" + itemPresented + " corretItem : " + correctItem );
        Debug.Break();
        return false;
    }

}
