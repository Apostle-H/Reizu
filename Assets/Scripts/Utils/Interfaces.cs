using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public interface IEntity
{
    public bool TakeDamage(int damage);


    public void Attack(IEntity enemy);
}
