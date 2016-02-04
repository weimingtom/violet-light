using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net;


//Items to save
// - string CurrentCaseFile
// - int currentScene
// - int QuestStage 
// - list of inventory item names

[System.Serializable]
public class Game
{
    private string currentCaseFile;
    private int currentScene;
    private int questStage;
    private List<string> inventory;
    private string date;
    private string time;

    public Game()
    {
        date = System.DateTime.Now.ToString("MM/dd/yyyy");
        time = System.DateTime.Now.ToString( "hh:mm:ss" );

        questStage = SceneManager.Instance.GetQuestStage();
        currentScene = SceneManager.Instance.GetScene();
        currentCaseFile = SceneManager.Instance.GetCaseFile();

        inventory = null;
        

    }

}



public static class SaveLoad
{
    //can me used for multiple save files!
    public static List<Game> savedGames = new List<Game>();

    public static void Save()
    {
        savedGames.Add( new Game() );
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create( Application.persistentDataPath + "/savedGames.gd" );
        bf.Serialize( file, SaveLoad.savedGames );
        file.Close();
    }
	
}
