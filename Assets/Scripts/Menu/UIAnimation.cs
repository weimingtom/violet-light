using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UIAnimation : MonoBehaviour 
{
    public static UIAnimation Instance;
    public GameObject[] uiElements;
    public GameObject[] uiElementButtons;
    private Vector3[] uiButtonOriginalPos;
    private Vector3 originalPosition;
    MenuManager.state myState = MenuManager.state.SaveLoad;
    MenuManager.state destinationState = MenuManager.state.SaveLoad;
    public float speed = 1.0f;
    public bool animate {get; set;}
    void Awake()
    {
        animate = false;
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
        //todo : implement back animation
        ResetPosition();
        myState = MenuManager.state.SaveLoad;
        destinationState = dest;
        animate = true;
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
    bool AnimateElement()
    {
        float step = speed * Time.deltaTime;
        Vector3 posBtn = uiButtonOriginalPos[(int)myState];
        posBtn.x += 800.0f;

        Vector3 pos = originalPosition;
        pos.x += 800.0f;

        uiElementButtons[(int)myState].transform.position = Vector3.MoveTowards( uiElementButtons[(int)myState].transform.position, posBtn, step);
        uiElements[(int)myState].transform.position = Vector3.MoveTowards( uiElements[(int)myState].transform.position, pos, step );
        if( uiElements[(int)myState].transform.position == pos )
        {
            return true;
        }
        return false;
    }
    void Update()
    {
        if( animate )
        {
            if (AnimateElement())
            {
                myState++;
            }
            if( myState == destinationState )
            {
                animate = false;
            }
        }
    }
}
