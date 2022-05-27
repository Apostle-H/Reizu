using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] public SidePlatform[] sidePlatfroms;
    [SerializeField] Transform[] anchorPoints;

    public void MoveTo(Transform movedObject)
    {
        movedObject.position = anchorPoints[0].position;
    }
}
