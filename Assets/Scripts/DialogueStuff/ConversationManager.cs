using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;

public class ConversationManager : MonoBehaviour 
{
    static public ConversationManager Instance;
    public string fileLocation;

    private bool LoadGameScripts(string fileName)
    {
        try
        {
            string line;
            // create a new stream reader, tell it which file to read and what encoding the file was saved as
            StreamReader reader = new StreamReader(fileName, Encoding.Default);
            using(reader)
            {
                do
                {
                    line = reader.ReadLine();
                    if( line != null ) // line is not empty
                    {
                        //Store Character
                        
                    }
                }
                while( line != null );
            }
        }
        catch(Exception e)
        {
            Debug.Log(String.Format("{0}\n", e.Message));
            Console.WriteLine("{0}\n", e.Message);
            return false;
        }
        return true;
    }
    void Awake()
    {
        Instance = this;
    }
}


//Dictionary<uniqueID, String> dialogStringEnglisth = new Dictionary<String, String>();
//dialogString.Add( "Conversation1_1", "Snowy\n\nYow!...\now! ...ow!" );
//dialogString.Add( "Conversation1_2", "Tintin\n\n?" );
//dialogString.Add( "Conversation1_3", "Tintin\n\nThere you are snowy. You see what comes of your dirty habit of exploring rubbish bins…\n I don’t go scavenging do i ?" );
//dialogString.Add( "Conversation1_4", "<end>" );

//Dictionary<uniqueID, String> dialogStringChinese = new Dictionary<String, String>();
//dialogString.Add( "Conversation1_1", "雪域\ñ \ nYow ！ ... \吧！ ...嗚！" );
//dialogString.Add( "Conversation1_2", "丁丁\ñ \ñ ？" );
//dialogString.Add( "Conversation1_3", "Tintin\n\nThere you are snowy. You see what comes of your dirty habit of exploring rubbish bins…\n I don’t go scavenging do i ?" );


//Dictionary<uniqueID, String> dialogString = dialogStringEnglisth;
//if (Options.Settings.Language == "Chinese")
//{
//    dialogString = dialogStringChinese;
//}

//class DialogPlaybackSystem
//{
//    public Play(int convoId)
//    {
//        String key = "Conversation" + convoId;

//        int index = 1;
//        String nextStr = dialogString[key + index]
//        while (nextStr != "<end>")
//        {
//            showText(nextStr);

//            while for click
//                index++
//                nextStr = dialogString[key + index];
//        }
//    }
//}