using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ItemManager : MonoBehaviour
{
    static public ItemManager Instance;
    List<Item> allItems = new List<Item>();
    List<Item> playerItems = new List<Item>();
    int currentNumberOfItems = 0;
	
    void Start()
    {
        Instance = this;
		ParseItem("ItemScript/scene1");
		//Use this for temporary purposes
		//TemporaryLoad();
    }
    void Update()
    {
		//NOTE(Hendry):Use this for diplaying purposes, use load Inventory to load actual item
		LoadInventory();
    }
	void LoadInventory()
	{
		//Note(Hendry): check if the number is bigger than the number of slots
		if (currentNumberOfItems != playerItems.Count && playerItems.Count > 0)
		{
			ItemInventory.Instance.ResetButton();
			int index = 0;
			foreach(Item item in playerItems)
			{
				if(index < ItemInventory.Instance.GetInventorySize())
				{
					ItemInventory.Instance.SetItemToInventory(item.GetItemTexture(), item.GetItemName(), index);
					index++;
				}
			}
		}
		//TODO(Hendry): add support for multiple page
	}
	void ResetInventory()
	{

	}
	void TemporaryLoad()
	{
		int index = 0;
		for( int i = 0; i < ItemInventory.Instance.GetInventorySize(); i++ )
        {
			if(index < allItems.Count)
			{
				ItemInventory.Instance.SetItemToInventory( allItems[i].GetItemTexture(), allItems[i].GetItemName(), i );
				index++;
			}
			else
			{
				ItemInventory.Instance.SetItemToInventory( Resources.Load<Sprite>( "Textures/Item/no_item"), "empty", i );
			}
        }
	}
    public string GetDescriptions(string name)
    {
        string description = "";
        foreach( Item thisItem in allItems )
        {
            if( thisItem.GetItemName() == name )
            {
                description = thisItem.GetItemDescriptions();
            }
        }
        return description;
    }
    public void ParseItem(string itemAddress)
    {
        char[] delimiterChar = { '\r', '\n' };
        char[] otherDelimiter = { ':' };
        string[] extractedWord = (Resources.Load( itemAddress ) as TextAsset).ToString().Split( delimiterChar, System.StringSplitOptions.RemoveEmptyEntries );
        string name = "";
        string textureAddress = "";
        string description = "";
        int count = 0;
        for( int i = 0; i < extractedWord.Length; i++ )
        {
            count = 0;
			name = "";
			textureAddress = "";
			description = "";
            for( int j = 0; j < extractedWord[i].Length; j++ )
            {
                if( extractedWord[i][j] == ' ' && count < 2)
                {
                    count++;
                }
                else
                {
                    if( count == 0 )
                    {
                        name += extractedWord[i][j];
                    }
                    else if( count == 1 )
                    {
                        textureAddress += extractedWord[i][j];
                    }
                    else if( count == 2 && extractedWord[i][j] != '"')
                    {
                        description += extractedWord[i][j];
                    }
                }
            }
            Item newItem =  new Item();
            newItem.InitializeItem(name, textureAddress, description);
            allItems.Add(newItem);
        }
    }
    public void AddItem(string item_name)
    {
        foreach( Item ThisItem in allItems)
        {
			try {
				
			} 
			catch (System.Exception ex) {
				
			}
            if( ThisItem.GetItemName() == item_name
			   && !playerItems.Contains(ThisItem))
            {
                playerItems.Add(ThisItem);
                return;
            }
			if(playerItems.Contains(ThisItem))
			{	
				Debug.Log("already in inventory");
			}
        }
    }
    public void RemoveItem(string item_name)
    {
		foreach(Item item in playerItems)
		{
			if(item.GetItemName() == item_name)
			{
				playerItems.Remove(item);
				return;
			}
		}
    }
    
}
