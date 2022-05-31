using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TameResolver : MonoBehaviour
{
    [SerializeField] private GameObject tamePanel;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void StartTame(int tameChance, Rarity rarity)
    {
        tamePanel.SetActive(true);

    }

    public void TryTame()
    {

    }
}
