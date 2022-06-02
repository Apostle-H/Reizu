using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Button Play;
    [SerializeField] private Button MainMenu;
    [SerializeField] private Button Exit;

    private void OnEnable()
    {
        Play?.onClick.AddListener(() => LoadScene(1));
        MainMenu?.onClick.AddListener(() => LoadScene(0));
        Exit?.onClick.AddListener(() => Application.Quit());
    }

    private void OnDisable()
    {
        Play?.onClick.RemoveListener(() => LoadScene(1));
        MainMenu?.onClick.RemoveListener(() => LoadScene(0));
        Exit?.onClick.RemoveListener(() => Application.Quit());
    }

    private void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
