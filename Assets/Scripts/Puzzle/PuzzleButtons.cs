using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(GameObject))]

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

    Vector3 epsilon;

    private UnityEngine.UI.Button ThisButton;

    void Start()
    {
        epsilon = new Vector3(0.001f, 0.001f, 0.001f);
        startPosition = transform.position;
        endPosition = startPosition;
        endPosition.y -= 290;
        
        ThisButton = gameObject.AddComponent<UnityEngine.UI.Button>();
        ThisButton.transition = Selectable.Transition.None;
        ThisButton.onClick.RemoveAllListeners();
        ThisButton.onClick.AddListener(() => OnAClick());
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

            if (((transform.position.y <= (destination.y + epsilon.y )) && !isDown) || 
                ((transform.position.y >= (destination.y - epsilon.y )) && isDown))
            {
                transform.position = destination;   
                moving = false;
                isDown = !isDown;
            }
        }
    }

	void OnAClick()
    {
        Debug.Log("[Puzzle UI] Button Clicked!");
        switch (thisType)
        {
            case ButtonType.Hint:
                break;
            case ButtonType.Quit:
                PuzzleManager.Instance.EndPuzzle();
                break;
            case ButtonType.Memo:
                break;
            case ButtonType.Restart:
                PuzzleManager.Instance.RestartPuzzle();
                break;
            case ButtonType.Submit:
                // TODO(jesse): Fix this
                PuzzleManager.Instance.SubmitPuzzle();
                break;
            case ButtonType.Info:
                if (!moving)
                {
                    moving = true;
                }
                break;
        }
    }

}
