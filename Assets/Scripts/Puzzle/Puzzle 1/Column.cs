using UnityEngine;
using System.Collections;

public class Column : MonoBehaviour 
{
    public SpriteRenderer barSprite;
    public string spriteName;
    void Start()
    {
        SetBar();
    }
    void SetBar()
    {
        barSprite.sprite = Resources.Load("Textures/Puzzle/01/"+spriteName) as Sprite;
    }
}
