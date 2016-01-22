using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour 
{

    Vector3 originalCameraPosition;
    private float radius;
    private float randomAngle;
    private Vector3 offset;
    float magnitude = 0.7f;

    public void Start()
    {
        originalCameraPosition = new Vector3( 0.0f, 0.0f, -10.0f ); //Camera.main.transform.position;
        radius = magnitude;
        float randomAngle = Random.value * 360;
        offset = new Vector3( Mathf.Sin( randomAngle ) * radius, Mathf.Cos( randomAngle ) * radius, originalCameraPosition.z ); //create offset 2d vector
        InvokeRepeating( "CameraShake", 0, .02f );
    }
	
    private void CameraShake()
    {
        if( radius > 0.25 )
        {
            radius *= 0.9f; //diminish radius
            randomAngle += Random.value > 0.5 ? (180 + Random.value * 60) : (180 - Random.value * 60);
            offset = new Vector3( Mathf.Sin( randomAngle ) * radius, Mathf.Cos( randomAngle ) * radius, originalCameraPosition.z );
            Camera.main.transform.position = originalCameraPosition + offset;
        }
        else
        {
            CancelInvoke( "CameraShake" );
            Camera.main.transform.position = originalCameraPosition;
            Destroy( this.gameObject );
        }
    }

}
