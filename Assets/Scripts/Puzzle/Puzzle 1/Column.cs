using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Column : MonoBehaviour 
{
	public Image[] bar;
	public string spriteName;
	private Vector2 position;
    void Start()
    {
		position = new Vector2(0 , 0);
        spriteName = "\0";
    }
	public void SetPosition(Vector2 pos)
	{
		position = pos;
		bar[0].gameObject.transform.position = position;
	}
	public Vector2 GetPosition()
	{
		return position;
	}

}
