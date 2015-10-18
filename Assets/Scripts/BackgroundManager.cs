using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundManager : MonoBehaviour
{
    static public BackgroundManager Instance;
    public Dictionary<string, string> backgroundLookup = new Dictionary<string, string>();
    private GameObject currBackground;
    private SpriteRenderer currBackgroundRend;

    //0.0 - 1.0. How quickly the picture fades in. 
    public float deltaAlpha = 0.03f;
    //thew new background that is fading in
    private GameObject newBackground;
    private SpriteRenderer newBackgroundRend;

    void Awake()
    {
        Instance = this;
    }

    void LoadBackgrounds( string filepath )
    {
        // TODO(jesse): Create this from file
        // Create Dictionary
        backgroundLookup.Add( "test1", "Textures/Backgrounds/backstreet_test" );
        backgroundLookup.Add( "test2", "Textures/Backgrounds/backstreet_test2" );
    }

    void Start()
    {
        LoadBackgrounds( "void" );

        // Create GameObject
        currBackground = new GameObject( "Background" );
        currBackgroundRend = currBackground.AddComponent<SpriteRenderer>();

        newBackground = new GameObject( "NewBackground" );
        newBackground.transform.position = new Vector3( 0f, 0f, -0.01f );
        newBackgroundRend = newBackground.AddComponent<SpriteRenderer>();
        newBackgroundRend.sprite = null;

        // Set initial background
        currBackgroundRend.sprite = Resources.Load<Sprite>( backgroundLookup["test1"] );
    }

    void Update()
    {
        if( newBackgroundRend.sprite != null )
            DoFade();
    }

    public void ChangeBackground( string backgroundName, float Speed = 0.5f)
    {
        // Load from \Resources\ folder
        newBackgroundRend.sprite = Resources.Load<Sprite>( backgroundLookup[backgroundName] );
        newBackgroundRend.color = new Color( 1f, 1f, 1f, 0f );
        deltaAlpha = Speed;
    }

    void DoFade()
    {
        float alpha = newBackgroundRend.color.a;

        if( alpha < 1.0 ) //Still doing Fade
        {
            newBackgroundRend.color = new Color( 1f, 1f, 1f, alpha + (deltaAlpha * Time.deltaTime) );
        }
        else //Fade done: Set old to new and new to null.
        {
            currBackgroundRend.sprite = newBackgroundRend.sprite;
            newBackgroundRend.sprite = null;
        }

    }
}
