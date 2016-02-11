using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LinearInterpolation : MonoBehaviour 
{

    private class ItemToInterpolate
    {
        public ItemToInterpolate( GameObject obj, Vector3 pos, float ease )
        {
            myObject = obj;
            finalPos = pos;
            easeDuration = ease;
        }

        public bool UpdatePosition()
        {
            Vector3 newCords = new Vector3( myObject.transform.position.x, myObject.transform.position.y, myObject.transform.position.z );
            newCords.x += (finalPos.x - myObject.transform.position.x) / easeDuration;
            newCords.y += (finalPos.y - myObject.transform.position.y) / easeDuration;

            myObject.transform.position = newCords;

            return myObject.transform.position == finalPos;
        }


        private GameObject myObject;
        private Vector3 finalPos;
        private float easeDuration;
    }


    private List<ItemToInterpolate> interpolateList = new List<ItemToInterpolate>();

    public void Interpolate(GameObject item, Vector3 newPos, float easeDuration = 2.0f)
    {
        interpolateList.Add( new ItemToInterpolate( item, newPos, easeDuration ) );
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
