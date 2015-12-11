using UnityEngine;
using System.Collections;
[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(FadeOutScreen))]
public class TitleDebug : MonoBehaviour 
{
    float fadeTime;
    float timer = 0.0f;
    void Start()
    {
        GameObject.FindObjectOfType<FadeOutScreen>().BeginFade(-1);
    }

    void Update()
    {
        if(fadeTime>0.0f)
        {
            if((timer += Time.deltaTime) > fadeTime)
            {
                Application.LoadLevel("MainScene");

            }
        }

    }

    void OnMouseDown()
    {
        fadeTime = GameObject.FindObjectOfType<FadeOutScreen>().BeginFade(1);
	}
}
