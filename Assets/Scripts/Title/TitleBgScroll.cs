using UnityEngine;
using System.Collections;

public class TitleBgScroll : MonoBehaviour {

    public float speed = 0.1f;
    public float loopPoint = -28.47f;
    private Transform spriteTransform;
	void Update () 
    {
        // TODO(jesse): Fix the hack, seem error, when second one gets pushed over it gets pushed into the first one
        // by about ~10% every other time
        spriteTransform = this.GetComponent<SpriteRenderer>().transform;
        float newX = spriteTransform.position.x - (Time.deltaTime * speed);
        if (newX < loopPoint)
        {
            newX = -loopPoint * (0.5f);
        }
        spriteTransform.position = new Vector2( newX, 0f );
	}
}
