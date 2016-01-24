﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class ItemInventory : MonoBehaviour {

    public static ItemInventory Instance;
    private int size = 0;
	private List<Image> spriteRenderer = new List<Image>();
    private List<Text> textHolder = new List<Text>();
    void Awake()
	{
		Instance = this;
	}
	void Start()
    {
        Initialize();
    }
    void Initialize()
    {
        size = this.transform.childCount;
        for( int i = 0; i < size; i++ )
        {
            if( this.gameObject.transform.GetChild( i ).tag == "inventory_item_sprites" )
            {
				spriteRenderer.Add(this.gameObject.transform.GetChild(i).gameObject.GetComponent<Image>());
            }
            else if( this.gameObject.transform.GetChild( i ).tag == "inventory_item_text" )
            {
                textHolder.Add(this.gameObject.transform.GetChild(i).gameObject.GetComponent<Text>());
            }
        }
    }

    public void SetItemToInventory( Sprite texture, int index )
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
