using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class CommandManager : MonoBehaviour 
{

    int destroyCount;
    bool done;

    public string correctItem { get; set; }
    public int presentItemIndex { get; set; }

    public bool prompt { get; set; }
    public int testimonyItemIndex { get; set;}
    public Text myTextHolder;
    public Text myNameHolder;

    public GameObject myBannerBox;
    public GameObject leftButton;
    public GameObject rightButton;

    static public CommandManager Instance;

    int commandTracker;
    List<Commands> myCommand;

    void UpdateButton()
    {
        leftButton.SetActive(false);
        rightButton.SetActive(false);
    }

    public void Terminate()
    {
        commandTracker = myCommand.Count;
    }

	public void AddCharIntoTextHolder(char c)
	{
		myTextHolder.text += c;
	}

    public void AddStringIntoTextHolder( string str )
    {
        myTextHolder.text += str;
    }

    public void SetNameIntoNameBox( string name )
    {
        myNameHolder.text = name;
    }

    public void SetTextHolder(string content)
    {
		myTextHolder.text = content;
	}

	public void TextSwitch(bool status)
	{
        myTextHolder.gameObject.SetActive( status );
        myNameHolder.gameObject.SetActive( status );
	}

	public void TextBoxSwitch(bool status)
	{
		myBannerBox.gameObject.SetActive (status);
	}

    void Start()
    {
        myTextHolder.supportRichText = true;
        destroyCount = 0;
        done = false;
        commandTracker = 0;
        Instance = this;
        myCommand = new List<Commands>();
    }

    public void AddCommand(Commands command)
    {
        myCommand.Add(command);
    }

    public void CheckItem(string itemName)
    {
        //therefore it is presenting on the scene
        if( itemName != correctItem )
        {
            //present in scene
            if( presentItemIndex == -1 && prompt == false )
            {
                SceneManager.Instance.AdvQuest();
            }//present in testimony
            else if( presentItemIndex != -1 && prompt == false )
            {
                if( testimonyItemIndex == presentItemIndex )
                {
                    //advance when correct item is presented
                    commandTracker++;
                    SceneManager.Instance.AdvQuest();
                }
                else
                {
                    //fail
                }
            }
            else
            {
                //do something for prompted
                prompt = false;
            }

        }
        else
        {
            //do something if fail
        }

    }




    public void Reinitialize()
    {
        correctItem = "none";
        presentItemIndex = -1;
        testimonyItemIndex = -1;
        destroyCount = 0;
        done = false;
        commandTracker = 0;
        myCommand.Clear();
    }

    void Update()
    {
        UpdateButton();
        switch( done )
        {
        case false:

		    if(commandTracker < myCommand.Count)
		    {
			    if ( myCommand[commandTracker].ExecuteCommand() ) 
			    {
				    commandTracker++;
                    Debug.Log("Run command no  : " + commandTracker);
			    }
		    }
		    else if (commandTracker == myCommand.Count)
		    {
                if( destroyCount < myCommand.Count
                    && myCommand[destroyCount].Destroy())
                {
                    destroyCount++;
                }
                else if(destroyCount == myCommand.Count)
                {
                    SceneManager.Instance.SetInputBlocker( false );
                    done = true;
                }
		    } 
            break;

        case true:
            break;
        }
    }

}
