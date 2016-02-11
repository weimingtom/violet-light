using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Blink : MonoBehaviour {

  
    public void Initialize( GameObject obj, float duration = 1.0f, float life = -1.0f, float dead = -1.0f )
    {
        myObject = obj;
        alivetime = duration;
        lifetime = life;
        if( dead < 0.0f )
            deadtime = duration;
        else
            deadtime = dead;
        timeSinceLastBlink = Time.time;
        timeCreated = Time.time;

        alive = true;
        myObject.SetActive( true );
    }

    public void Update() //redo this. this is realy bad. 
    {

        if( !(lifetime >= 0.0f && timeCreated + lifetime < Time.time) )
        {
            if( alive )
            {
                if( timeSinceLastBlink + alivetime > Time.time )
                {
                    myObject.SetActive( false );
                    alive = !alive;
                }
            }
            else
            {
                if( timeSinceLastBlink + deadtime > Time.time )
                {
                    myObject.SetActive( true );
                    alive = !alive;
                }
            }
        }
        else
        {
            if(!alive)
            {
                myObject.SetActive(true);
                alive = true;
            }
        }
    }


    private GameObject myObject;
    private float alivetime; //time between blinks, if deadtime is < 0.0f: deadtime is equal to alivetime
    private float deadtime;  //used only if the duration it is visible is inequivelent to the duration the item is invisible
    private float lifetime;  //item will stop blinking after this time (< 0.0f is unlimited time)

    private float timeSinceLastBlink;
    private float timeCreated;

    private bool alive;

}
