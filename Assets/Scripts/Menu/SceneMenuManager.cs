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

    // called by command manager
    bool talkCurrentState = false;
    bool presentCurrentState = false;
    bool examineCurrentState = false;
    bool moveCurrentState = false;
    bool backCurrentState = false;

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
        presentButton.onClick.AddListener(() => Present());
        moveButton.onClick.AddListener( () => Move());

        talkButton.transform.gameObject.SetActive( false );
        presentButton.transform.gameObject.SetActive( false );
        moveButton.transform.gameObject.SetActive( false );
        examineButton.transform.gameObject.SetActive( false );
        backButton.transform.gameObject.SetActive( false );
    }

    void Present()
    {
        MenuManager.instance.OpenEvidenceTab();
    }
    void Move()
    {
        MenuManager.instance.OpenTravelTab();
    }
	// Use this for initialization
    public void EnteredNewScene()
    {
        SceneManager.Instance.SetInputBlocker( true );
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
    }

    public void ActivateBackButton()
    {
        backButton.transform.gameObject.SetActive(true);
    }

    public void ResetButton()
    {
        SceneManager.Instance.SetInputBlocker( true );
        talkButton.transform.gameObject.SetActive( showTalkButton );
        presentButton.transform.gameObject.SetActive( showPresentButton );
        examineButton.transform.gameObject.SetActive( showExamineButton );
        moveButton.transform.gameObject.SetActive( true );
        backButton.transform.gameObject.SetActive( false );
        CommandManager.Instance.isExamine = false;
    }

    public void hideAll()
    {
        talkButton.transform.gameObject.SetActive( false );
        presentButton.transform.gameObject.SetActive( false );
        examineButton.transform.gameObject.SetActive( false );
        moveButton.transform.gameObject.SetActive( false );
        backButton.transform.gameObject.SetActive( false );
    }

    void ShowExamineButton()
    {
        int childNumber = InteractableManager.Instance.GetNumberOfInteractable();
        if( childNumber > 0 )
        {
            if( !InteractableManager.Instance.IsOnlyCharacterInScene() )
            {
                showExamineButton = true;
                examineButton.transform.gameObject.SetActive( showExamineButton );
            }
        }
    }
    public void ExamineScene()
    {
        SceneManager.Instance.SetInputBlocker( false );
        talkButton.transform.gameObject.SetActive( false );
        presentButton.transform.gameObject.SetActive( false );
        examineButton.transform.gameObject.SetActive( false );
        moveButton.transform.gameObject.SetActive( false );
        backButton.transform.gameObject.SetActive( true );
        CommandManager.Instance.isExamine = true;
    }

    public void HideFromCommandManager()
    {
        talkCurrentState = talkButton.transform.gameObject.activeInHierarchy;
        presentCurrentState = presentButton.transform.gameObject.activeInHierarchy;
        examineCurrentState = examineButton.transform.gameObject.activeInHierarchy;
        moveCurrentState = moveButton.transform.gameObject.activeInHierarchy;
        backCurrentState = backButton.transform.gameObject.activeInHierarchy;
        talkButton.transform.gameObject.SetActive( false );
        presentButton.transform.gameObject.SetActive( false );
        examineButton.transform.gameObject.SetActive( false );
        moveButton.transform.gameObject.SetActive( false );
        backButton.transform.gameObject.SetActive( false );
    }

    public void ActivateFromCommandManager()
    {
        talkButton.transform.gameObject.SetActive( talkCurrentState );
        presentButton.transform.gameObject.SetActive( presentCurrentState );
        examineButton.transform.gameObject.SetActive( examineCurrentState );
        moveButton.transform.gameObject.SetActive( moveCurrentState );
        backButton.transform.gameObject.SetActive( backCurrentState );
    }

    void TalkToCharacterInScene()
    {
        Prop Character = GameObject.Find(SceneManager.Instance.GetChar()).GetComponent<Prop>();
        Character.Talk();
    }
    public void BackButtonOnEnter()
    {
        Debug.Log("button on enter");
        SceneManager.Instance.SetInputBlocker( true );
        //Debug.Break();
    }
    public void BackButtonOnExit()
    {
        Debug.Log("button on exit");
        SceneManager.Instance.SetInputBlocker( false );
    }
    void Update()
    {
        if( SceneManager.Instance.GetChar() == "" )
        {
            presentButton.transform.gameObject.SetActive(false);
            talkButton.transform.gameObject.SetActive(false);
        }
    }
}
