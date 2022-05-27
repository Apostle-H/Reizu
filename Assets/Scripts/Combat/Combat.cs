using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Enemy[] enemies;

    public void SetUpCombat(Enemy[] enemies)
    {
        this.enemies = enemies;
    }
}
