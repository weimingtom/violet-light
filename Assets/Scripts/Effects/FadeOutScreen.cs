using UnityEngine;
using System.Collections;

public class FadeOutScreen : MonoBehaviour 
{

    public Texture2D fadeOutTex;
    public float defFadeSpeed = 0.8f;
    private float fadeSpeed;
    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = 1;
    static public FadeOutScreen instance;

    void Awake()
    {
        instance = this;
    }

	void OnGUI()
    {

        alpha += fadeDir * fadeSpeed * Time.deltaTime;

        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTex);
       
    }

    public float BeginFade(int newFadeDir)
    {
        fadeDir = newFadeDir;
        fadeSpeed = defFadeSpeed;
        return (fadeSpeed);
    }

    public float BeginFade(int newFadeDir, float newFadeSpeed)
    {
        fadeDir = newFadeDir;
        fadeSpeed = newFadeSpeed;
        return (fadeSpeed);
    }

    // NOTE(jesse): Returns if it has been fully faded to black 
    public bool GetFadedOut()
    {
        if (alpha == 0.0f)
        {
            return true;
        }
        return false;
    }

    public bool GetFadedIn()
    {
        if (alpha == 1.0f)
        {
            return true;
        }
        return false;
    }

    void OnLevelWasLoaded(int level)
    {
        BeginFade(-1);
    }
}
