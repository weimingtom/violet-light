using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LinearInterpolation : MonoBehaviour 
{

    private class ItemToInterpolate
    {
        public ItemToInterpolate( GameObject obj, Vector3 pos, float ease, int type )
        {
            myObject = obj;
            finalPos = pos;
            easeDuration = ease;
            lerpType = type;
            originalPos = obj.transform.position;
        }

        public bool UpdatePosition()
        {
            if( lerpType == 0 )
            {
                Vector3 newCords = new Vector3( myObject.transform.position.x, myObject.transform.position.y, myObject.transform.position.z );
                newCords.x += (finalPos.x - myObject.transform.position.x) / easeDuration;
                newCords.y += (finalPos.y - myObject.transform.position.y) / easeDuration;

                myObject.transform.position = newCords;

                return myObject.transform.position == finalPos;
            }
            else if( lerpType == 1 )
            {
                count += Time.deltaTime / easeDuration;
                myObject.transform.position = Vector3.Lerp( originalPos, finalPos, Mathf.SmoothStep( 0.0f, 1.0f, count));
                return myObject.transform.position == finalPos;
            }
            else
                return true;
            
        }

        private float count = 0.0f;
        private Vector3 originalPos;
        private GameObject myObject;
        private Vector3 finalPos;
        private float easeDuration;
        private float lerpType;
    }


    private List<ItemToInterpolate> interpolateList = new List<ItemToInterpolate>();

    //LerpType 0 for ease out, LerpType 1 for ease in and out
    public void Interpolate(GameObject item, Vector3 newPos, float easeDuration = 2.0f, int LerpType = 0)
    {
        interpolateList.Add( new ItemToInterpolate( item, newPos, easeDuration, LerpType ) );
    }
	
	// Update is called once per frame
	void Update () 
    {
        for( int i = 0; i < interpolateList.Count; ++i )
        {
            if( interpolateList[i].UpdatePosition() )
            {
                interpolateList.Remove( interpolateList[i] );
                break;
            }
        }
	}
}
