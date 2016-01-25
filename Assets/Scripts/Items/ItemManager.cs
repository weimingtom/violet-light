using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ItemManager : MonoBehaviour
{
    static public ItemManager Instance;
    List<Item> AllItems = new List<Item>();
    List<Item> PlayerItems = new List<Item>();
    int CurrentNumberOfItems = 0;
	
    void Start()
    {
        Instance = this;
		ParseItem("ItemScript/scene1");
		TemporaryLoad();
    }

    void Update()
    {
    }
    
    //NOTE(Hendry): Used to temporarily fill the inventory
    //The method to fill from inventory should be the same
	void TemporaryLoad()
	{
        for( int i = 0; i < AllItems.Count; i++ )
        {
            ItemInventory.Instance.SetItemToInventory( AllItems[i].GetItemTexture(), AllItems[i].GetItemName(), i );
        }
	}
    public string GetDescriptions(string name)
    {
        string description = "";
        foreach( Item thisItem in AllItems )
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
            AllItems.Add(newItem);
        }
    }
    public void AddItem(string item_name)
    {
        foreach( Item ThisItem in AllItems)
        {
            if( ThisItem.GetItemName() == item_name )
            {
                PlayerItems[CurrentNumberOfItems++] = ThisItem;
                return;
            }
        }
    }
    public void RemoveItem(string item_name)
    {
        for( int i = 0; i < PlayerItems.Count; i++ )
        {
            if( PlayerItems[i].GetItemName() == item_name )
            {
                PlayerItems.RemoveAt(i);
                return;
            }
        }
    }
    
}
