using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInImage : MonoBehaviour 
{
    public float fadeDuration;
    private Image image;
    void Start()
    {
        image = this.gameObject.GetComponent<Image>();
        image.canvasRenderer.SetAlpha( 0.0f );
    }
	void Update () 
    {
        image.CrossFadeAlpha( 1.0f, fadeDuration, false );
	}
}
