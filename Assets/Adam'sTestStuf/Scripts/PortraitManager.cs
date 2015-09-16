using UnityEngine;
using System.Collections;

public class PortraitManager : MonoBehaviour {

	public GameObject [] portraits;
	public Transform [] transfoms;
	private int i = 1;

	// Use this for initialization
	void Start () 
	{
		
	}
	// Update is called once per frame
	void Update () 
	{
		if(Input.anyKeyDown)
		{
			Enter(0,i);
			Debug.Log("i"+i);
			i++;
			if(i > 4)
			{
				i = 1;
			}
		}
	}

	public void Enter(int picIndex, int position)
	{
		//portraits [picIndex].SetActive (!portraits[picIndex].activeSelf);
		Vector3 PlaceToGo;
		float x = Screen.width;
		float y = Screen.height / 2;
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
		PlaceToGo.x = x;
		PlaceToGo.y = y;
		PlaceToGo.z = 0;
	
		Debug.Log("x"+x);
		//portraits[picIndex].transform.Translate(PlaceToGo , Space.World);

		portraits[picIndex].rigidbody2D.transform.rigidbody2D.transform.rigidbody2D.transform.rigidbody2D.transform.rigidbody2D.transform.position.Set = PlaceToGo;
		//portraits[picIndex].transform.position.Set(PlaceToGo.x, PlaceToGo.y, 0);
		//portraits[picIndex].gameObject.transform.position.Set(PlaceToGo.x, PlaceToGo.y, 0);
		//portraits[picIndex].rigidbody2D.transform.position.Set(PlaceToGo.x, PlaceToGo.y, 0);

	}

}
