using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartButton : MonoBehaviour {
    private UnityEngine.UI.Button ThisButton;
	// Use this for initialization
    public void EnableButton()
    {
        ThisButton = gameObject.AddComponent<UnityEngine.UI.Button>();
        ThisButton.transition = Selectable.Transition.None;
        ThisButton.onClick.RemoveAllListeners();
        ThisButton.onClick.AddListener( () => OnAClick() );
        
    
    }
    void OnAClick()
    {
        //TitleManager.instance.StartGame();
    }
}
