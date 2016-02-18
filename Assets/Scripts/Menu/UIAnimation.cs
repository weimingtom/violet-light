﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class UIAnimation : MonoBehaviour 
{
    public static UIAnimation Instance;
    public GameObject[] uiElements;
    public GameObject[] uiElementButtons;

    public GameObject uiDestination;

    private Vector3[] uiButtonOriginalPos;
    private Vector3 originalPosition;
    MenuManager.state myState = MenuManager.state.SaveLoad;
    MenuManager.state destinationState = MenuManager.state.SaveLoad;
    public float speed = 1.0f;
    public bool animateForward { get; set; }
    public bool animateBackward { get; set; }
    void Awake()
    {
        animateForward = false;
        Instance = this;

        originalPosition = uiElements[0].transform.position;
        uiButtonOriginalPos = new Vector3[uiElementButtons.Length];
        for( int i = 0; i < uiElementButtons.Length; i++ )
        {
            uiButtonOriginalPos[i] = uiElementButtons[i].transform.position;
        }
    }
    public void StartAnimate(MenuManager.state dest)
    {
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
            go.transform.position = uiButtonOriginalPos[index];
            index++;
        }
    }
    bool AnimateElementBackward()
    {
        float step = speed * Time.deltaTime * Screen.width / 100;

        if( uiElements[(int)myState].transform.position != originalPosition )
        {
            uiElements[(int)myState].transform.position = Vector3.MoveTowards( uiElements[(int)myState].transform.position, originalPosition, step );
        }
        if( uiElementButtons[(int)myState].transform.position != uiButtonOriginalPos[(int)myState] )
        {
            uiElementButtons[(int)myState].transform.position = Vector3.MoveTowards( uiElementButtons[(int)myState].transform.position, uiButtonOriginalPos[(int)myState], step );
        }
        if( Mathf.Abs(uiElements[(int)myState].transform.position.magnitude - originalPosition.magnitude) < 0.1f 
            && Mathf.Abs( uiElementButtons[(int)myState].transform.position.magnitude - uiButtonOriginalPos[(int)myState].magnitude ) < 0.1f )
        {
            return true;
        }
        return false;
    }
    //Screen.width / 100
    bool AnimateElementFroward()
    {
        float step = speed * Time.deltaTime * Screen.width / 100;
        Vector3 posBtn = uiButtonOriginalPos[(int)myState];
        posBtn.x += uiDestination.transform.position.x;

        Vector3 pos = originalPosition;
        pos.x += uiDestination.transform.position.x;
        if( uiElements[(int)myState].transform.position != pos )
        {
            uiElements[(int)myState].transform.position = Vector3.MoveTowards( uiElements[(int)myState].transform.position, pos, step );
        }
        if( uiElementButtons[(int)myState].transform.position != posBtn )
        {
            uiElementButtons[(int)myState].transform.position = Vector3.MoveTowards( uiElementButtons[(int)myState].transform.position, posBtn, step);
        }
        if( Mathf.Abs(uiElements[(int)myState].transform.position.magnitude - pos.magnitude) < 0.1f
            && Mathf.Abs(uiElementButtons[(int)myState].transform.position.magnitude - posBtn.magnitude) < 0.1f )
        {
            return true;
        }
        return false;
    }
    void FixedUpdate()
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
