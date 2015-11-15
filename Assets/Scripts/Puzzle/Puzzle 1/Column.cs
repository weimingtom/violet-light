using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Column : MonoBehaviour 
{
	public GameObject[] bars;
	private Vector2[] positions;
	public string[] spriteName;
    void Start()
    {
        positions = new Vector2[bars.Length];
        InitializePosition();
    }
	public void SetPosition(Vector2 pos, uint index)
	{
        positions[index] = pos;
        bars[index].gameObject.transform.position = positions[index];
	}
	public Vector2 GetPosition(uint index)
	{
		return positions[index];
	}
    private void InitializePosition()
    {
        int length = bars.Length + 1;
        Debug.Log("Length : " + length);
        Vector2 screenArea = new Vector2 ((Screen.width / length) , Screen.height * 0.5f);
        Vector2 tmp;
        for( uint i = 0; i < bars.Length; i++ )
        {
            tmp = screenArea;
            tmp.x = tmp.x * (i + 1);
            SetPosition( tmp, i );
        }
    }

}
