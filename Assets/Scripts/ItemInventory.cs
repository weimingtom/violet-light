using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ItemInventory : MonoBehaviour
{
    Dictionary<string, Item> itemDictionary;
    void Initialize()
    {
        
    }
    void AddItem(string idname, string name, string address)
    {
        Item temporary = new Item();
        temporary.InitializeItem(name, address, false);
        itemDictionary.Add(idname, temporary);
    }
    void GetItem( int index )
    {
        
    }
}
