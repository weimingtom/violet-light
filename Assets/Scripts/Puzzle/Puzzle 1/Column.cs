using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Column : MonoBehaviour 
{
	float yPos;
	public static Column instance;
	void Awake()
	{
		Transform startingColumn;
		SpriteRenderer colSprite = new SpriteRenderer();
		instance = this;
		int count = 0;
		for (int i = 0; i < this.transform.childCount; i++)
		{
			if(this.transform.GetChild(i).name == "second_col")
			{
				startingColumn = this.transform.GetChild(i);
				colSprite = startingColumn.gameObject.GetComponent<SpriteRenderer>();
				count++;
			}
		}
		yPos = colSprite.renderer.bounds.min.y;
	}
	public float GetYPos()
	{
		return yPos;
	}
}
