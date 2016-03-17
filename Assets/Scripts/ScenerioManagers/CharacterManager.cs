using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour {

    public enum Positions
    {
        Offscreen,
        Left2,
        Left1,
        Centre,
        Right1,
        Right2
    }
    public enum Facings
    { 
        auto,
        left,
        right
    }
    
    /************************ CHARACTER CODE ******************************/
    private Dictionary<string, Character> characterList = new Dictionary<string,Character>();
    
    public float defaultEasingDuration = 2.0f;
    public float defaultDeltaAlpha = 2.5f;
    private const float zValue = -3.0f;

    static public CharacterManager Instance;
    void Awake()
    { Instance = this;  }

    //TODO account for case of starting another movement before last has finished. 
    private List<string> changingPortraits = new List<string>();
    public bool IsCharacter(string name)
    {
        if( characterList.ContainsKey( name ) )
        {
            return true;
        }
        return false;
    }
	void Update () 
    {
        for( int i = 0; i < changingPortraits.Count; i++ )
        {
            if( characterList[changingPortraits[i]].UpdatePosition() )
            {
                changingPortraits.Remove( changingPortraits[i] );
                break;
            }
        }
	}

    bool GetDoneFade()
    { return changingPortraits.Count == 0; }

    //this just teleports all offscreen for now.
    public void FadeAllOut()
    {
        foreach( KeyValuePair<string, CharacterManager.Character> entry in characterList )
        {
            entry.Value.mGObject.transform.position = new Vector3( -30.0f, 0.0f, zValue );
        }   
    }
    
    public void ChangePosition(string character, Positions newPosition = Positions.Offscreen, Facings facing = Facings.auto, float fadeSpeed = 0, float easeSpeed = 0) 
    {
        if(fadeSpeed == 0)
        {
            fadeSpeed = defaultDeltaAlpha;
        }
        if (easeSpeed == 0)
        {
            easeSpeed = defaultEasingDuration;
        }
        changingPortraits.Add(character);

        if(newPosition == Positions.Offscreen)
            fadeSpeed *= -1;

        Debug.Log( "<color=green>[Char Man]</color> Char trying to change pose:" + character + "To Position -" + newPosition.ToString());
        Debug.Assert( IsCharacter( character ), "CHRACTER IS NOT A CHARACTER" );

        characterList[character.ToLower()].SetForMovement(newPosition, facing, fadeSpeed, easeSpeed);
    }

    public void KillCharacter(string character)
    {
        characterList[character.ToLower()].mGObject.transform.position = new Vector3(-30.0f, 0.0f, zValue);
    }

    public void ChangeCharacterPose(string name, string pose)
    {
        try
        {
            Debug.Log( "Success ! [Character Manager] name : " + name + "[Character Manager] pose : "  + pose);
            characterList[name.ToLower()].ChangePose(pose);
        }
        catch( KeyNotFoundException )
        {
            if( name == null )
            {
                Debug.Log( "Failed ! [Character Manager] name is null" );
            }
            else if( pose == null )
            {
                Debug.Log("Failed ! [Character Manager] pose is null");
            }
            else
            {
                Debug.Log("Failed to get key [" + name + "]" + " pose : " + pose);
                //Debug.Break();
            }
        }
    }
    public void addCharacter( string key, string name )
    {
        characterList.Add(key.ToLower(), new CharacterManager.Character());
        characterList[key].Initialize( name );
    }
    public void AddCharacterPose( string key, string poseName, string posePath )
    {
        characterList[key].AddPose( poseName, posePath );
    }
    public void AddCharacterExpression( string key, string exprName, string exprPath )
    {
        characterList[key].AddExpression( exprName, exprPath );
    }
    public void SetAllToNeutral()
    {
        foreach( KeyValuePair<string, CharacterManager.Character> entry in characterList )
        {
            entry.Value.ChangePose( "neutral" );
        }
    }
    public Positions GetPosition( string character )
    {
        try
        { 
            return characterList[character.ToLower()].positionToGoTo;
        }
        catch( KeyNotFoundException )
        {
            //Debug.Log( "name[" + character + "] <color=red>Postion not found!</color> is this intentional ??" );
            return Positions.Left1;
        }
    }
    public void GetCharacter( string character, ref GameObject charGameObj, ref short invert )
    {
        foreach( KeyValuePair<string, CharacterManager.Character> entry in characterList )
        {
            if( entry.Key == character )
            {
                charGameObj = entry.Value.GetGameObject();
                if( entry.Value.GetFacing() == Facings.left )
                    invert = 1;
                else
                    invert = -1;
                return;
            }
        }
        Debug.Log( "ERROR: Character " + character + " not found. [SurpriseChar]" );
        invert = 0;
        charGameObj = null;
    }

    private class Character
    {

        private string mName;
        public GameObject mGObject;

        //list of filepaths to all the poses
        private Dictionary<string, string> mPoses;
        private SpriteRenderer mPortrait;
        private Facings currentFacing; 

        //list of filepaths to all expressions
        private Dictionary<string, string> mExpressions;

        private List<bool> mFlags;  //will add functionality later if time.
        private int mAffinity;      //will add functionality later if time.

        //new stuffs
        private float easeDuration;
        private float deltaAlpha;
        public Positions positionToGoTo;
        private float yValue = -0.5f;


        public Character()
        { mAffinity = 0; }

        //TODO: change Initialize to take a filepath to a file for that character, parse the file and add all expressions and stuff from here
        public void Initialize(string name)
        {
            //reference to this code later
            mName = name;
            mGObject = new GameObject(name);
            mGObject.transform.position = new Vector3(-30, 0, zValue);
            mPortrait = mGObject.AddComponent<SpriteRenderer>();
            //mExpressionRend = mGObject.AddComponent<SpriteRenderer>();
            currentFacing = Facings.left;

            mPoses = new Dictionary<string,string>();
            mExpressions = new Dictionary<string,string>();

        }
        public string GetName()
        {   return mName;   }
        public GameObject GetGameObject()
        { return mGObject; }
        public void AddToAffinity( int change )
        {
            mAffinity += change;
        }
        public void AddPose(string name, string filepath)
        {
            mPoses.Add(name, filepath);
        }
        public void AddExpression(string name, string filepath)
        {
            mExpressions.Add(name, filepath);
        }
        public void ChangePose(string pose)
        {
            //do an assert or something here to check that the pose exists.
            mPortrait.sprite = Resources.Load<Sprite>( mPoses[pose] );
            //Set facing back to default(left)
        }
        public void ChangeExpression(string expression)
        {
            //mExpressionRend.sprite = Resources.Load<Sprite>( mExpressions[expression] );
        }
        public Facings GetFacing()
        { return currentFacing;  }

        public void SetForMovement(Positions position, Facings facing, float fadeSpeed, float easeSpeed)
        {
            if(position != Positions.Offscreen)
                mPortrait.color = new Color( 1f, 1f, 1f, 0f );
            //mExpressionRend.color = new Color( 1f, 1f, 1f, 0f );

            easeDuration = easeSpeed;
            deltaAlpha = fadeSpeed;
            positionToGoTo = position;


            //find facing
            if( facing == Facings.auto )
            {
                if( position == Positions.Left1 || position == Positions.Left2 || position == Positions.Centre )
                    facing = Facings.right;
                else
                    facing = Facings.left;
            }

            if( facing != currentFacing )
            { 
                //flip the sprite
                Vector3 scale = mPortrait.transform.localScale;
                scale.x *= -1;
                mPortrait.transform.localScale = scale;
                //set new currentFacing
                currentFacing = facing;
            }

            mGObject.transform.position = FindStartPlace( position );
        }

        //TODO: change these to be able to adapt to the screen size. NO MORE MAGIC NUMBERS
        private Vector3 FindStartPlace( Positions position )
        {
            Vector3 startPlace =  Vector3.zero;;
             switch(position)
             {
                case Positions.Left1:
                     startPlace = new Vector3(-10.0f, yValue, zValue);
                break;

                case Positions.Left2:
                startPlace = new Vector3(-6.0f, yValue, zValue);
                break;

                case Positions.Centre:
                startPlace = new Vector3(0.0f, yValue-3.0f, zValue);
                Debug.Log("[Char Manager] Start place is Center!");
                break;

                case Positions.Right1:
                startPlace = new Vector3(6.0f, yValue, zValue);
                break;

                case Positions.Right2:
                startPlace = new Vector3(10.0f, yValue, zValue);
                break;
         
                case Positions.Offscreen:
                startPlace = new Vector3(-30.0f, mPortrait.transform.position.y, mPortrait.transform.position.z );
                break;
             }

             return startPlace;
        }
        private Vector3 FindEndPlace( Positions position ) 
        {
            Vector3 endPlace = Vector3.zero;
            switch( position )
            {
            case Positions.Left1:
                    endPlace = new Vector3(-6.0f, yValue, zValue);
            break;

            case Positions.Left2:
            endPlace = new Vector3(-3.0f, yValue, zValue);
            break;

            case Positions.Centre:
            endPlace = new Vector3(0.0f, yValue, zValue);
            break;

            case Positions.Right1:
            endPlace = new Vector3(3.0f, yValue, zValue);
            break;

            case Positions.Right2:
            endPlace = new Vector3(6.0f, yValue, zValue);
            break;

            case Positions.Offscreen:
            endPlace = new Vector3( -30.0f, mPortrait.transform.position.y, mPortrait.transform.position.z );
            break;
            }

            return endPlace;
        }

        //returns true when done
        public bool UpdatePosition()
        {
            float alpha = mPortrait.color.a;
            Vector3 destination;
            destination = FindEndPlace( positionToGoTo );

           

            if( alpha < 1.0 ) //Still doing Fade
            {
                mPortrait.color = new Color( 1f, 1f, 1f, alpha + (deltaAlpha * Time.deltaTime) );
                //mExpressionRend.color = new Color( 1f, 1f, 1f, alpha + (deltaAlpha * Time.deltaTime));
                alpha = mPortrait.color.a;
            }
            

            //ease out
            Vector3 newCords = new Vector3(mPortrait.transform.position.x, mPortrait.transform.position.y, zValue);
            newCords.x += (destination.x - mPortrait.transform.position.x) / easeDuration;
            newCords.y += (destination.y - mPortrait.transform.position.y) / easeDuration;

            mPortrait.transform.position = newCords;


            if((alpha >= 1.0f || alpha == 0.0f) && mGObject.transform.position == destination)
            {
                if( positionToGoTo == Positions.Offscreen )
                    mPortrait.transform.position = new Vector3(-30.0f, yValue, zValue);
                return true;
            }


            return false;
        }

    }


	
}
