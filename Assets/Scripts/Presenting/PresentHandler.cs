using UnityEngine;
using System.Collections;

public class PresentHandler
{
    public string itemPresented         { get; set; }
    public string correctItem           { get; set; }

    public void SetPresentHandler(string presented, string correct, string timming)
    {
        itemPresented = presented;
        correctItem = correct;
    }
    void Start()
    {
        ResetItem();
    }
    void ResetItem()
    {
        itemPresented = "none";
        correctItem = "none";
    }
    public bool PresentItem()
    {
        if( itemPresented == "none" || correctItem == "none" )
        {
            Debug.Log( "[Present Manager] failed to present parameter is null (itemPresented) : " + itemPresented
                + " - (correctItem) : " + correctItem );
            Debug.Break();
            return false;
        }
        else
        {
            ResetItem();
            return CheckItem();
        }
    }

    bool CheckItem()
    {
        if( itemPresented == correctItem )
        {
            return true;
        }
        else
        {
            Debug.Log("Wrong Item");
            Debug.Break();
            return false;
        }
    }

}
