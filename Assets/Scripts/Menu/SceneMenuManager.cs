using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SceneMenuManager : MonoBehaviour
{
    static public SceneMenuManager instance;
    
    public Button talkButton;
    public Button presentButton;
    public Button moveButton;
    public Button examineButton;
    public Button backButton;
    
    bool showTalkButton;
    bool showPresentButton;
    bool showExamineButton;

    string characterOnScene = "";
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        showTalkButton = false;
        showPresentButton = false;
        showExamineButton = false;
        talkButton.onClick.AddListener( () => TalkToCharacterInScene() );
        backButton.onClick.AddListener( () => ResetButton() );
        examineButton.onClick.AddListener( () => ExamineScene() );
        talkButton.transform.gameObject.SetActive( false );
        presentButton.transform.gameObject.SetActive( false );
        moveButton.transform.gameObject.SetActive( false );
        examineButton.transform.gameObject.SetActive( false );
        backButton.transform.gameObject.SetActive( false );
    }
	// Use this for initialization
	public void EnteredNewScene()
    {
        characterOnScene = SceneManager.Instance.GetChar();
        if( characterOnScene != "" )
        {
            ShowExamineButton();
            showTalkButton = true;
            showPresentButton = true;
        }
        ShowExamineButton();
        talkButton.transform.gameObject.SetActive( showTalkButton );
        presentButton.transform.gameObject.SetActive( showPresentButton );
        moveButton.transform.gameObject.SetActive( true );

        // Check if there is a character in the scene //
            // if yes then show button for PRESENT and for TALK
                // IF THE TALK BUTTON IS PRESSED RUN:
                    //TalkToCharacterInScene()
            
                // if Present button is pressed
                    //MenuManager.instance.OpenItemMenu
            // if no then don't show those buttons

        // Check if there are things to click on in the background
            // if yes then show INVESTIGATE button
                //if investigate button is clicked HIDE THE UI FOR THIS MENU AND SHOW A BACK BUTTON
                    // IF BACK BUTTON is pressed show this screen again.
        // The player should not be able to click on anything in
        
        // Show move button no matter what
    }

    public void ResetButton()
    {
        talkButton.transform.gameObject.SetActive( showTalkButton );
        presentButton.transform.gameObject.SetActive( showPresentButton );
        examineButton.transform.gameObject.SetActive( showExamineButton );
        moveButton.transform.gameObject.SetActive( true );
        backButton.transform.gameObject.SetActive( false );
    }

    public void hideAll()
    {
        talkButton.transform.gameObject.SetActive( false );
        presentButton.transform.gameObject.SetActive( false );
        examineButton.transform.gameObject.SetActive( false );
        moveButton.transform.gameObject.SetActive( true );
        backButton.transform.gameObject.SetActive( false );
    }

    void ShowExamineButton()
    {
        int childNumber = InteractableManager.Instance.GetNumberOfInteractable();
        //check if there is something to examine
        if( childNumber > 0 )
        {
            if( !InteractableManager.Instance.IsOnlyCharacterInScene() )
            {
                showExamineButton = true;
                examineButton.transform.gameObject.SetActive( showExamineButton );
            }
        }
    }

    void ExamineScene()
    {
        talkButton.transform.gameObject.SetActive( false );
        presentButton.transform.gameObject.SetActive( false );
        examineButton.transform.gameObject.SetActive( false );
        moveButton.transform.gameObject.SetActive( false );
        backButton.transform.gameObject.SetActive( true );
    }

    void TalkToCharacterInScene()
    {
        Prop Character = GameObject.Find(SceneManager.Instance.GetChar()).GetComponent<Prop>();
        Character.Talk();
    }

}
