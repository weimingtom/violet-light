using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Row : MonoBehaviour 
{
    public static Row Instance;
	public SpriteRenderer[] redSprite;
	public SpriteRenderer[] blueSprite;
	public SpriteRenderer[] greenSprite;
	public SpriteRenderer[] yellowSprite;

    private List<Transform> RowObject;
    private List<SpriteRenderer> RowSprite; 

    public GameObject[] redRow; 	
	public GameObject[] blueRow;
	public GameObject[] greenRow;
	public GameObject[] yellowRow;
    bool red, blue, green, yellow;
    Transform a;
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
        Debug.Break();
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
        string[] temp = nameTag.Split(' ');
        print("name : " + temp[0]);
    }
    public void RedRowSwitch()
	{
        red = !red;
		for(int i = 0; i < redRow.Length; i++)
		{
			redRow[i].SetActive(red);
            RowSprite[i].color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
		}
	}
    public void BlueRowSwitch()
	{
        blue = !blue;
		for(int i = 0; i < blueRow.Length; i++)
		{
			blueRow[i].SetActive(true);
		}
	}
    public void GreenRowSwitch()
	{
        green = !green;
		for(int i = 0; i < greenRow.Length; i++)
		{
			greenRow[i].SetActive(green);
		}
	}
    public void YellowRowSwitch()
	{
        yellow = !yellow;
		for(int i = 0; i < yellowRow.Length; i++)
		{
			yellowRow[i].SetActive(yellow);
		}
	}
}
