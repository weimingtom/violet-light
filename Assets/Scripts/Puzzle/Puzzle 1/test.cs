using UnityEngine;
using System.Collections;

public class test : MonoBehaviour 
{
	void Update()
	{
		Vector2 pos = this.gameObject.transform.position;
		pos.y += 0.01f;
		this.gameObject.transform.position = pos;
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "test")
		{
			print("test collision");
			Debug.Break();
		}
	}

}
