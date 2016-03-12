using UnityEngine;
using System;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class Prop : MonoBehaviour {

    public bool IsPickUp;
    public bool IsCharacter;
    public int minStageRange = 0;
    public int maxStageRange = 0;
    private int range;

    void Awake()
    {
        if(IsCharacter)
        {
            SceneManager.Instance.SetChar( name );
            ItemInventory.Instance.TogglePresentButton( true );
        }

        if(minStageRange != 0 || maxStageRange != 0)
        {
            for(int i = minStageRange; i < maxStageRange; ++i)
            {
                if( IsPickUp && SceneManager.Instance.GetScenePlayed( ( i) + "_" + SceneManager.Instance.GetSceneName() + "_" + name ) )
                {
                    this.gameObject.SetActive( false );
                }
            }
        }
        else if( IsPickUp && SceneManager.Instance.GetScenePlayed( SceneManager.Instance.GetQuestStage() + "_" + SceneManager.Instance.GetSceneName() + "_" + name ) )
        {
            this.gameObject.SetActive( false );
        }
    }


    void OnMouseDown()
    {
        if(!FileReader.Instance.IsScene( SceneManager.Instance.GetQuestStage() + "_" + SceneManager.Instance.GetSceneName() + "_" + name ))
        {
            FileReader.Instance.LoadScene( SceneManager.Instance.GetQuestStage() + "_" + SceneManager.Instance.GetSceneName() + "_obj" );
        }
        else
            FileReader.Instance.LoadScene( SceneManager.Instance.GetQuestStage() + "_" + SceneManager.Instance.GetSceneName() + "_" + name );

        if( IsPickUp )
        {
            Debug.Log( "[Prop] Picked the item up!" );
            this.gameObject.SetActive(false);
        }
    }

    void OnMouseEnter()
    {
        SceneManager.Instance.SetCursor( name );
        //SceneManager.Instance.SetCursor( !SceneManager.Instance.GetScenePlayed( SceneManager.Instance.GetQuestStage() + "_" + SceneManager.Instance.GetSceneName() + "_" + name ) );
        //Debug.Log( "[Prop] Cursor Entered, changing cursor to not Sparkle? " + SceneManager.Instance.GetScenePlayed( SceneManager.Instance.GetQuestStage() + "_" + SceneManager.Instance.GetSceneName() + "_" + name ).ToString() );
    }
    void OnMouseExit()
    {
        SceneManager.Instance.SetCursor( false );
    }
}
