using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ItemInventory : MonoBehaviour
{
    public static ItemInventory Instance;

    int presentButtonLocation = -1;
    Image mainImageHolder;
    Text[] mainTextHolder = new Text[2];
    Button[] buttons;
    
    void Awake()
	{
		Instance = this;
        
	}

    void Start()
    {
        ItemManager.Instance.SetLoadInventory( true );
        Initialize();
        TogglePresentButton(false);
    }

    void Initialize()
    {
		int index = 0;
        for( int i = 0; i < this.transform.childCount; i++ )
        {
            if( this.gameObject.transform.GetChild( i ).tag == "inventory_item_sprites" )
            {
                mainImageHolder = this.gameObject.transform.GetChild( i ).gameObject.GetComponent<Image>();
                mainImageHolder.sprite = Resources.Load<Sprite>( "Textures/Item/no_item" );
            }
            else if( this.gameObject.transform.GetChild( i ).tag == "inventory_item_text" )
            {
				mainTextHolder[index] = this.gameObject.transform.GetChild( i ).gameObject.GetComponent<Text>();
				if(index == 0)
				{
					mainTextHolder[index].text = "No Item";
					index++;
				}
				else
				{
					mainTextHolder[index].text = "~Empty Content~";
				}
            }
        }
		buttons = this.GetComponentsInChildren<Button>();
        for( int i = 0; i < buttons.Length; i++ )
        {
            if( buttons[i].tag == "PresentButton" )
            {
                presentButtonLocation = i;
                buttons[i].onClick.AddListener( () => PresentButton() );
            }
			else
			{
				buttons[i].GetComponentInChildren<Text>().text = "no item";
				buttons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Item/no_item");
			}
        }
    }

    public void ClickButton(int btn_id)
    {
        SetMainImage( btn_id );
    }



    public void TogglePresentButton( bool toggle )
    {
        buttons[presentButtonLocation].gameObject.SetActive(toggle);
    }

    public void PresentButton()
    {
        //`CommandManager.Instance.SetCheckedItem( mainTextHolder[0].text.ToString() );
    }

	void SetMainImage(int buttonIndex)
    {
        if( buttons[buttonIndex].image.sprite.texture.name != "UISprite"
            && buttons[buttonIndex].image.sprite.texture.name != "no_item" )
        {
            TogglePresentButton(true);
            mainImageHolder.sprite = buttons[buttonIndex].image.sprite;
            mainTextHolder[0].text = buttons[buttonIndex].GetComponentInChildren<Text>().text;
            mainTextHolder[1].text = ItemManager.Instance.GetDescriptions( buttons[buttonIndex].GetComponentInChildren<Text>().text );
        }
    }

	public void ResetButton()
	{
		foreach (Button button in buttons) 
		{
            if( button.tag != "PresentButton" )
            {
			    button.image.sprite = Resources.Load<Sprite>( "Textures/Item/no_item" );
			    button.GetComponentInChildren<Text>().text = "empty";
            }
		}
	}
	
    public int GetInventorySize()
	{
		return buttons.Length;
	}
    
    public void SetItemToInventory( Sprite texture, string name, int index )
    {
        buttons[index].image.enabled = true;
        buttons[index].image.sprite = texture;
        buttons[index].GetComponentInChildren<Text>().text = name;
    }
    
    public void DeleteFromInventory(int index)
    {
        buttons[index].enabled = false;
        buttons[index].image.sprite = null;
        buttons[index].GetComponentInChildren<Text>().text = "";
    }
    
    void OnDisable()
    {
        ItemManager.Instance.SetLoadInventory( false );
    }

}
