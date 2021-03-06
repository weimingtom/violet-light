﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{

    public static GameManager instance;

    public bool newGame {get; set;}
    public int gameToLoad {get; set;}

    private bool demoMode = false;

    void Awake()
    {
        if( GameManager.instance == null )
        {
            newGame = true;
            gameToLoad = -1; 
            instance = this;
            DontDestroyOnLoad( gameObject );
        }
        else
        {
            Destroy( this );
        }
    }

   
    public void StartGame(bool _newGame, int _gameToLoad = -1)
    {
        newGame = _newGame;
        gameToLoad = _gameToLoad;
        Application.LoadLevel( "MainScene" );
    }

    public void SetDemoMode(bool _enabled)
    {
        demoMode = _enabled;
    }

    public bool IsDemoMode()
    {
        return demoMode;
    }

}
