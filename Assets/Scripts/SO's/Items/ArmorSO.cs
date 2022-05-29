using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSO : ItemSO
{
    [SerializeField] private int defence;
    [SerializeField] private Stat risingStat;

    public (Stat, int) GetValues() => (risingStat, defence);
}
