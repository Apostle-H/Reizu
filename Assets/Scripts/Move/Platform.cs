using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Transform mainAnchor;

    public void MoveTo(Transform movedObject)
    {
        movedObject.position = mainAnchor.position;
    }
}
