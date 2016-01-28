using UnityEngine;
using System.Collections;

public class OldDemo : MonoBehaviour 
{
    float timer;
    bool firstRun;
    bool secondRun;

    bool[] firstTimeInArea;

	// Use this for initialization
	void Start () 
    {
        SceneManager.Instance.LoadCase(1);
        SceneManager.Instance.ChangeScene(4);
        MusicManager.instance.ChangeSong("violetstheme");
        timer = Time.time + 1;
        firstRun = true;
        secondRun = true;

        firstTimeInArea = new bool[7];

        firstTimeInArea[0] = true;
        firstTimeInArea[1] = true;
        firstTimeInArea[2] = true;
        firstTimeInArea[3] = true;
        firstTimeInArea[4] = true;
        firstTimeInArea[5] = true;
        firstTimeInArea[6] = true;

        foreach(bool time in firstTimeInArea)
        {
            Debug.Log("Bool set to " + time);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
       

        switch(SceneManager.Instance.GetScene())
        {
            case 0:
                if(firstTimeInArea[0])
                {
                    firstTimeInArea[0] = false;
                }
                break;
            case 1:
                if (firstTimeInArea[1])
                {
                    firstTimeInArea[1] = false;
                }
                break;
            case 2:
                if (firstTimeInArea[2])
                {
                    firstTimeInArea[2] = false;
                }
                break;
            case 3:
                if (firstTimeInArea[3])
                {
                    FileReader.Instance.LoadScene("demo_crimescene_intro");
                    firstTimeInArea[3] = false;
                }
                break;
            case 4:
                if (firstTimeInArea[4])
                {
                    firstTimeInArea[4] = false;
                }
                if (timer < Time.time && secondRun)
                {
                    if (firstRun)
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
                break;
            case 5:
                if (firstTimeInArea[5])
                {
                    FileReader.Instance.LoadScene("demo_music_intro");
                    firstTimeInArea[5] = false;
                }
                break;
            case 6:
                if (firstTimeInArea[6])
                {
                    FileReader.Instance.LoadScene("demo_warf_intro");
                    firstTimeInArea[6] = false;
                }
                break;

        }
        
	}
}
