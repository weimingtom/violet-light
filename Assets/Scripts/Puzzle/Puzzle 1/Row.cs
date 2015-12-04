using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Row : MonoBehaviour 
{
    public static Row Instance;
	
    private List<Transform> RowObject;
    private List<SpriteRenderer> RowSprite;
	
    bool red, blue, green, yellow;
	void Awake()
	{
        RowObject = new List<Transform>();
        RowSprite = new List<SpriteRenderer>();
        red = true;
        blue = true;
        green = true;
        yellow = true;
        Instance = this;
        InitializeRow();
	}
    public void Reset()
    {
        red = true;
        blue = true;
        green = true;
        yellow = true;
        for( int i = 0; i < RowObject.Capacity; i++ )
        {
            RowObject[i].tag = "row";
            RowSprite[i].color = new Color( 1.0f, 1.0f, 1.0f, 1.0f );
        }
    }
    public void RunGame()
    {
        CheckMouse();
    }
	void CheckMouse()
	{
		if( Input.GetMouseButtonUp( 0 ) )
		{
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
			if( hitCollider )
			{
				Switch(hitCollider.transform.name);
			}
		}
	}
    public void InitializeRow()
    {
        for( int i = 0; i < this.transform.childCount; i++ )
        {
            Transform New = this.gameObject.transform.GetChild( i );
            RowObject.Add( this.gameObject.transform.GetChild( i ) );
            RowSprite.Add( RowObject[i].gameObject.GetComponent<SpriteRenderer>() );
        }

    }
    public void Switch(string nameTag)
    {
        string[] tempNameTag = nameTag.Split(' ');
		string[] name;
		bool passedFlag = true;
		switch (tempNameTag [0]) 
		{
		case "blue_row":
			blue = !blue;
			passedFlag = blue;
			break;
		case "red_row":
			red = !red;
			passedFlag = red;
			break;
		case "yellow_row":
			yellow = !yellow;
			passedFlag = yellow;
			break;
		case "green_row":
			green = !green;
			passedFlag = green;
			break;
		}
		for (int i = 0; i < RowSprite.Count; i++) 
		{
			name = RowSprite[i].transform.name.Split(' ');
			if(name[0] == tempNameTag[0])
			{
				switch(passedFlag)
				{
				case true:
                    string[] tagSwap = RowObject[i].tag.Split(':');
					RowObject[i].tag = tagSwap[0];
					RowSprite[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
					break;
				case false:
                    RowObject[i].tag += ":Disabled";
					RowSprite[i].color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
					break;
				}
			}
		}
    }
}
