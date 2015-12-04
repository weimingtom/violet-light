using UnityEngine;
using System.Collections;

public class Item
{
    private string name;
    private Texture2D myTexture;
    private Vector2 position;
    bool collected;
    void Awake()
    {
        name = "\0";
    }
    void SetName(string _nm)
    {
        name = _nm;
    }
    void SetTexture(string _add)
    {
        myTexture = Resources.Load(_add) as Texture2D;
    }
    void SetPosition(Vector2 pos )
    {
        position = pos;
    }
    public void InitializeItem(string _name, string _address, bool _flag)
    {
        SetName( _name );
        SetTexture( _address);
        SetCollected( _flag );
    }
    public void SetCollected(bool _flag)
    {
        collected = _flag;
    }
    public bool GetCollected()
    {
        return collected;
    }
    Vector2 GetPosition()
    {
        return position;
    }
}
