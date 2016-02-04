using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PresentItemManager : MonoBehaviour
{
    //string to store when to present, and present handler to store item inside of it
    public Dictionary<string, PresentHandler> presentedItemLibrary;
    public string currentConversationID { get; set; }

    void Start()
    {
        presentedItemLibrary = new Dictionary<string, PresentHandler>();
    }
    //TODO: Do this
    void AddItem( string presentedItem, string correctItem, string timingId)
    {
        PresentHandler presentItem = new PresentHandler();
        presentItem.correctItem = correctItem;
        presentedItemLibrary.Add( timingId, presentItem );
    }

    public bool checkItem(string conversationId, string itemPresented)
    {
        bool check = false;
        if( presentedItemLibrary.ContainsKey( conversationId ) )
        {
            Debug.Log("[PresentItemManager] item presented, it is removed from the library");
            check = presentedItemLibrary[conversationId].CheckItem(itemPresented);
            presentedItemLibrary.Remove( conversationId );
        }
        return check;
    }

}
