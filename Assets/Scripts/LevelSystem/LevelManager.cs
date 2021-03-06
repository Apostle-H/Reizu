using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Platform[] platforms;

    private int indexer = -1;

    public Platform[] Platfroms { get { return platforms; } }

    public Platform next { get { if (indexer < platforms.Length - 1) return platforms[++indexer]; else return null; } }
}
