using UnityEngine;
using System.Collections;

public class Case0Body : MonoBehaviour 
{

    public Vector3 endPosition;
    private Vector3 startPosition;
    public float speed;
    private float startTime;
    private float journeyLength;

    private bool started;

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
            if(this.transform.position.y == endPosition.y)
            {
                this.gameObject.SetActive( false );
            }
        }

    }
}
