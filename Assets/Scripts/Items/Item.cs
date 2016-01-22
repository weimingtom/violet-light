using UnityEngine;
using System.Collections;

public class Item:MonoBehaviour
{
    private string itemName;
    private string itemDescription;
    private Sprite itemTexture;

    public void InitializeItem(string name, string textureAddress, string description)
    {
        name = itemName;
        itemDescription = description;
        itemTexture = Resources.Load( textureAddress ) as Sprite;
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
