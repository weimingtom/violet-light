using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//start with one background in the array and it will add a second one beside it and move them accordingly
//parent the background you wish to loop to the Background Gameobject and add it to the backgrounds list.
public class TitleBgScroll : MonoBehaviour {

    public List<GameObject> backgrounds;
    public float speed = -0.1f;
    public float loopPoint = -30.0f;
    Vector3 beside;
    private int current = 0;
    private float spriteSize;
   
    void Start()
    {
        backgrounds[0].transform.position = Vector3.zero;
        spriteSize = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
        beside = new Vector3(backgrounds[0].transform.position.x + spriteSize - 0.05f, backgrounds[0].transform.position.y, backgrounds[0].transform.position.z);
        backgrounds.Add(Instantiate<GameObject>(backgrounds[0]));
        backgrounds[1].transform.position = beside;
    }

    private int notCurrent()
    {
        return current == 1 ? 0 : 1;
    }
	void Update () 
    {
        //move both portraits every frame
        Vector3 movement = new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
        for( int i = 0; i < backgrounds.Count; ++i )
        {
            backgrounds[i].transform.position += movement;
        }

        if( backgrounds[current].transform.position.x < loopPoint )
        { 
            Vector3 newpos = new Vector3(backgrounds[notCurrent()].transform.position.x + spriteSize - 0.05f, backgrounds[notCurrent()].transform.position.y, backgrounds[notCurrent()].transform.position.z);
            backgrounds[current].transform.position = newpos;
            current = notCurrent();
        }

	}
}
