﻿using UnityEngine;
using System.Collections;

public class Item:MonoBehaviour
{
    private string itemName;
    private string itemDescription;
    private Sprite itemTexture;

    public void InitializeItem(string name, string textureAddress, string description)
    {
        itemName = name;
        itemDescription = description;
        itemTexture = Resources.Load<Sprite>( textureAddress );
        if(itemTexture == null)
        {
            itemTexture = Resources.Load<Sprite>( "Textures/Item/no_item" );
        }
    }

    public string GetItemName()
    {
        return itemName;
    }

    public string GetItemDescriptions()
    {
        return itemDescription;
    }

    public Sprite GetItemTexture()
    {
        return itemTexture;
    }
}
