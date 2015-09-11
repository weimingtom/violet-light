﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DialogueScripts : MonoBehaviour 
{
    private int index;
    private string[] dialogueString;

    public Text DialogueText;
	void Start () 
    {
        int tempIdx = 0;
		dialogueString = new string[100];
        //page 1
        dialogueString[tempIdx] = "Snowy\n\nYow!...\now! ...ow!";
        tempIdx++;
        dialogueString[tempIdx] = "Tintin\n\n?";
        tempIdx++;
        dialogueString[tempIdx] = "Tintin\n\nThere you are snowy. You see what comes of your dirty habit of exploring rubbish bins…\n I don’t go scavenging do i ?";
        tempIdx++;
        //page 2
        dialogueString[tempIdx] = "Tintin\n\nYou’ve been lucky!\n you could have cut yourself. Look how jagged the edges are";
        tempIdx++;
        dialogueString[tempIdx] = "Tintin\n\nNow, come on!... \nAnd don’t do that again, \nor i’ll buy a muzzle and you’ll walk on a lead!";
        tempIdx++;
        dialogueString[tempIdx] = "Snowy\n\n(Imagine him on the led)";
        tempIdx++;
        dialogueString[tempIdx] = "Thomson and Thompson\n\nHi! Hello there, Tintin!";
        tempIdx++;
        dialogueString[tempIdx] = "Thompson\n\nWaiter, bring another drink!";
        tempIdx++;
        dialogueString[tempIdx] = "Waiter\n\nYes, sir";
        tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nMy dear Tintin, how nice to see you again!...";
        tempIdx++;
        dialogueString[tempIdx] = "Thompson\n\nto be precise; how nice to see you again, my dear Tintin!";
        tempIdx++;
        dialogueString[tempIdx] = "Waiter\n\nHere you are, sir";
        tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nYour health!";
        tempIdx++;
        dialogueString[tempIdx] = "Thompson\n\nand yours!";
        tempIdx++;
        dialogueString[tempIdx] = "Tintin\n\nMy dear old friends, how nice to see you again!";
        tempIdx++;
        //page 3
        dialogueString[tempIdx] = "Tintin\n\nwell, now, what’s going on?";
        tempIdx++;
        dialogueString[tempIdx] = "Thompson\n\nEverything’s fine: we’ve just been entrusted with a very important case.";
        tempIdx++;
        dialogueString[tempIdx] = "Tintin\n\noh?";
        tempIdx++;
        dialogueString[tempIdx] = "Thompson\n\nTo be precise; a very.. … er … important case";
        tempIdx++;
        dialogueString[tempIdx] = "Tintin\n\noh?";
        tempIdx++;
        dialogueString[tempIdx] = "Thompson\n\nLook… have you read this ?";
        tempIdx++;
        dialogueString[tempIdx] = "Tintin\n\n'Watch out for counterfeit coins!'... yes, I saw it";
        tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nWell, we two have been instructed to clear this thing up";
        tempIdx++;
        dialogueString[tempIdx] = "Tintin\n\nOh?... Jolly good! I say, is it easy to spot one of these fakes ?";
        tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\noh, you know how it is. people like ourselves who have examined them can tell one in a flash of course.";
        tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nwaiter! how much ?";
        tempIdx++;
        dialogueString[tempIdx] = "Waiter\n\nForty five pence, sir";
        tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nHere’s fifty pence! But most people are easily fooled by them";
        tempIdx++;
        dialogueString[tempIdx] = "Waiter\n\nI’m sorry, sir";
        tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nGood gracious, someone’s slipped me a dud fifty pence piece!";
        tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nThere!";
        tempIdx++;
        dialogueString[tempIdx] = "Waiter\n\nThank You!";
        tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nIf you’ve nothing to do, come along with us. We'll show you the papers we’ve already collected in our investigations";
        tempIdx++;
        dialogueString[tempIdx] = "Thanks";
        tempIdx++;
        dialogueString[tempIdx] = "Where did you put those papers?";
        tempIdx++;
        dialogueString[tempIdx] = "But you put them away yourself!";
        tempIdx++;
        dialogueString[tempIdx] = "!";
        tempIdx++;
        //page 4
        dialogueString[tempIdx] = "Tintin\n\nWhat’s that ?";
        tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nThat?... It all came from police headquarters.\nThey are things taken from a body found in the sea.\nDid you notice?\nhe had five coins on him, all duds… Odd, don’t you think?";
        tempIdx++;
        dialogueString[tempIdx] = "Tintin\n\nVery odd! May I… ?";
        tempIdx++;
        dialogueString[tempIdx] = "Tintin\n\nI’ll be back in a minute!";
        tempIdx++;
        dialogueString[tempIdx] = "Thomson and Thompson\n\n?";
        tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nI’m going after him!";
        tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nWhat’s bitten him!";
        tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nGood gracious! I’ve forgotten my stick!";
        tempIdx++;
        dialogueString[tempIdx] = "Thompson\n\nGood gracious !  He’s forgotten his stick!";
        tempIdx++;
        //other
		index = 0;
        DialogueText.text = dialogueString[0];
	}

    void Update()
    {
        if( Input.GetKeyDown( "space" ) )
        {
            index++;
            DialogueText.text = dialogueString[index];
        }

    }
	
}
