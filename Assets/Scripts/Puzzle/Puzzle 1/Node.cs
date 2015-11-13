using UnityEngine;
using System.Collections;

public class Node
{
    public enum eColor
    { 
        None,
        Red,
        Green,
        Blue,
        yellow
    }
    public eColor colorTag = eColor.None;
    private Vector3 originPosition;
    private Vector3 targetPosition;
    private bool activeStatus;
    void Start()
    {
        colorTag = eColor.None;
        originPosition = new Vector3();
        targetPosition = new Vector3();
        activeStatus = false;
    }

    public void SetOriginPosition( Vector3 target )
    {
        originPosition = target;
    }
    public void SetTargetPosition(Vector3 target)
    {
        targetPosition = target;
    }
    public Vector3 GetOriginCoordinate()
    {
        return originPosition;
    }
    public Vector3 GetTargetCoordinate()
    {
        return targetPosition;
    }
    public void SetActive(bool status)
    {
        activeStatus = status;
    }
    public bool GetStatus()
    {
        return activeStatus;
    }
}
