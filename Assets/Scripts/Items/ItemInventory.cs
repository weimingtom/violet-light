using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ItemInventory : MonoBehaviour
{
    public static ItemInventory Instance;

    private int size = 0;
    Image mainImageHolder;
    Text[] mainTextHolder = new Text[2];
    Button[] buttons;
    
    void Awake()
	{
		Instance = this;
        ItemManager.Instance.SetLoadInventory(true);
        Initialize();
	}

    void Start()
    {
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
        buttons[0].onClick.AddListener( () => ClickedButton_0() );
        buttons[1].onClick.AddListener( () => ClickedButton_1() );
        buttons[2].onClick.AddListener( () => ClickedButton_2() );
        buttons[3].onClick.AddListener( () => ClickedButton_3() );
        buttons[4].onClick.AddListener( () => ClickedButton_4() );
        buttons[5].onClick.AddListener( () => ClickedButton_5() );
        buttons[6].onClick.AddListener( () => ClickedButton_6() );
        buttons[7].onClick.AddListener( () => PresentButton()   );
    }

    void ClickedButton_0()
    {
		SetMainImage(0);
    }
    void ClickedButton_1()
    {
		SetMainImage(1);
    }
    void ClickedButton_2()
    {
		SetMainImage(2);
    }
    void ClickedButton_3()
    {
		SetMainImage(3);
    }
    void ClickedButton_4()
    {
		SetMainImage(4);
    }
    void ClickedButton_5()
    {
		SetMainImage(5);
    }
    void ClickedButton_6()
    {
		SetMainImage(6);
    }
    void ClickPresent()
    {
        
    }

    public void TogglePresentButton( bool toggle )
    {
        buttons[7].gameObject.SetActive(toggle);
    }
    public void PresentButton()
    {
        Prop.Instance.SetCheckedItem( mainTextHolder[0].text.ToString() );
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
