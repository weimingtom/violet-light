using UnityEngine;
using System.Collections;


public class Row : MonoBehaviour 
{
	public SpriteRenderer[] red;
	public SpriteRenderer[] blue;
	public SpriteRenderer[] green;
	public SpriteRenderer[] yellow;

	public GameObject[] redRow; 	
	public GameObject[] blueRow;
	public GameObject[] greenRow;
	public GameObject[] yellowRow;
	void Start()
	{

	}
	void SetRedRowActive(bool status)
	{
		for(int i = 0; i < redRow.Length; i++)
		{
			redRow[i].SetActive(status);
		}
	}
	void SetBlueRowActive(bool status)
	{
		for(int i = 0; i < blueRow.Length; i++)
		{
			blueRow[i].SetActive(status);
		}
	}
	void SetGreenRowActive(bool status)
	{
		for(int i = 0; i < greenRow.Length; i++)
		{
			greenRow[i].SetActive(status);
		}
	}
	void SetYellowRowActive(bool status)
	{
		for(int i = 0; i < yellowRow.Length; i++)
		{
			greenRow[i].SetActive(status);
		}
	}
}
