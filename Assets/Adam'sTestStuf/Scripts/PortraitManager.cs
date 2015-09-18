using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PortraitManager : MonoBehaviour {

	public GameObject [] portraits;
	private int j = 1;

	//enter and exit stuff.
	private int listSize = 0;
	private List<int> portraitList = new List<int>();
	private List<Vector3> destinationList = new List<Vector3>();
	public const float duration = 3.0f; //higher this is the slower the easing 
	public const float deltaAlpha = 0.04f; //0.0 - 1.0



	// Use this for initialization
	void Start () 
	{

	}
	// Update is called once per frame
	void Update () 
	{
		UpdateEnter();


		if(Input.anyKeyDown)
		{
			Enter(0,j);
			j++;
			if(j > 5)
			{
				j = 1;
			}
		}
	}
	

	void UpdateEnter()
	{

		for(int i = 0; i < listSize; ++i)
		{
			//portraits [portraitList[i]].transform.position = destinationList[i];
			//fade in
			float alpha = portraits[portraitList[i]].gameObject.GetComponent<SpriteRenderer>().color.a;
			if(alpha < 1.0f)
				portraits[portraitList[portraitList[i]]].gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,alpha + deltaAlpha);

			//ease out
			Vector3 newposition = new Vector3(portraits[portraitList[i]].transform.position.x, portraits[portraitList[i]].transform.position.y, 0.0f);
			newposition.x += (destinationList[portraitList[i]].x - portraits[portraitList[i]].transform.position.x) / duration;
			newposition.y += (destinationList[portraitList[i]].y - portraits[portraitList[i]].transform.position.y) / duration;
			portraits[portraitList[i]].transform.position = newposition;
			//Remove When Done

			if(portraits[portraitList[portraitList[i]]].transform.position == destinationList[i] && alpha >= 1.0)
			{
				destinationList.RemoveAt(i);
				portraitList.RemoveAt(i);
				listSize--;
			}

		}
	}

	public void Enter(int picIndex, int position)
	{
		//portraits [picIndex].SetActive (!portraits[picIndex].activeSelf);
		Vector3 placeToGo;
		Vector3 startPlace;
		float sX = 0.0f, x = 0.0f;
		float sY = 0.0f, y = 0.0f;
		float z = 0.0f;
		//added stuff for coordinates, mod as needed
		if (Camera.main.aspect >= (16/9)) 
		{
			//therefore ratio = 16/9
			switch(position)
			{
			default:
			case 1:
				x = -6.0f;//mid
				sX = -10.0f;
				break;
			case 2:
				x = -3.0f;
				sX = -6.0f;
				break;
			case 3:
				x = 0.0f;
				sX = 0.0f;
				sY -= 5.0f;
				break;
			case 4:
				x = 3.0f;
				sX = 10.0f;
				break;
			case 5:
				x = 6.0f;
				sX = 10.0f;
				break;
			}

		}
		else
		{
			print ("else");
			switch(position)
			{
			default:
			case 1:
				x = 0;
				break;
			case 2:
				x = x/4;
				break;
			case 3:
				x = x/2;
				break;
			case 4:
				x = x - (x/4);
				break;
			}
		}

		placeToGo.x = x;
		placeToGo.y = y;
		placeToGo.z = 0;
		placeToGo = new Vector3(x, y, z);
		startPlace = new Vector3(sX, sY, z);

		portraits[picIndex].gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
		portraitList.Add(picIndex);
		destinationList.Add(placeToGo);
		portraits[picIndex].transform.position = startPlace;
		listSize++;

	}

}
