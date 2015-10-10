using UnityEngine;
using System.Collections;
using System.IO;
public class ConversationManager : MonoBehaviour 
{
    public string fileLocation;
    
	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () {
	
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