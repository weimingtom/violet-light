using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ItemInventory : MonoBehaviour
{
    public static ItemInventory Instance;

    int presentButtonIndex = -1;
    Image mainImageHolder;
    Text[] mainTextHolder = new Text[2];

    Button[] buttons;
    Button next;
    Button back;

    void Awake()
	{
		Instance = this;
        Initialize();
	}

    void Start()
    {
        ItemManager.Instance.SetLoadInventory( true );
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
                Color col = mainImageHolder.color;
                col.a = 0.0f;
                mainImageHolder.color = col;
            }
            else if( this.gameObject.transform.GetChild( i ).tag == "inventory_item_text" )
            {
				mainTextHolder[index] = this.gameObject.transform.GetChild( i ).gameObject.GetComponent<Text>();
				if(index == 0)
				{
					mainTextHolder[index].text = "";
					index++;
				}
				else
				{
					mainTextHolder[index].text = "";
				}
            }
        }

		buttons = this.GetComponentsInChildren<Button>();
        for( int i = 0; i < buttons.Length; i++ )
        {
            switch(buttons[i].tag)
            {
            case "PresentButton":
                presentButtonIndex = i;
                buttons[i].onClick.AddListener( () => PresentButton() );
            break;
            default:
            buttons[i].GetComponentInChildren<Text>().text = "";
			buttons[i].GetComponent<Image>().sprite = null;//Resources.Load<Sprite>("Textures/Item/no_item");
            Color col = buttons[i].GetComponent<Image>().color;
            col.a = 0.0f;
            buttons[i].GetComponent<Image>().color = col;
            break;
            }
        }
    }

    void NextButton()
    {

    }

    void BackButton()
    {
        
    }

    public void ClickButton(int btn_id)
    {
        SetMainImage( btn_id );
    }

    public void TogglePresentButton( bool toggle )
    {
        buttons[presentButtonIndex].gameObject.SetActive( toggle );
    }

    public void PresentButton()
    {
        CommandManager.Instance.CheckItem( mainTextHolder[0].text.ToString() );
    }

	void SetMainImage(int buttonIndex)
    {
        if( buttons[buttonIndex].image.sprite.texture.name != null )
        {
            TogglePresentButton(true);
            mainImageHolder.sprite = buttons[buttonIndex].image.sprite;

            Color col = mainImageHolder.color;
            col.a = 1.0f;
            mainImageHolder.color = col;

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
                button.image.sprite = null;
			    button.GetComponentInChildren<Text>().text = "";
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
        string newName = name.Replace('_', ' ');
        buttons[index].GetComponentInChildren<Text>().text = newName;
        Color col = buttons[index].GetComponent<Image>().color;
        col.a = 1.0f;
        buttons[index].GetComponent<Image>().color = col;
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

    public void ResetInventory()
    {    }

}
