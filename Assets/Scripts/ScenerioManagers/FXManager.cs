using UnityEngine;
using System.Collections;

public class FXManager : MonoBehaviour {

    static public FXManager Instance;

    void Awake()
    {
        Instance = this;
    }


    /****************** SCREEN SHAKE CODE ********************/
    Vector3 originalCameraPosition;
    private float radius;
    private float randomAngle;
    private Vector3 offset;
    public Camera mainCamera;

    public void StartShake( float magnitude = 0.7f )
    {
        originalCameraPosition = mainCamera.transform.position;
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
            mainCamera.transform.position = originalCameraPosition + offset;
        }
        else
        {
            CancelInvoke( "CameraShake" );
            mainCamera.transform.position = originalCameraPosition;
        }
    }

}
