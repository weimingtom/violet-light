using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SpriteRenderer))]
public class FadeSprite : MonoBehaviour 
{
    private SpriteRenderer spriteRenderer;

    public float fadeSpeed = 1.0f;
    private bool fading = false;
    private float fadeDir = -1.0f;


    private float alpha = 1.0f;
	// Use this for initialization
	void Awake () 
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(fading)
        {
            
            alpha += (fadeDir * Time.deltaTime * fadeSpeed );
            if(  alpha < 0.0f || alpha > 1.0f)
            {
             
                fading = false;
            }
            alpha = Mathf.Clamp( alpha, -1.0f, 1.0f );
            spriteRenderer.color = new Color( spriteRenderer.color.r, spriteRenderer.color.g,
                                          spriteRenderer.color.b, alpha );
        }
	}

    public void StartFade(float newFadeDir)
    {
        fadeDir = Mathf.Clamp(newFadeDir,-1.0f,1.0f);
        fading = true;
    }

    public void SetVisible(bool isVis)
    {
        alpha = 0.0f;
        if (isVis)
        {
            alpha = 1.0f;
        }
        spriteRenderer.color = new Color( spriteRenderer.color.r, spriteRenderer.color.g,
                                          spriteRenderer.color.b, alpha );
    }

    public bool IsFading()
    {
        return fading;
    }
}
