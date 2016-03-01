using UnityEngine;
using System.Collections;

public class CutsceneInterpolateSprite : MonoBehaviour 
{

    public Vector3 endPosition;
    private Vector3 startPosition;
    public float speed;
    private float startTime;
    private float journeyLength;

    private bool started;

    public bool startOnAwake = false;

    void Start()
    {

        if(startOnAwake)
        {
            StartPan();
        }
    }

    public void StartPan()
    {
        startPosition = this.transform.position;
        startTime = Time.time;
        journeyLength = Vector3.Distance( startPosition, endPosition );
        started = true;
    }
    void Update()
    {
        if( started )
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            this.transform.position = Vector3.Lerp( startPosition, endPosition, fracJourney );
        }
    }
}
