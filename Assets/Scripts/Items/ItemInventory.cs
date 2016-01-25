using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class ItemInventory : MonoBehaviour {
    public static ItemInventory Instance;
    private int size = 0;
    Image mainImageHolder;
    Text mainTextHolder;
    //private List<Transform> buttonHolder;
    Button[] buttons;
    void Awake()
	{
		Instance = this;
        buttons = this.GetComponentsInChildren<Button>();
        Initialize();
	}
    void Initialize()
    {
        for( int i = 0; i < this.transform.childCount; i++ )
        {
            if( this.gameObject.transform.GetChild( i ).tag == "inventory_item_sprites" )
            {
                mainImageHolder = this.gameObject.transform.GetChild( i ).gameObject.GetComponent<Image>();
                mainImageHolder.sprite = Resources.Load<Sprite>( "Textures/Item/no_item" );
            }
            else if( this.gameObject.transform.GetChild( i ).tag == "inventory_item_text" )
            {
                mainTextHolder = this.gameObject.transform.GetChild( i ).gameObject.GetComponent<Text>();
                mainTextHolder.text = "No Item Selected\n\nPlease Select Item From Inventory";
            }
        }
        buttons[0].onClick.AddListener(() => ClickedButton_0());
        buttons[1].onClick.AddListener( () => ClickedButton_1() );
        buttons[2].onClick.AddListener( () => ClickedButton_2() );
        buttons[3].onClick.AddListener( () => ClickedButton_3() );
        buttons[4].onClick.AddListener( () => ClickedButton_4() );
        buttons[5].onClick.AddListener( () => ClickedButton_5() );
        buttons[6].onClick.AddListener( () => ClickedButton_6() );
    }
    
    void ClickedButton_0()
    {
        mainImageHolder.sprite = buttons[0].image.sprite;
        mainTextHolder.text = ItemManager.Instance.GetDescriptions(buttons[0].GetComponentInChildren<Text>().text);
        Debug.Log("0");
    }
    void ClickedButton_1()
    {
        mainImageHolder.sprite = buttons[1].image.sprite;
        mainTextHolder.text = buttons[1].GetComponentInChildren<Text>().text;
        Debug.Log( "1" );
    }
    void ClickedButton_2()
    {
        mainImageHolder.sprite = buttons[2].image.sprite;
        mainTextHolder.text = buttons[2].GetComponentInChildren<Text>().text;
        Debug.Log( "2" );
    }
    void ClickedButton_3()
    {
        mainImageHolder.sprite = buttons[3].image.sprite;
        mainTextHolder.text = buttons[3].GetComponentInChildren<Text>().text;
        Debug.Log( "3" );
    }
    void ClickedButton_4()
    {
        mainImageHolder.sprite = buttons[4].image.sprite;
        mainTextHolder.text = buttons[4].GetComponentInChildren<Text>().text;
        Debug.Log( "4" );
    }
    void ClickedButton_5()
    {
        mainImageHolder.sprite = buttons[5].image.sprite;
        mainTextHolder.text = buttons[5].GetComponentInChildren<Text>().text;
        Debug.Log( "5" );
    }
    void ClickedButton_6()
    {
        mainImageHolder.sprite = buttons[6].image.sprite;
        mainTextHolder.text = buttons[6].GetComponentInChildren<Text>().text;
        Debug.Log( "6" );
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

}
