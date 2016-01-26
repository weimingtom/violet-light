using UnityEngine;
using System.Collections;

public class OldDemo : MonoBehaviour 
{
    float timer;
    bool firstRun;
    bool secondRun;
	// Use this for initialization
	void Start () 
    {
        SceneManager.Instance.LoadCase(1);
        SceneManager.Instance.ChangeScene(4);
        MusicManager.instance.ChangeSong("violetstheme");
        timer = Time.time + 1;
        firstRun = true;
        secondRun = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (timer < Time.time && secondRun)
        {
            
            if(firstRun)
            {
                firstRun = false;
                FileReader.Instance.LoadScene("demo_intro");
                timer = Time.time + 1;
            }
            else if (!CommandManager.Instance.myBannerBox.activeInHierarchy)
            {
                secondRun = false;
                SceneManager.Instance.ChangeScene(3);
                MusicManager.instance.ChangeSong("theme");
            }

        }
	}
}
