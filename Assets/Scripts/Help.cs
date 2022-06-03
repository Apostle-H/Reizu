using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    [SerializeField] private GameObject tutor;

    public void ShowHide()
    {
        if (tutor != null)
            tutor.SetActive(!tutor.activeSelf);
    }
}
