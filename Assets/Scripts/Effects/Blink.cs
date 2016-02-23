using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Blink : MonoBehaviour {

    //will implement the unused variables if need!!!

    private float alivetime; //time between blinks, if deadtime is < 0.0f: deadtime is equal to alivetime
    //private float deadtime;  //used only if the duration it is visible is inequivelent to the duration the item is invisible
    //private float lifetime;  //item will stop blinking after this time (< 0.0f is unlimited time)

    private float lastBlink; //the time when the object reached full or no alpha.
    //private float timeCreated;

    private bool alive;
    private SpriteRenderer myRenderer;

    private bool run = false; //only run after initialize has been called. 


    public void Initialize( bool fade = true, float duration = 5.0f, float life = -1.0f, float dead = -1.0f)
    {
        alivetime = duration;
        //lifetime = life;
        //if( dead < 0.0f )
        //    deadtime = duration;
        //else
        //    deadtime = dead;
        lastBlink = Time.time;
        //timeCreated = Time.time;

        alive = true;
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        myRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        run = true;
    }

    public void pause()
    {
        run = false;
    }
    public void restart()
    {
        run = true;
        alive = true;
        myRenderer.color = new Color( 1.0f, 1.0f, 1.0f, 1.0f );
        lastBlink = Time.time;
    }

    public void UpdateThis()  //for some reason update didn't want to run so I am calling it in title manager's update.
    {
        if( run )
        {
            float newAlpha; 
            if(alive)
                newAlpha = (float)Mathf.Lerp( 0.0f, 1.0f, (float)((float)(Time.time - lastBlink) / alivetime) );
            else
                newAlpha = (float)Mathf.Lerp( 1.0f, 0.0f, (float)((float)(Time.time - lastBlink) / alivetime) );


            myRenderer.color = new Color( 1.0f, 1.0f, 1.0f, newAlpha );

            if( alive && newAlpha >= 1.0f || !alive && newAlpha <= 0.0f )
            {
                alive = !alive;
                lastBlink = Time.time;
            }
        }

    }


    

}
