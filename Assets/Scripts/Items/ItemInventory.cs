using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ItemInventory : MonoBehaviour {

    public static ItemInventory Instance;
    private int size = 16;
    private List<SpriteRenderer> spriteRenderer;

    void Initialize()
    {
        for( int i = 0; i < this.transform.childCount; i++ )
        {
            spriteRenderer.Add(this.gameObject.transform.gameObject.GetComponent<SpriteRenderer>());
        }
    }
    void SetItemToInventory( Sprite texture, int index )
    {
        spriteRenderer[index].enabled = true;
        spriteRenderer[index].sprite = texture;
    }
    void DeleteFromInventory(int index)
    {
        spriteRenderer[index].enabled = false;
        spriteRenderer[index].sprite = null;
    }
}
