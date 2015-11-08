using UnityEngine;
using System.Collections;

public class PuzzleButtons : MonoBehaviour 
{
    public enum ButtonType
    {
        Hint,
        Quit,
        Memo,
        Restart,
        Submit,
        Info
    }

    public ButtonType thisType;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool moving = false;
    private bool isDown = false;
    private float easeDuration = 2.0f;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition;
        endPosition.y += 300;
    }

    void Update()
    {
        if(moving)
        {
            Vector3 destination = startPosition;
            if(!isDown)
            {
                destination = endPosition;
            }

            Vector3 newCords = new Vector3( transform.position.x, transform.position.y, 0.0f );
            newCords.x += (destination.x - transform.position.x) / easeDuration;
            newCords.y += (destination.y - transform.position.y) / easeDuration;

            transform.position = newCords;

            if(transform.position == destination )
            {
                moving = false;
                isDown = !isDown;
            }
        }
    }

	void OnMouseDown()
    {
        Debug.Log( "[Puzzle UI] Button Clicked!" );
        switch(thisType)
        {
            case ButtonType.Hint:
            break;
            case ButtonType.Quit:
            break;
            case ButtonType.Memo:
            break;
            case ButtonType.Restart:
            break;
            case ButtonType.Submit:
            break;
            case ButtonType.Info:
            if(!moving)
            { 
                moving = true;
            }
            break;
        }
    }
}
