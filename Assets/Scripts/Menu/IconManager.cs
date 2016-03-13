using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IconManager : MonoBehaviour
{
    enum iconPos
    {
        middle = 0,
        left = 1,
        right = 2
    }
    public static IconManager instance;
    public Transform[] destinationTransforms;
    public Image image;
    float scale;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if( destinationTransforms.Length != 3 )
        {
            Debug.Log("[IconManager]<color=red>no position assigned!</color>");
            //Debug.Break();
        }
        DestroyIcon();
    }
    public void ShowIcon( string iName, string position, float scale )
    {
        
        string itemName = iName.Replace(' ', '_');
        image.sprite = ItemManager.Instance.GetItemTexture( itemName );
        Color col = new Color();
        col = Color.white;
        image.color = col;
        iconPos pos = new iconPos();
        switch( position.ToLower() )
        {
        case "middle":
        case "mid":
        case "m":
        pos = iconPos.middle;
        break;
        case "left":
        case "l":
        pos = iconPos.left;
        break;
        case "right":
        case "r":
        pos = iconPos.right;
        break;
        }
        image.GetComponent<Transform>().position = destinationTransforms[(int)pos].position;
        image.GetComponent<Transform>().localScale *= scale;
    }
    public void DestroyIcon()
    {
        image.sprite = null;
        Color col = new Color();
        col.a = 0;
        image.color = col;
        image.GetComponent<Transform>().position = destinationTransforms[(int)iconPos.middle].position;
        image.GetComponent<Transform>().localScale = Vector3.one;
    }
}
