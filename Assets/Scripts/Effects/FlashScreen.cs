using UnityEngine;
using System.Collections;

public class FlashScreen : MonoBehaviour 
{

    public Texture2D flashTex;
    public float flashSpeed = 20.0f;
    private int drawDepth = -1000;
    private float alpha = 0.0f;
    private int fadeDir = 1;

    void OnGUI()
    {
        alpha += fadeDir * flashSpeed * Time.deltaTime;
        if (alpha > 1.0f) 
        {
            BeginFade( -1 );
        }
        else if (alpha <0.0f)
        {
            Destroy( this.gameObject );
        }
        //alpha = Mathf.Clamp01( alpha );
        GUI.color = new Color( GUI.color.r, GUI.color.g, GUI.color.b, alpha );
        GUI.depth = drawDepth;
        GUI.DrawTexture( new Rect( 0, 0, Screen.width, Screen.height ), flashTex );
    }

    public float BeginFade( int newFadeDir  = 1 )
    {
        fadeDir = newFadeDir;
        return (flashSpeed);
    }

}
