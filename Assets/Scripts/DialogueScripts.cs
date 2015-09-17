using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class DialogueScripts : MonoBehaviour 
{
	struct CharacterAndConversation
	{
		eCharacter character;
		string dialogue;
	}
	enum eCharacter
	{
		Background = 0,
		Snowy = 1,
		Tintin = 2,
		Thomson = 3,
		Thompson = 4,
		Waiter = 5,
	}
	private List<eCharacter> myCharacter;
    private int index;
    private string[] dialogueString;
    public Text DialogueText;
	void Start () 
    {
        int tempIdx = 0;
		dialogueString = new string[100];
        //page 1
        dialogueString[tempIdx] = "Snowy\n\nYow!...\now! ...ow!";
		myCharacter.Add (eCharacter.Snowy);
        tempIdx++;
        dialogueString[tempIdx] = "Tintin\n\n?";
		myCharacter.Add (eCharacter.Tintin);
		tempIdx++;
        dialogueString[tempIdx] = "Tintin\n\nThere you are snowy. You see what comes of your dirty habit of exploring rubbish bins…\n I don’t go scavenging do i ?";
		myCharacter.Add (eCharacter.Tintin);
		tempIdx++;

        //page 2
        dialogueString[tempIdx] = "Tintin\n\nYou’ve been lucky!\n you could have cut yourself. Look how jagged the edges are";
		myCharacter.Add (eCharacter.Tintin);
		tempIdx++;
        dialogueString[tempIdx] = "Tintin\n\nNow, come on!... \nAnd don’t do that again, \nor i’ll buy a muzzle and you’ll walk on a lead!";
		myCharacter.Add (eCharacter.Tintin);
		tempIdx++;
        dialogueString[tempIdx] = "Snowy\n\n(Imagine him on the led)";
		myCharacter.Add (eCharacter.Snowy);
		tempIdx++;
        dialogueString[tempIdx] = "Thomson and Thompson\n\nHi! Hello there, Tintin!";
		myCharacter.Add (eCharacter.Thomson);
		tempIdx++;
        dialogueString[tempIdx] = "Thompson\n\nWaiter, bring another drink!";
		myCharacter.Add (eCharacter.Thompson);
		tempIdx++;
        dialogueString[tempIdx] = "Waiter\n\nYes, sir";
		myCharacter.Add (eCharacter.Waiter);
		tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nMy dear Tintin, how nice to see you again!...";
		myCharacter.Add (eCharacter.Thomson);
		tempIdx++;
        dialogueString[tempIdx] = "Thompson\n\nto be precise; how nice to see you again, my dear Tintin!";
		myCharacter.Add (eCharacter.Thompson);
		tempIdx++;
        dialogueString[tempIdx] = "Waiter\n\nHere you are, sir";
		myCharacter.Add (eCharacter.Waiter);
		tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nYour health!";
		myCharacter.Add (eCharacter.Thomson);
        tempIdx++;
        dialogueString[tempIdx] = "Thompson\n\nand yours!";
		myCharacter.Add (eCharacter.Thompson);
        tempIdx++;
        dialogueString[tempIdx] = "Tintin\n\nMy dear old friends, how nice to see you again!";
		myCharacter.Add (eCharacter.Tintin);
		tempIdx++;

        //page 3
        dialogueString[tempIdx] = "Tintin\n\nwell, now, what’s going on?";
		myCharacter.Add (eCharacter.Tintin);
        tempIdx++;
        dialogueString[tempIdx] = "Thompson\n\nEverything’s fine: we’ve just been entrusted with a very important case.";
		myCharacter.Add (eCharacter.Thompson);
		tempIdx++;
        dialogueString[tempIdx] = "Tintin\n\noh?";
		myCharacter.Add (eCharacter.Tintin);
		tempIdx++;
		dialogueString[tempIdx] = "Thompson\n\nTo be precise; a very.. … er … important case";
		myCharacter.Add (eCharacter.Thompson);
		tempIdx++;
		dialogueString[tempIdx] = "Tintin\n\noh?";
		myCharacter.Add (eCharacter.Tintin);
        tempIdx++;
		dialogueString[tempIdx] = "Thompson\n\nLook… have you read this ?";
		myCharacter.Add (eCharacter.Thompson);
		tempIdx++;
		dialogueString[tempIdx] = "Tintin\n\n'Watch out for counterfeit coins!'... yes, I saw it";
		myCharacter.Add (eCharacter.Tintin);
		tempIdx++;
		dialogueString[tempIdx] = "Thomson\n\nWell, we two have been instructed to clear this thing up";
		myCharacter.Add (eCharacter.Thompson);
		tempIdx++;
		dialogueString[tempIdx] = "Tintin\n\nOh?... Jolly good! I say, is it easy to spot one of these fakes ?";
		myCharacter.Add (eCharacter.Tintin);
		tempIdx++;
		dialogueString[tempIdx] = "Thomson\n\noh, you know how it is. people like ourselves who have examined them can tell one in a flash of course.";
		myCharacter.Add (eCharacter.Thomson);
		tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nwaiter! how much ?";
		myCharacter.Add (eCharacter.Thomson);
		tempIdx++;
        dialogueString[tempIdx] = "Waiter\n\nForty five pence, sir";
		myCharacter.Add (eCharacter.Waiter);
		tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nHere’s fifty pence! But most people are easily fooled by them";
		myCharacter.Add (eCharacter.Thomson);
		tempIdx++;
        dialogueString[tempIdx] = "Waiter\n\nI’m sorry, sir";
		myCharacter.Add (eCharacter.Waiter);
        tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nGood gracious, someone’s slipped me a dud fifty pence piece!";
		myCharacter.Add (eCharacter.Thomson);
		tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nThere!";
		myCharacter.Add (eCharacter.Thomson);
		tempIdx++;
        dialogueString[tempIdx] = "Waiter\n\nThank You!";
		myCharacter.Add (eCharacter.Waiter);
		tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nIf you’ve nothing to do, come along with us. We'll show you the papers we’ve already collected in our investigations";
		myCharacter.Add (eCharacter.Thomson);
		tempIdx++;
        dialogueString[tempIdx] = "Thanks";
		myCharacter.Add (eCharacter.Background);
        tempIdx++;
        dialogueString[tempIdx] = "Where did you put those papers?";
		myCharacter.Add (eCharacter.Background);
        tempIdx++;
        dialogueString[tempIdx] = "But you put them away yourself!";
		myCharacter.Add (eCharacter.Background);
		tempIdx++;
        dialogueString[tempIdx] = "!";
		myCharacter.Add (eCharacter.Background);
        tempIdx++;

        //page 4
        dialogueString[tempIdx] = "Tintin\n\nWhat’s that ?";
		myCharacter.Add (eCharacter.Tintin);
        tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nThat?... It all came from police headquarters.\nThey are things taken from a body found in the sea.\nDid you notice?\nhe had five coins on him, all duds… Odd, don’t you think?";
		myCharacter.Add (eCharacter.Thomson);
		tempIdx++;
        dialogueString[tempIdx] = "Tintin\n\nVery odd! May I… ?";
		myCharacter.Add (eCharacter.Tintin);
		tempIdx++;
        dialogueString[tempIdx] = "Tintin\n\nI’ll be back in a minute!";
		myCharacter.Add (eCharacter.Tintin);
		tempIdx++;
        dialogueString[tempIdx] = "Thomson and Thompson\n\n?";
		myCharacter.Add (eCharacter.Thomson);
		tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nI’m going after him!";
		myCharacter.Add (eCharacter.Thomson);
        tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nWhat’s bitten him!";
		myCharacter.Add (eCharacter.Thomson);
        tempIdx++;
        dialogueString[tempIdx] = "Thomson\n\nGood gracious! I’ve forgotten my stick!";
		myCharacter.Add (eCharacter.Thomson);
		tempIdx++;
        dialogueString[tempIdx] = "Thompson\n\nGood gracious !  He’s forgotten his stick!";
		myCharacter.Add (eCharacter.Thomson);
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
			string temporaryText = dialogueString[index];

			int count = 0;
			string breakOut = "\n";
			string name;

        }

    }
	
}
