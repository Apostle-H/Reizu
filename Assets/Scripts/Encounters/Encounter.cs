using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Encounter : MonoBehaviour
{
    public delegate void EndEncounter();
    public event EndEncounter OnEndEncounter;

    public abstract void Appear();
}
