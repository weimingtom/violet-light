using UnityEngine;
using System.Collections;


private class BlinkingItem
{
    public BlinkingItem( GameObject obj, float duration, float life, float dead)
    {
        myObject = obj;
        blinkDuration = duration;
        lifetime = life;
        if( dead < 0.0f )
            deadtime = duration;
        else
            deadtime = dead;
        timeSinceLastBlink = Time.time;
        timeCreated = Time.time;
    }

    public bool UpdateItem()
    {

        return true;
    }


    private GameObject myObject;
    private float blinkDuration; //time between blinks, if deadtime is < 0.0f: deadtime is equal to duration
    private float deadtime; //used only if the duration it is visible is inequivelent to the duration the item is invisible
    private float lifetime; //item will stop blinking after this time (< 0.0f is unlimited time)

    private float timeSinceLastBlink;
    private float timeCreated;
    

}

public class Blink : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
