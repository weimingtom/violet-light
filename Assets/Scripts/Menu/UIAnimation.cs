using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class UIAnimation : MonoBehaviour 
{
    public static UIAnimation Instance;
    public GameObject[] uiElements;
    public GameObject[] uiElementButtons;

    public GameObject uiDestination;

    public Transform[] uiButtonOriginalPos;

    public Transform[] uiButtonDestination;
    private Vector3 originalPosition;
    MenuManager.state myState = MenuManager.state.SaveLoad;
    MenuManager.state destinationState = MenuManager.state.SaveLoad;
    public float speed = 1.0f;
    public bool animateForward { get; set; }
    public bool animateBackward { get; set; }
    private bool animateBtn;
    
    //opening animation
    Vector3[] menuOpenPosition;
    public float openCloseMenuOffset = 500.0f;





    Resolution res;


    // Update is called once per frame
    void Update()
    {

        if( res.width != Screen.currentResolution.width )
        {

            SetPositions();
            res = Screen.currentResolution;

        }

    }


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        animateBtn = false;
        animateForward = false;
        Instance = this;

        originalPosition = uiElements[0].transform.position;
        
        menuOpenPosition = new Vector3[uiElements.Length];
        res = Screen.currentResolution;

        SetPositions();
    }

    public void SetPositions()
    {
        for( int i = 0; i < menuOpenPosition.Length; i++ )
        {
            menuOpenPosition[i] = new Vector3( originalPosition.x, originalPosition.y + openCloseMenuOffset + (openCloseMenuOffset * i), originalPosition.z );
            uiElements[i].transform.position = menuOpenPosition[i];
        }

        int j = 0;
        for( int i = uiElements.Length - 1; i >= 0; i-- )
        {
            uiElements[i].transform.position = menuOpenPosition[j];
            j++;
        }
    }

    bool AnimateDown()
    {
        float step = speed * Time.deltaTime * Screen.height / 100;
        int doneCount = 0;
        foreach(GameObject gO in uiElements)
        {
            gO.transform.position = Vector3.MoveTowards(gO.transform.position, originalPosition, step);
            if( Mathf.Abs( gO.transform.position.magnitude - originalPosition.magnitude ) < 0.1f )
            {
                doneCount++;
            }
        }
        if( doneCount == uiElements.Length )
        {
            
            return true;
        }
        else
        {
            return false;
        }
    }
    bool AnimateUp()
    {
        float step = speed * Time.deltaTime * Screen.height / 100;
        int doneCount = 0;
        float speedModifier = 0.1f;
        Vector3 destination;
        for( int i = 0; i < uiElements.Length; i++ )
        {
            destination = menuOpenPosition[i];
            uiElements[i].transform.position = Vector3.MoveTowards( uiElements[i].transform.position, destination, step / (float)(i * speedModifier) );
            if( Mathf.Abs( uiElements[i].transform.position.magnitude - destination.magnitude ) < 0.1f )
            {
                doneCount++;
            }
        }
        if( doneCount == uiElements.Length )
        {
            int j = 0;
            for( int i = uiElements.Length - 1; i >= 0; i-- )
            {
                uiElements[i].transform.position = menuOpenPosition[j];
                j++;
            } 
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool OpeningMenuAnimation()
    {
        bool done = AnimateDown();
        return done;
    }
    public bool ClosingMenuAnimation()
    {
        bool menuReadyToClose = !AnimateUp();
        return menuReadyToClose;
    }

    public void ResetButtonPosition()
    {
        for( int i = 0; i < uiElementButtons.Length; i++ )
        {
            uiElementButtons[i].transform.position = uiButtonOriginalPos[i].position;
        }
    }

    public void StartAnimate(MenuManager.state dest)
    {
        animateBtn = true;
        destinationState = dest;
        if( dest > myState )
        {
            animateForward = true;
        }
        else if( dest < myState)
        {
            animateBackward = true;
        }
    }

    public void ResetPosition()
    {
        foreach( GameObject go in uiElements )
        {
            go.transform.position = originalPosition;
        }
        int index = 0;
        foreach( GameObject go in uiElementButtons )
        {
            go.transform.position = uiButtonOriginalPos[index].position;
            index++;
        }
    }

    bool AnimateElementBackward()
    {
        float step = speed * Time.deltaTime * Screen.width / 100;
        if( animateBtn == true )
        {
            uiElementButtons[(int)myState].transform.position = uiButtonOriginalPos[(int)myState].position;
            animateBtn = false;
        }
        if( Mathf.Abs(uiElements[(int)myState].transform.position.magnitude - originalPosition.magnitude) < 0.1f )
        {
			animateBtn = true;
            return true;
        }
		else
		{
            uiElements[(int)myState].transform.position = Vector3.MoveTowards( uiElements[(int)myState].transform.position, originalPosition, step );
			
		}
        return false;
    }

    bool AnimateElementFroward()
    {
        float step = speed * Time.deltaTime * Screen.width / 100;

        Vector3 pos = originalPosition;
        pos.x += uiDestination.transform.position.x;
        if( animateBtn == true )
        {
            uiElementButtons[(int)myState].transform.position = uiButtonDestination[(int)myState].position;
            animateBtn = false;
        }
        if( Mathf.Abs(uiElements[(int)myState].transform.position.magnitude - pos.magnitude) < 0.1f )
        {
			animateBtn = true;
            return true;
        }
		else
		{
            uiElements[(int)myState].transform.position = Vector3.MoveTowards( uiElements[(int)myState].transform.position, pos, step );
		}
        return false;
    }

    void FixedUpdate()
    {
        if( MenuManager.instance.IsDoneAnimating() )
        {
            if( animateForward )
            {
                if( AnimateElementFroward() )
                {
                    myState++;
                }
                if( myState == destinationState )
                {
                    animateForward = false;
                }
            }
            if( animateBackward )
            {
                if( AnimateElementBackward() )
                {
                    myState--;
                }
                if( myState < destinationState )
                {
                    Debug.Log("my state is :" + myState.ToString() );
                    //Debug.Break();
                    animateBackward = false;
                    myState = destinationState;
                }
            }
        }
    }
}

