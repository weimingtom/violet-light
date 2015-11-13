using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Column : MonoBehaviour 
{
    public SpriteRenderer barTexture = new SpriteRenderer();
    public string spriteName;
    void Start()
    {
        barTexture = new SpriteRenderer();
        spriteName = "\0";
        SetBar();
    }
    void SetBar()
    {
        //Debug.Log( "Textures " + Resources.FindObjectsOfTypeAll( typeof( Texture ) ).Length );
        barTexture = Resources.Load( "Textures/Puzzle/01/" + spriteName ) as SpriteRenderer;
        
    }
}
