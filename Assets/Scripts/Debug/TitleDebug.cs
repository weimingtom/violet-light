using UnityEngine;
using System.Collections;


public class TitleDebug : MonoBehaviour 
{
    float fadeTime = 0.0f;
    float timer = 0.0f;
    void Start()
    {
        GetComponent<FadeOutScreen>().BeginFade(-1);
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

    public void StartGame()
    {
        fadeTime = GetComponent<FadeOutScreen>().BeginFade(1);
	}
}
