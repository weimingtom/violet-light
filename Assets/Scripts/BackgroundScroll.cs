using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BackgroundScroll : MonoBehaviour {

	public float leftTreshold;
	public float rightTreshold;
	public float speed;
	private Vector2 position;
	public Button leftButton;
	// Update is called once per frame
	void Update () 
	{
	}
	void OnMouseOver()
	{
		print ("yay");
		Debug.Break();
	}
}
