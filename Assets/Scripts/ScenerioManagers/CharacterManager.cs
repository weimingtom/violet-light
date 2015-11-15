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

    /****************** SCREEN SHAKE CODE ********************/
    Vector3 originalCameraPosition;
    private float radius;
    private float randomAngle;
    private Vector3 offset;
    public Camera mainCamera;

    public void StartShake( float magnitude = 0.7f)
    {
        radius = magnitude;
        float randomAngle =  Random.value * 360;
        offset = new Vector3( Mathf.Sin(randomAngle) * radius , Mathf.Cos(randomAngle) * radius, originalCameraPosition.z); //create offset 2d vector
        InvokeRepeating( "CameraShake", 0, .02f );
    }

    private void CameraShake()
    {
        if( radius > 0.25 )
        {
            radius *= 0.9f; //diminish radius
            randomAngle += Random.value > 0.5 ? (180 + Random.value * 60) : (180 - Random.value * 60);
            offset = new Vector3( Mathf.Sin( randomAngle ) * radius, Mathf.Cos( randomAngle ) * radius, originalCameraPosition.z );
            mainCamera.transform.position = originalCameraPosition + offset;
        }
        else 
        {
            CancelInvoke( "CameraShake" );
            mainCamera.transform.position = originalCameraPosition;
        }
    }
    
    /************************ CHARACTER CODE ******************************/
    private Dictionary<string, Character> characterList = new Dictionary<string,Character>();
    
    public const float defaultEasingDuration = 2.0f;
    public const float defaultDeltaAlpha = 2.5f;
    private const float zValue = -3.0f;
    private float easeDuration;
    private float deltaAlpha;

    static public CharacterManager Instance;
    void Awake()
    { Instance = this;  }

    //TODO account for case of starting another movement before last has finished. 
    private string changingPortrait;
    private Positions positionToGoTo;
    
    void Start()
    {
        changingPortrait = null;
        originalCameraPosition = mainCamera.transform.position;
    }

	void Update () 
    {
        if( changingPortrait != null )
            if( characterList[changingPortrait].GoToNewPosition( positionToGoTo, deltaAlpha, easeDuration ) )
            {
                changingPortrait = null;
                positionToGoTo = Positions.Offscreen;
            }
	}
    
    public void ChangePosition(string character, Positions newPosition = Positions.Offscreen, float fadeSpeed = defaultDeltaAlpha, float easeSpeed = defaultEasingDuration) 
    {
        changingPortrait = character;
        positionToGoTo = newPosition;
        characterList[character].SetForMovement(newPosition);
        easeDuration = easeSpeed;
        deltaAlpha = fadeSpeed;
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

        //list of filepaths to all expressions
        private Dictionary<string, string> mExpressions;
        //private GameObject mExpressionGO; //not needed for now.
        //private SpriteRenderer mExpressionRend;

        private List<bool> mFlags;  //will add functionality later if time.
        private int mAffinity;      //will add functionality later if time.

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
        }
        public void ChangeExpression(string expression)
        {
            //mExpressionRend.sprite = Resources.Load<Sprite>( mExpressions[expression] );
        }

        public void SetForMovement(Positions position)
        {
            mPortrait.color = new Color( 1f, 1f, 1f, 0f );
            //mExpressionRend.color = new Color( 1f, 1f, 1f, 0f );
            mGObject.transform.position = FindStartPlace( position );
        }

        //TODO: change these to be able to adapt to the screen size.
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
        public bool GoToNewPosition( Positions newPosition, float DeltaAlpha, float easeDuration )
        {
            float alpha = mPortrait.color.a;
            Vector3 destination;
            destination = FindEndPlace( newPosition );

           

            if( alpha < 1.0 ) //Still doing Fade and ease
            {
                mPortrait.color = new Color( 1f, 1f, 1f, alpha + (DeltaAlpha * Time.deltaTime) );
                //mExpressionRend.color = new Color( 1f, 1f, 1f, alpha + (deltaAlpha * Time.deltaTime));
            }

            //ease out
            Vector3 newCords = new Vector3(mPortrait.transform.position.x, mPortrait.transform.position.y, zValue);
            newCords.x += (destination.x - mPortrait.transform.position.x) / easeDuration;
            newCords.y += (destination.y - mPortrait.transform.position.y) / easeDuration;

            mPortrait.transform.position = newCords;


            if(alpha >= 1.0f && mGObject.transform.position == destination)
            {
                return true;
            }


            return false;
        }

    }


	
}
