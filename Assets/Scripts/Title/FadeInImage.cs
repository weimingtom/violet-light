using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInImage : MonoBehaviour 
{
    public float fadeDuration;
    private Image image;
    private float delayTimer = 0.0f;
    public float delay;

    void Start()
    {
        image = this.gameObject.GetComponent<Image>();
        image.canvasRenderer.SetAlpha( 0.0f );
    }
	void Update () 
    {
        if (delayTimer > delay)
        {
            image.CrossFadeAlpha(1.0f, fadeDuration, false);
        }
        else
        {
            delayTimer += Time.deltaTime;
        }
	}
}
