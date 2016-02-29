using UnityEngine;
using System.Collections;

public class PanDown : MonoBehaviour
{

    public Vector3 endPosition;
    private Vector3 startPosition;
    public float speed;
    private float startTime;
    private float journeyLength;

    private bool started;

    public void StartPan()
    {
        startPosition = Camera.main.transform.position;
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
            Camera.main.transform.position = Vector3.Lerp( startPosition, endPosition, fracJourney );

        }

    }
}
