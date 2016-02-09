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
    private string date;
    private string time;
    
    private int questStage;
    private int currentScene;
    private string currentCaseFile;
    
    private List<string> inventory;

    public Game()
    {
        date = System.DateTime.Now.ToString("MM/dd/yyyy");
        time = System.DateTime.Now.ToString( "hh:mm:ss" );

        questStage = SceneManager.Instance.GetQuestStage();
        currentScene = SceneManager.Instance.GetScene();
        currentCaseFile = SceneManager.Instance.GetCaseFile();

        //this will change to GetHeldItems( ref inventory ) in the next update
        ItemManager.Instance.GetHeldItem( ref inventory );
    }

}



public static class SaveLoad
{
    //can me used for multiple save files!
    public static List<Game> savedGames = new List<Game>();
    private const string VLSaveFile = "/saveGames.vls";

    public static void Save()
    {
        savedGames.Add( new Game() );
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create( Application.persistentDataPath + VLSaveFile );
        bf.Serialize( file, SaveLoad.savedGames );
        file.Close();
    }

    public static void Load()
    {
        if( File.Exists( Application.persistentDataPath + VLSaveFile ) )
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open( Application.persistentDataPath + VLSaveFile, FileMode.Open );
            SaveLoad.savedGames = (List<Game>)bf.Deserialize( file );
            file.Close();
        }
    }
	
}
