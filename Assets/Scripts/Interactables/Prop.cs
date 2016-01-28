﻿using UnityEngine;
using System;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class Prop : MonoBehaviour {

    static public Prop Instance;
    public bool IsPickUp;
    public string[] DialougeScene;
    public string[] items;
    public string Character = "";
    string checkedItem = "";
    bool checkItem = false;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if(Character != "")
        {
            SceneManager.Instance.SetChar( Character );
        }
    }
    public void SetCheckedItem(string item)
    {
        checkedItem = "defaultItem";
        foreach( string myitem in items )
        {
            if( item == myitem )
            {
                checkedItem = item;
            }
        }
        checkItem = true;
    }
    void OnMouseDown()
    {
        FileReader.Instance.LoadScene(DialougeScene[SceneManager.Instance.GetQuestStage()]);
        /*
        if( DialougeScene[SceneManager.Instance.GetQuestStage()] != "null" )
        {
            if( SceneManager.Instance.GetChar().Length > 0 && checkItem )
            {
                FileReader.Instance.LoadScene( SceneManager.Instance.GetChar() + "_" + DialougeScene[SceneManager.Instance.GetQuestStage()] + "_" + checkedItem );
                checkItem = false;
            }
            else
            {
                FileReader.Instance.LoadScene( SceneManager.Instance.GetChar() + "_" + DialougeScene[SceneManager.Instance.GetQuestStage()] + "_" + checkedItem );
            }
        }*/
        if( IsPickUp )
        {
            //Add to inventory
            Debug.Log( "[Prop] Picked the item up!" );
            this.gameObject.SetActive(false);
        }
    }
}
