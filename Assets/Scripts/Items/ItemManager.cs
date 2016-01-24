using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ItemManager : MonoBehaviour
{
    List<Item> AllItems = new List<Item>();
    List<Item> PlayerItems = new List<Item>();
    int CurrentNumberOfItems = 0;
	Sprite test;
	Image mainImageHolder;
	Text mainTextHolder;
    void Start()
    {
		ParseItem("ItemScript/scene1");
		TemporaryLoad();
    }
	void Initialize()
	{
		for(int i = 0; i < this.transform.childCount; i++)
		{
			if( this.gameObject.transform.GetChild( i ).tag == "inventory_item_sprites" )
			{
				mainImageHolder = this.gameObject.transform.GetChild(i).gameObject.GetComponent<Image>();
				test = Resources.Load<Sprite>("Textures/Item/no_item");
				
				mainImageHolder.sprite = test;
			}
			else if( this.gameObject.transform.GetChild( i ).tag == "inventory_item_text" )
			{
				mainTextHolder = this.gameObject.transform.GetChild(i).gameObject.GetComponent<Text>();
				mainTextHolder.text = "No Item Selected\n\nPlease Select Item From Inventory";
			}
		}
	}
	void TemporaryLoad()
	{
		for(int i = 0; i < AllItems.Count; i++)
		{
			ItemInventory.Instance.SetItemToInventory(AllItems[i].GetItemTexture(),i);
		}
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
