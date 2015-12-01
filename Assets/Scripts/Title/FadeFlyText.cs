using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeFlyText : MonoBehaviour {

    public float fadeDuration;
    public float delay;
    private Text text;
    private float delayTimer = 0.0f;
    void Start()
    {
        text = this.gameObject.GetComponent<Text>();
        text.canvasRenderer.SetAlpha( 0.0f );
    }
    void Update()
    {
        if(delayTimer > delay)
        {
            text.CrossFadeAlpha( 1.0f, fadeDuration, false );
        }
        else
        {
            delayTimer += Time.deltaTime;
        }
    }
}
