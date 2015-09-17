using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour {

    private Sprite InteractableSprite;
    private Vector2 CenterPoint;
    public Rect    CollisionBox;
    public float BoxWidth = 5.0f;

	// Use this for initialization
	void Start () 
    {
        InteractableSprite = GetComponent<SpriteRenderer>().sprite;
        CenterPoint = new Vector2( gameObject.transform.position.x, gameObject.transform.position.y);
        CollisionBox.center = CenterPoint;
        CollisionBox.width = BoxWidth;
        CollisionBox.height = BoxWidth;
	}

    bool CheckBoxCollision( Rect CollisionRect, Vector2 Point )
    {
        bool Result = false;

        if( Point.x < CollisionBox.xMin &&
            Point.x > CollisionBox.xMax &&
            Point.y < CollisionBox.yMin &&
            Point.y > CollisionBox.yMax )
            Result = true;

        return Result;
    }

	// Update is called once per frame
	void Update () 
    {
        Vector2 MousePos = new Vector2( Input.mousePosition.x, Input.mousePosition.y);
        MousePos = Camera.main.ScreenToWorldPoint( MousePos );
        Debug.Log( MousePos.x );
        Debug.Log( MousePos.y );
        
        if( Input.GetMouseButtonDown( 0 ) && CheckBoxCollision(CollisionBox,MousePos))
        {
            Debug.Log( "Clicked!" );
        }
	}
}
