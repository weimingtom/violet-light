using UnityEngine;
using System.Collections;

public class ItemPrefab : MonoBehaviour
{
	public string newName;// = "bloody_knife";

	void Update()
	{
		if( Input.GetMouseButtonUp( 0 ) )
		{
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
			if( hitCollider )
			{
				ItemManager.Instance.AddItem(newName);
				Debug.Log(newName);
				Destroy(this.gameObject);
			}
		}
	}
}
