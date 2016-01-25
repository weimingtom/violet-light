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
    
    public const float defaultEasingDuration = 2.0f;
    public const float defaultDeltaAlpha = 2.5f;
    private const float zValue = -3.0f;
    

    static public CharacterManager Instance;
    void Awake()
    { Instance = this;  }

    //TODO account for case of starting another movement before last has finished. 
    private List<string> changingPortraits;

	void Update () 
    {
        foreach (string thisString in changingPortraits)
        {
            if( characterList[thisString].UpdatePosition() )
            {
                changingPortraits.Remove( thisString );
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
    
    public void ChangePosition(string character, Positions newPosition = Positions.Offscreen, Facings facing = Facings.auto, float fadeSpeed = defaultDeltaAlpha, float easeSpeed = defaultEasingDuration) 
    {
        changingPortraits.Add(character);

        if(newPosition == Positions.Offscreen)
            fadeSpeed *= -1;
        
        characterList[character].SetForMovement(newPosition, facing, fadeSpeed, easeSpeed);
    }

    public void ChangeCharacterPose(string name, string pose)
    {
        characterList[name].ChangePose(pose);
    }
    public void addCharacter( string key, string name )
    {
        characterList.Add( key, new CharacterManager.Character() );
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

    private class Character
    {

        //May need to change expressions so there is functionality to change position
        //of the face when the pose changes. This is to save memory and not save
        //multiples of the same face just slightly moved or rotated.

        private string mName;
        public GameObject mGObject;

        //list of filepaths to all the poses
        private Dictionary<string, string> mPoses;
        private SpriteRenderer mPortrait;
        private Facings currentFacing; 

        //list of filepaths to all expressions
        private Dictionary<string, string> mExpressions;
        //private GameObject mExpressionGO; //not needed for now.
        //private SpriteRenderer mExpressionRend;

        private List<bool> mFlags;  //will add functionality later if time.
        private int mAffinity;      //will add functionality later if time.

        //new stuffs
        private float easeDuration;
        private float deltaAlpha;
        private Positions positionToGoTo;


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
            Vector3 startPlace;
             switch(position)
             {
                case Positions.Left1:
                     startPlace = new Vector3(-10.0f, 0f, zValue);
                break;

                case Positions.Left2:
                startPlace = new Vector3(-6.0f, 0f, zValue);
                break;

                case Positions.Centre:
                startPlace = new Vector3(0.0f, 0f, zValue);
                break;

                case Positions.Right1:
                startPlace = new Vector3(6.0f, 0f, zValue);
                break;

                case Positions.Right2:
                startPlace = new Vector3(10.0f, 0f, zValue);
                break;
         
                case Positions.Offscreen:
                default:
                startPlace = new Vector3( mPortrait.transform.position.x, mPortrait.transform.position.y, mPortrait.transform.position.z );
                break;
             }

             return startPlace;
        }
        private Vector3 FindEndPlace( Positions position ) 
        {
            Vector3 endPlace;
            switch( position )
            {
            case Positions.Left1:
                    endPlace = new Vector3(-6.0f, 0f, zValue);
            break;

            case Positions.Left2:
            endPlace = new Vector3(-3.0f, 0f, zValue);
            break;

            case Positions.Centre:
            endPlace = new Vector3(0.0f, 0f, zValue);
            break;

            case Positions.Right1:
            endPlace = new Vector3(3.0f, 0f, zValue);
            break;

            case Positions.Right2:
            endPlace = new Vector3(6.0f, 0f, zValue);
            break;

            case Positions.Offscreen:
            default:
            endPlace = new Vector3( mPortrait.transform.position.x, mPortrait.transform.position.y, mPortrait.transform.position.z );
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
                    mPortrait.transform.position = new Vector3( -30.0f, 0.0f, zValue );
                return true;
            }


            return false;
        }

    }


	
}
