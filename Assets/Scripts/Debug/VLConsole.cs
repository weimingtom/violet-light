using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VLConsole : MonoBehaviour 
{

    private Image consoleTextBox;
    private InputField commandInput;
    private bool consoleEnabled = false;

	void Awake() 
    {
        consoleTextBox = GetComponentInChildren<Image>();
        commandInput = GetComponentInChildren<InputField>();
        InputField.SubmitEvent submitEvent = new InputField.SubmitEvent();
        submitEvent.AddListener(SubmitCommand);
        commandInput.onEndEdit = submitEvent;
        ToggleConsole(false);
	}
	
	void Update () 
    {
	    if(Input.GetKeyDown(KeyCode.BackQuote))
        {
            ToggleConsole(!consoleEnabled);
        }
	}

    private void ToggleConsole(bool on)
    {
        commandInput.gameObject.SetActive(on);
        consoleEnabled = on;
        if(on)
        {
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(commandInput.gameObject, null);
            //commandInput.OnPointerClick(null);
        }
    }

    private void SubmitCommand(string command)
    {
        Debug.Log("[VLConsole] Attempting command " + command);

        char[] delimiterChar = { '\r', '\n', ' ' };
        string[] commandSeg = command.Split(delimiterChar, System.StringSplitOptions.RemoveEmptyEntries);

        if(commandSeg.Length > 1)
            Debug.Log("[VLConsole] Command Split into '" + commandSeg[0] + "' + '" + commandSeg[1] + "' ");

        switch(commandSeg[0].ToLower())
        {
            case ("cs"):
            {
                SceneManager.Instance.ChangeScene(commandSeg[1]);
            }
            break;
            case ("ld"):
            {
                FileReader.Instance.LoadScene(commandSeg[1]);
            }
            break;
            case ("break"):
            {
                CommandManager.Instance.Terminate();
            }
            break;
            case ("advq"):
            {
                SceneManager.Instance.AdvQuest();   
            }
            break;
            case ("save"):
            {
                SaveLoad.Save();
            } 
            break;
        }

        commandInput.text = "";
    }
}
