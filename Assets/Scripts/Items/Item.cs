using UnityEngine;
using System.Collections;

public class Item:MonoBehaviour
{
    private string name;
    private GameObject item;
    private PolygonCollider2D collider;
    private Texture2D myTexture;

    bool collected;
    void Awake()
    {
        name = "";
    }
    void SetName(string _nm)
    {
        name = _nm;
    }
    void SetTexture(string _add)
    {
        myTexture = Resources.Load(_add) as Texture2D;
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

}
