using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundManager : MonoBehaviour 
{
    private Sprite CurrentBackground;
    private Dictionary<string, string> Backgrounds = new Dictionary<string, string>();
    private GameObject ThisBackground;
    private SpriteRenderer BgRenderer; 

	void Start () 
    {
        // TODO(jesse): Create this from file
        // Create Dictionary
        Backgrounds.Add( "test", "Textures/Backgrounds/backstreet_test" );
        Backgrounds.Add( "test2", "Textures/Backgrounds/backstreet_test2" );

        // Create GameObject
        ThisBackground = new GameObject("Background");
        BgRenderer = ThisBackground.AddComponent<SpriteRenderer>();

        // Set initial background
        ChangeBackground( "test" );
	}

    public void ChangeBackground(string BackgroundName)
    {
        // Load from \Resources\ folder
        BgRenderer.sprite = Resources.Load<Sprite>( Backgrounds[BackgroundName] );
    }
}
