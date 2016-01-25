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
        int i = 0;
        foreach( Button thisButton in buttons)
        {
            thisButton.onClick.RemoveAllListeners();
            thisButton.onClick.AddListener( () => ClickedButton(thisButton, i) );
            i++;
        }
        
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
    }
    void ClickedButton(Button button, int i)
    {
        switch( i )
        {
        case 0:
        Debug.Log( i );
        break;
        case 1:
        Debug.Log( i );
        break;
        case 2:
        Debug.Log( i );
        break;
        case 3:
        Debug.Log( i );
        break;
        case 4:
        Debug.Log( i );
        break;
        case 5:
        Debug.Log( i );
        break;
        case 6:
        Debug.Log( i );
        break;
        default:
            Debug.Log("fail");
            break;
        }
        
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
