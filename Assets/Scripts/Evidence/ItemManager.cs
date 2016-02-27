using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
public class ItemManager : MonoBehaviour
{
    static public ItemManager Instance;
    List<Item> allItems = new List<Item>();
    List<Item> playerItems = new List<Item>();
    int currentNumberOfItems = 0;
    bool loadInventory = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ParseItem( "Dialogue/items_scene_1" );
    }

    void Update()
    {
		//NOTE(Hendry):Use this for diplaying purposes, use load Inventory to load actual item
		LoadInventory();
    }

    //NOTE(Hendry): adam call this
    public void GetHeldItem( ref List<string> itemHolder )
    {
        foreach( Item itm in playerItems )
        {
            itemHolder.Add( itm.GetItemName() );
        }
    }

    public void SetLoadInventory( bool load )
    {
        loadInventory = load;
    }

	void LoadInventory()
	{
        if( loadInventory )
        {
            //Note(Hendry): check item number change
            if( currentNumberOfItems < playerItems.Count && playerItems.Count > 0 )
            {
                ItemInventory.Instance.ResetButton();
                int index = 0;
                foreach( Item item in playerItems )
                {
                    if( index < ItemInventory.Instance.GetInventorySize() )
                    {
                        ItemInventory.Instance.SetItemToInventory( item.GetItemTexture(), item.GetItemName(), index );
                        index++;
                    }
                }
                currentNumberOfItems = playerItems.Count;
            }
        }//TODO(Hendry): add support for multiple page
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
                ItemInventory.Instance.SetItemToInventory( null, "", i );//Resources.Load<Sprite>( "Textures/Item/no_item"), "empty", i );
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
        bool addItem = false;
        Debug.Log( "[Item Manager] Attempting to add " + item_name );
        for( int i = 0; i < allItems.Count; i++ )
        {
            Debug.Log( "[Item Manager] Comparing item with " + allItems[i].GetItemName() );
            if( allItems[i].GetItemName().ToLower() == item_name )
            {
                addItem = true;
                for( int j = 0; j < playerItems.Count; j++ )
                {
                    // If player already has item don't add it
                    if( playerItems[j].GetItemName().ToLower() == item_name )
                    {
                        Debug.Log( "[Item Manager] Player already has " + item_name );
                        addItem = false;
                        return;
                    }
                }
                if( addItem )
                {
                    playerItems.Add( allItems [i]);
                    return;
                }
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
